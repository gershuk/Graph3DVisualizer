using UnityEngine;
using System;

namespace Grpah3DVisualser
{
    public interface IMove
    {
        event Action<Vector3,UnityEngine.Object> OnMove;
        void SetGlobalCoordinates (Vector3 coordinates);
        void SetLocalCoordinates (Vector3 coordinates);
    }

    public interface IDestoryed
    {
        event Action<UnityEngine.Object> OnDestroyed;
    }

    public interface IVisibile
    {
        void SetVisibility (bool state);
        bool GetVisibility ();
        event Action<bool,UnityEngine.Object> OnVisibleChange;
    }
}
