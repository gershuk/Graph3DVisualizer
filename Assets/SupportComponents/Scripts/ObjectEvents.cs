using System;
using System.Collections;
using System.Collections.ObjectModel;

using UnityEngine;

namespace SupportComponents
{
    public interface IMoveable
    {
        float Speed { get; set; }
        float RotationSpeed { get; set; }
        event Action<Vector3, UnityEngine.Object> OnObjectMove;
        Vector3 GlobalCoordinates { get; set; }
        Vector3 LocalCoordinates { get; set; }
        void Translate (Vector3 moveVector, float deltaTime);
        void Rotate (Vector2 rotationChange, float deltaTime);
        IEnumerator MoveAlongTrajectory (ReadOnlyCollection<Vector3> trajectory);
    }

    public interface IDestructible
    {
        event Action<UnityEngine.Object> OnDestroyed;
    }

    public interface IVisibile
    {
        bool Visibility { get; set; }
        event Action<bool, UnityEngine.Object> OnVisibleChange;
    }

    public interface ISelectable
    {
        bool IsSelected { get; set; }
        bool IsHighlighted { get; set; }
        event Action<UnityEngine.Object, bool> OnSelectedChange;
        event Action<UnityEngine.Object, bool> OnHighlightedChange;
    }
}
