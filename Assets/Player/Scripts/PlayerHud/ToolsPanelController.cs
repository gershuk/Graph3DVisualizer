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

using Graph3DVisualizer.Gui;
using UnityEngine.Events;
using UnityEngine.UI;
using Graph3DVisualizer.TextureFactory;
using System.Collections.Generic;

namespace Graph3DVisualizer.Player.HUD
{
    public class ToolsPanelController : MonoBehaviour, IVisibile
    {
        [SerializeField]
        private GameObject _canvas;

        private bool _visibility = true;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        [SerializeField]
        private GameObject _content;
        private List<GameObject> _buttonList = new List<GameObject>();

        private float _buttonPisitionX = -95;
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
        public void ClearAll ()
        {
            foreach (var button in _buttonList)
                Destroy(button);
            _buttonList.Clear();
            _buttonPisitionX = -195;
        }

        public void AddTool (Image image, UnityAction action, string text)
        {

            var rectTransformParameters = new GUIFactory.RectTransformParameters(_content.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(5f, 5f), new Vector2(2f, 2f), new Vector3(_buttonPisitionX , 0, 0), new Vector3(372.34f, 180f, 360f),new Vector3(20,19.5f,0));
            var buttonParameters = new GUIFactory.ButtonParameters(action, text, rectTransformParameters, image);
            var button = GUIFactory.CreateButton(buttonParameters);
            var rectTransfotmParametersText = new GUIFactory.RectTransformParameters(button.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(100f, 50f), new Vector2(0, 0),new Vector3 (0, 0, 0), new Vector3(12.34f, 180f, 0f), new Vector3(0.05f, 0.05f, 0));

            var font = FontsGenerator.GetOrCreateFont("STHupo", 25);

            var textParameters = new GUIFactory.TextParameters(text, Color.white, font, TextAnchor.MiddleCenter, 20, rectTransfotmParametersText);

            _buttonList.Add(button);

            GUIFactory.CreateText(textParameters);

            _buttonPisitionX += 103;
        }
    }
}