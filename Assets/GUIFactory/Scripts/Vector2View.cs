using System;

using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace Graph3DVisualizer.Gui
{
    public class Vector2View : MonoBehaviour
    {
        [SerializeField]
        private Text _name;

        private Vector3 _vector;

        [SerializeField]
        private InputField _xInput;

        [SerializeField]
        private InputField _yInput;

        public event Action<Vector2>? OnChanged;

        public string Name { get => _name.text; set => _name.text = value; }

        public Vector2 Vector
        {
            get => _vector;
            set
            {
                _xInput.text = value.x.ToString();
                _yInput.text = value.y.ToString();
            }
        }

        private void Awake ()
        {
            _xInput.onValueChanged.AddListener(SetX);
            _yInput.onValueChanged.AddListener(SetY);
        }

        private void SetX (string value)
        {
            _vector.Set(Convert.ToSingle(value), _vector.y, _vector.z);
            OnChanged?.Invoke(_vector);
        }

        private void SetY (string value)
        {
            _vector.Set(_vector.x, Convert.ToSingle(value), _vector.z);
            OnChanged?.Invoke(_vector);
        }
    }
}