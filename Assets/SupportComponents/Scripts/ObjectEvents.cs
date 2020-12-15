// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

using UnityEngine;

namespace SupportComponents
{
    public interface IMoveable
    {
        float MovingSpeed { get; set; }
        float RotationSpeed { get; set; }
        event Action<Vector3, UnityEngine.Object> ObjectMoved;
        Vector3 GlobalCoordinates { get; set; }
        Vector3 LocalCoordinates { get; set; }
        void Translate (Vector3 moveVector, float deltaTime);
        void Rotate (Vector2 rotationChange, float deltaTime);
        IEnumerator MoveAlongTrajectory (ReadOnlyCollection<Vector3> trajectory);
    }

    public interface IDestructible
    {
        event Action<UnityEngine.Object> Destroyed;
    }

    public interface IVisibile
    {
        bool Visibility { get; set; }
        event Action<bool, UnityEngine.Object> VisibleChanged;
    }

    public interface ISelectable
    {
        bool IsSelected { get; set; }
        bool IsHighlighted { get; set; }
        event Action<UnityEngine.Object, bool> SelectedChanged;
        event Action<UnityEngine.Object, bool> HighlightedChanged;
        Color SelectFrameColor { get; set; }
    }

    public abstract class CustomizableParameter { };

    public interface ICustomizable<TParams> where TParams : CustomizableParameter
    {
        void SetupParams (TParams parameters);
        TParams DownloadParams ();
    }

    public static class CustomizableExtension
    {
        public static void CallSetUpParams (object customizable, object[] parameters)
        {
            foreach (var param in parameters)
            {
                var isFinded = false;

                foreach (var interfaceType in customizable.GetType().GetInterfaces())
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>) && interfaceType.GetGenericArguments()[0] == param.GetType())
                    {
                        interfaceType.GetMethod("SetupParams", interfaceType.GetGenericArguments()).Invoke(customizable, new[] { param });
                        isFinded = true;
                    }
                }

                if (!isFinded)
                    throw new Exception($"Customizable methods with parameter type {param.GetType()} not found");
            }
        }

        public static List<T> CallDownloadParams<T> (object customizable) where T : CustomizableParameter
        {
            var parameters = new List<T>();
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>)
                    && (interfaceType.GetGenericArguments()[0].IsSubclassOf(typeof(T)) || interfaceType.GetGenericArguments()[0] == typeof(T)))
                {
                    parameters.Add((T) interfaceType.GetMethod("DownloadParams").Invoke(customizable, null));
                }
            }
            return parameters;
        }
    }

    public class WrongTypeInCustomizableParameterException : Exception
    {
        protected WrongTypeInCustomizableParameterException (SerializationInfo info, StreamingContext context) : base(info, context) { }

        public WrongTypeInCustomizableParameterException () { }

        public WrongTypeInCustomizableParameterException (string message) : base(message) { }

        public WrongTypeInCustomizableParameterException (string message, Exception innerException) : base(message, innerException) { }

        public WrongTypeInCustomizableParameterException (Type expectedType, Type receivedType) : base($"The type inherited from {expectedType.Name} was expected, and {receivedType.Name} was obtained.") { }
    }
}
