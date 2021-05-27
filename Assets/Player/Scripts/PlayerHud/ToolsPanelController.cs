using System;

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.Player.HUD
{
    public class ToolsPanelController : MonoBehaviour, IVisibile
    {
        [SerializeField]
        private GameObject _canvas;

        private bool _visibility = true;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        public bool Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                    return;

                _visibility = value;

                _canvas.SetActive(value);

                VisibleChanged?.Invoke(value, this);
            }
        }

        private void Start ()
        {
        }

        // Update is called once per frame
        private void Update ()
        {
        }
    }
}