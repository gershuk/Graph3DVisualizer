---
title: Assets/Player/Scripts/PlayerTools/SelectItemTool.cs

---

# Assets/Player/Scripts/PlayerTools/SelectItemTool.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::PlayerInputControls](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::PlayerInputControls::SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md)** <br>The tool allows you to select objects with components that implement the ISelectable.  |
| class | **[Graph3DVisualizer::PlayerInputControls::SelectItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool_params.md)** <br>Class that describes [SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md) parameters for ICustomizable<TParams>.  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright В© Gershuk Vladislav 2021.
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

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Graph3DVisualizer.PlayerInputControls
{
    [RequireComponent(typeof(LaserPointer))]
    [CustomizableGrandType(Type = typeof(SelectItemToolParams))]
    public class SelectItemTool : AbstractPlayerTool, ICustomizable<SelectItemToolParams>
    {
        private const string _changeColorActionName = "ChangeColorAction";
        private const string _inputActionName = "SelectItemActionMap";
        private const string _selectActionName = "SelectItemAction";
        private int _colorIndex;
        private IReadOnlyList<Color> _colors;
        private InputActionMap _inputActions;

        private LaserPointer _laserPointer;

        [SerializeField]
        private float _rayCastRange = 1000;

        public float RayCastRange { get => _rayCastRange; set => _rayCastRange = value; }

        private void Awake ()
        {
            _laserPointer = GetComponent<LaserPointer>();
            _colorIndex = 0;
        }

        private void CallChangeColor (InputAction.CallbackContext obj) => ChangeColor(Mathf.RoundToInt(obj.ReadValue<float>()));

        private void CallSelectItem (InputAction.CallbackContext obj) => SelectItem();

        private void OnDisable ()
        {
            _inputActions?.Disable();
            _laserPointer.LaserState = LaserState.Off;
        }

        private void OnEnable ()
        {
            _inputActions?.Enable();
            _laserPointer.LaserState = LaserState.On;
            _laserPointer.Range = _rayCastRange;
        }

        public void ChangeColor (int deltaIndex) => _colorIndex = (_colorIndex + deltaIndex) < 0 ? _colors.Count - 1 : (_colorIndex + deltaIndex) % _colors.Count;

        public SelectItemToolParams DownloadParams () => new SelectItemToolParams(_colors);

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_inputActionName);
            var selectItemAction = _inputActions.AddAction(_selectActionName, InputActionType.Button, "<Mouse>/leftButton");
            var changeColorAction = _inputActions.AddAction(_changeColorActionName, InputActionType.Button);
            changeColorAction.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            selectItemAction.canceled += CallSelectItem;
            changeColorAction.performed += CallChangeColor;
        }

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

        public void SetupParams (SelectItemToolParams parameters) => _colors = parameters.Colors;
    }


    [Serializable]
    public class SelectItemToolParams : AbstractToolParams
    {
        public IReadOnlyList<Color> Colors { get; }

        public SelectItemToolParams (IReadOnlyList<Color> colors) => Colors = colors ?? throw new ArgumentNullException(nameof(colors));
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
