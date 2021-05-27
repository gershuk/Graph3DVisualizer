using System;

using Graph3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.UI;

namespace Graph3DVisualizer.Player.HUD
{
    public class TaskPanelController : MonoBehaviour, IVisibile
    {
        [SerializeField]
        private GameObject _canvas;

        [SerializeField]
        private Text _text;

        private bool _visibility = true;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        public string Text { get => _text.text; set => _text.text = value; }

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
    }
}