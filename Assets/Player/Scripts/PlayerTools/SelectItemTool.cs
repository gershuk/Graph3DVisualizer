// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

using Grpah3DVisualizer.Customizable;
using Grpah3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Grpah3DVisualizer.PlayerInputControls
{
    public class SelectItemToolParams : ToolParams
    {
        public IReadOnlyList<Color> Colors { get; }

        public SelectItemToolParams (IReadOnlyList<Color> colors) => Colors = colors ?? throw new ArgumentNullException(nameof(colors));
    }

    [RequireComponent(typeof(LaserPointer))]
    public class SelectItemTool : PlayerTool, ICustomizable<SelectItemToolParams>
    {
        private IReadOnlyList<Color> _colors;

        private const string _inputActionName = "SelectItemActionMap";
        private const string _selectActionName = "SelectItemAction";
        private const string _changeColorActionName = "ChangeColorAction";

        [SerializeField]
        private float _rayCastRange = 1000;

        private int _colorIndex;
        private InputActionMap _inputActions;
        private LaserPointer _laserPointer;

        public float RayCastRange { get => _rayCastRange; set => _rayCastRange = value; }

        private void Awake ()
        {
            _laserPointer = GetComponent<LaserPointer>();
            _colorIndex = 0;
        }

        private void OnEnable ()
        {
            _inputActions?.Enable();
            _laserPointer.LaserState = LaserState.On;
            _laserPointer.Range = _rayCastRange;
        }

        private void OnDisable ()
        {
            _inputActions?.Disable();
            _laserPointer.LaserState = LaserState.Off;
        }

        private void CallSelectItem (InputAction.CallbackContext obj) => SelectItem();
        private void CallChangeColor (InputAction.CallbackContext obj) => ChangeColor(Mathf.RoundToInt(obj.ReadValue<float>()));

        public void SelectItem ()
        {
            var selectableComponent = RayCast(_rayCastRange).transform?.GetComponent<ISelectable>();
            if (selectableComponent != null)
            {
                if (selectableComponent.IsSelected)
                {
                    if (selectableComponent.SelectFrameColor == _colors[_colorIndex])
                    {
                        selectableComponent.IsSelected = false;
                    }
                    else
                    {
                        selectableComponent.SelectFrameColor = _colors[_colorIndex];
                    }
                }
                else
                {
                    selectableComponent.IsSelected = true;
                    selectableComponent.SelectFrameColor = _colors[_colorIndex];
                }
            }
        }

        public void ChangeColor (int deltaIndex) => _colorIndex = (_colorIndex + deltaIndex) < 0 ? _colors.Count - 1 : (_colorIndex + deltaIndex) % _colors.Count;

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_inputActionName);
            var selectItemAction = _inputActions.AddAction(_selectActionName, InputActionType.Button, "<Mouse>/leftButton");
            var changeColorAction = _inputActions.AddAction(_changeColorActionName, InputActionType.Button);
            changeColorAction.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            selectItemAction.canceled += CallSelectItem;
            changeColorAction.performed += CallChangeColor;
        }

        public void SetupParams (SelectItemToolParams parameters) => _colors = parameters.Colors;

        public SelectItemToolParams DownloadParams () => new SelectItemToolParams(_colors);
    }
}
