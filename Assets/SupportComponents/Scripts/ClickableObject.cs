using System;

using UnityEngine;

namespace SupportComponents
{
    public abstract class ClickableObject : MonoBehaviour
    {
        protected Transform _transform;
        protected GameObject _gameObject;

        protected abstract void ClickAction (GameObject gameObject);

        public event Action<GameObject> Clicked;
        public void Click (GameObject gameObject)
        {
            ClickAction(gameObject);
            Clicked?.Invoke(gameObject);
        }
        public abstract void SetDisabled ();
    }
}
