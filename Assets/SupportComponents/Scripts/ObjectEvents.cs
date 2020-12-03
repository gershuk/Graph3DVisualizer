using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public interface ICustomizable<TParams>
    {
        void SetupParams (TParams parameters);
        TParams DownloadParams ();
    }

    public static class CustomizableExtension
    {
        //public static void SetupParams<T> (this ICustomizable<T> customizable, object parameters)
        //{
        //    if (parameters is T setupParams)
        //        customizable.SetupParams(setupParams);
        //    else
        //        throw new InvalidCastException();
        //}

        public static void CallSetUpParams (object customizable, object[] parameters)
        {
            var isFinded = false;
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>))
                {
                    interfaceType.GetMethod("SetupParams", interfaceType.GetGenericArguments()).Invoke(customizable, parameters);
                    isFinded = true;
                }
            }

            if (!isFinded)
                throw new Exception("Customizable methods not found");
        }

        public static List<object> CallDownloadParams (object customizable)
        {
            var parameters = new List<object>();
            foreach (var interfaceType in customizable.GetType().GetInterfaces())
            {
                if (interfaceType.GetGenericTypeDefinition() == typeof(ICustomizable<>))
                {
                    parameters.Add(interfaceType.GetMethod("DownloadParams").Invoke(customizable,null));
                }
            }
            return parameters;
        }
    }
}
