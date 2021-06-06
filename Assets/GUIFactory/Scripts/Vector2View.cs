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