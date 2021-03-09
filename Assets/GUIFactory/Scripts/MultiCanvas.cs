using System;
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.GUI;

using UnityEngine;
using UnityEngine.UI;

namespace Graph3DVisualizer.VrHud
{
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
            new MultiCanvasParameters(_canvasControllers.Select(c => new CanvasControllerInfo (c.GetType(), (CanvasControllerParameters) CustomizableExtension.CallDownloadParams(c))).ToArray());

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
        public Vector3 LocalPosition { get; set; }
        public Vector3 LocalEulerAngles { get; set; }
        public Vector3 Scale { get; set; }

        public CanvasControllerParameters (Vector3 localPosition, Vector3 localEulerAngles, Vector3 scale) => (LocalPosition, LocalEulerAngles, Scale) = (localPosition, localEulerAngles, scale);
    }

    public struct CanvasControllerInfo
    {
        public Type Type { get; set; }
        public CanvasControllerParameters Parameters { get; set; }

        public CanvasControllerInfo (Type type, CanvasControllerParameters parameters)
        {
            Type = type;
            Parameters = parameters;
        }

        public override bool Equals (object obj)
        {
            return obj is CanvasControllerInfo other &&
                   EqualityComparer<Type>.Default.Equals(Type, other.Type) &&
                   EqualityComparer<CanvasControllerParameters>.Default.Equals(Parameters, other.Parameters);
        }

        public override int GetHashCode ()
        {
            var hashCode = -1030903623;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<CanvasControllerParameters>.Default.GetHashCode(Parameters);
            return hashCode;
        }

        public void Deconstruct (out Type type, out CanvasControllerParameters parameters)
        {
            type = Type;
            parameters = Parameters;
        }

        public static implicit operator (Type, CanvasControllerParameters) (CanvasControllerInfo value)
        {
            return (value.Type, value.Parameters);
        }

        public static implicit operator CanvasControllerInfo ((Type type, CanvasControllerParameters parameters) value)
        {
            return new CanvasControllerInfo(value.type, value.parameters);
        }
    }
}
