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