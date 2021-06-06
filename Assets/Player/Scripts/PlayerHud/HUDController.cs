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
using System.Collections.Generic;
using System.Text;

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.Player.HUD
{
    public class HUDController : MonoBehaviour, IVisibile
    {
        [SerializeField]
        private float _dist;

        [SerializeField]
        private GameObject _head;

        [SerializeField]
        private GameObject _menuExit;

        [SerializeField]
        private ObjectInfoPanelController _objectInfoPanel;

        [SerializeField]
        private TaskPanelController _taskPanel;

        [SerializeField]
        private ToolsPanelController _toolsPanel;

        private Transform _transform;
        private bool _visibility = true;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        public float Dist { get => _dist; set => _dist = value; }

        public Func<List<Verdict>>? GetResultFromTask { get; set; }
        public Action GoPrevScene { get; set; }

        public string SceneInfo
        {
            get => _taskPanel.Text;
            set => _taskPanel.Text = value;
        }

        public bool Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                    return;

                _visibility = value;

                _objectInfoPanel.Visibility = value;
                _taskPanel.Visibility = value;
                _toolsPanel.Visibility = value;
                _menuExit.SetActive(value);

                VisibleChanged?.Invoke(value, this);

                transform.position = _head.transform.position + _head.transform.forward * Dist;
            }
        }

        private void Awake ()
        {
            _transform = transform;
            _transform.position = _head.transform.position + _head.transform.forward * Dist;
        }

        private void LateUpdate ()
        {
            const float eps = 1.2f;
            var targetPoint = _head.transform.position + _head.transform.forward * Dist;

            if (Vector3.Distance(transform.position, targetPoint) > eps)
                _transform.position = Vector3.Lerp(_transform.position, targetPoint, Time.deltaTime);

            transform.LookAt(_head.transform, Vector3.up);
        }

        public void GetResult ()
        {
            if (GetResultFromTask != null)
            {
                var stringBuilder = new StringBuilder();
                foreach (var verdict in GetResultFromTask())
                {
                    stringBuilder.Append($"{ verdict}{Environment.NewLine}");
                }
                _objectInfoPanel.Text = stringBuilder.ToString();
            }
            else
            {
                _objectInfoPanel.Text = "Tasks not found";
            }
        }

        public void GoBack () => GoPrevScene();
    }
}