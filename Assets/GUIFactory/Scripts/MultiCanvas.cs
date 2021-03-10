// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Graph3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Graph3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Graph3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;

using UnityEngine;

namespace Graph3DVisualizer.VrHud
{
    public struct CanvasControllerInfo
    {
        public CanvasControllerParameters Parameters { get; set; }
        public Type Type { get; set; }

        public CanvasControllerInfo (Type type, CanvasControllerParameters parameters)
        {
            Type = type;
            Parameters = parameters;
        }

        public static implicit operator (Type, CanvasControllerParameters) (CanvasControllerInfo value) => (value.Type, value.Parameters);

        public static implicit operator CanvasControllerInfo ((Type type, CanvasControllerParameters parameters) value) => new CanvasControllerInfo(value.type, value.parameters);

        public void Deconstruct (out Type type, out CanvasControllerParameters parameters)
        {
            type = Type;
            parameters = Parameters;
        }

        public override bool Equals (object obj) => obj is CanvasControllerInfo other &&
                   EqualityComparer<Type>.Default.Equals(Type, other.Type) &&
                   EqualityComparer<CanvasControllerParameters>.Default.Equals(Parameters, other.Parameters);

        public override int GetHashCode ()
        {
            var hashCode = -1030903623;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<CanvasControllerParameters>.Default.GetHashCode(Parameters);
            return hashCode;
        }
    }

    [CustomizableGrandType(Type = typeof(CanvasControllerParameters))]
    public abstract class AbstractCanvasController : ICustomizable<CanvasControllerParameters>
    {
        public GameObject Root { get; protected set; }

        public CanvasControllerParameters DownloadParams () => new CanvasControllerParameters(Root.transform.localPosition, Root.transform.localEulerAngles, Root.transform.localScale);

        public void SetupParams (CanvasControllerParameters parameters) =>
            (Root.transform.localPosition, Root.transform.localEulerAngles, Root.transform.localScale) = (parameters.LocalPosition, parameters.LocalEulerAngles, parameters.Scale);
    }

    public class CanvasControllerParameters : AbstractCustomizableParameter
    {
        public Vector3 LocalEulerAngles { get; set; }
        public Vector3 LocalPosition { get; set; }
        public Vector3 Scale { get; set; }

        public CanvasControllerParameters (Vector3 localPosition, Vector3 localEulerAngles, Vector3 scale) => (LocalPosition, LocalEulerAngles, Scale) = (localPosition, localEulerAngles, scale);
    }

    [CustomizableGrandType(Type = typeof(MultiCanvasParameters))]
    public class MultiCanvas : MonoBehaviour, ICustomizable<MultiCanvasParameters>
    {
        protected HashSet<AbstractCanvasController> _canvasControllers;
        protected Transform _transform;

        private void Awake ()
        {
            _transform = transform;
            _canvasControllers = new HashSet<AbstractCanvasController>();
        }

        public void AddNewCanvas<T> (Vector3 localPosition, Vector3 localEulerAngles, Vector3 scale) where T : AbstractCanvasController, new()
        {
            var canvasController = new T();
            canvasController.Root.transform.parent = _transform;
            canvasController.Root.transform.localPosition = localPosition;
            canvasController.Root.transform.eulerAngles = localEulerAngles;
            canvasController.Root.transform.localScale = scale;
            _canvasControllers.Add(canvasController);
        }

        public MultiCanvasParameters DownloadParams () =>
            new MultiCanvasParameters(_canvasControllers.Select(c => new CanvasControllerInfo(c.GetType(), (CanvasControllerParameters) CustomizableExtension.CallDownloadParams(c))).ToArray());

        public void SetupParams (MultiCanvasParameters parameters)
        {
            foreach (var info in parameters.CanvasControllerInfos)
            {
                typeof(MultiCanvas).GetMethod(nameof(MultiCanvas.AddNewCanvas)).
                    MakeGenericMethod(info.Type).Invoke(this, new object[] { info.Parameters.LocalPosition, info.Parameters.LocalEulerAngles, info.Parameters.Scale });
            }
        }
    }

    public class MultiCanvasParameters : AbstractCustomizableParameter
    {
        public CanvasControllerInfo[] CanvasControllerInfos { get; set; }

        public MultiCanvasParameters (CanvasControllerInfo[] canvasControllerTypes) =>
           CanvasControllerInfos = canvasControllerTypes ?? throw new ArgumentNullException(nameof(canvasControllerTypes));
    }
}