---
title: Assets/Player/Scripts/FlyPlayer/FlyPlayer.cs


---

# Assets/Player/Scripts/FlyPlayer/FlyPlayer.cs







## Namespaces

| Name           |
| -------------- |
| **[PlayerInputControls](Namespaces/namespace_player_input_controls.md)**  |
| **[System::Collections::ObjectModel](Namespaces/namespace_system_1_1_collections_1_1_object_model.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[PlayerInputControls::FlyPlayer](Classes/class_player_input_controls_1_1_fly_player.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
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
using System.Collections.ObjectModel;
using System.Linq;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControls
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class FlyPlayer : AbstractPLayer, ICustomizable<PlayerParams>
    {
        private Transform _transform;
        private Vector3 _moveDirVector;
        private FlyControls _inputActions;
        private MoveComponent _moveComponent;
        private List<PlayerTool> _playerTools;
        private int _currentToolIndex = 0;
        private GameObject _hand;

        private void Awake ()
        {
            _transform = transform;

            _hand = new GameObject("Hand");
            _hand.transform.parent = _transform;
            _hand.transform.localPosition = new Vector3(2, -2, 0);
            _hand.AddComponent(typeof(LaserPointer));

            _playerTools = new List<PlayerTool>();

            _inputActions = new FlyControls();
            _moveComponent = GetComponent<MoveComponent>();
            _inputType = InputType.ToolsOnly;
        }

        private void CreateTool (params ToolConfig[] toolsConfig)
        {
            var clonedList = _playerTools.GetRange(0, _playerTools.Count);
            try
            {
                foreach (var config in toolsConfig)
                {
                    var newTool = ((PlayerTool) _hand.AddComponent(config.ToolType));
                    newTool.RegisterEvents(_inputActions);

                    try
                    {
                        CustomizableExtension.CallSetUpParams(newTool, new[] { config.ToolParams });
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning(e.Message);
                    }

                    _playerTools.Add(newTool);
                    _playerTools[_playerTools.Count - 1].enabled = false;
                }

                SelectTool(0);
            }
            catch
            {
                _playerTools = clonedList;
                throw;
            }
        }

        private void LateUpdate ()
        {
            _moveComponent.Rotate(_inputActions.FlyModel.LookRotation.ReadValue<Vector2>(), Time.deltaTime);
            _moveComponent.Translate(_moveDirVector, Time.deltaTime);
        }

        private void OnEnable ()
        {
            _inputActions.FlyModel.SelectItem.performed += OnSelectItem;

            _inputActions.FlyModel.Move.performed += OnPlayerMove;
            _inputActions.FlyModel.Move.canceled += OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed += OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed += OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled += OnPlayerChangeAltitude;

            _inputActions.FlyModel.Enable();

            UpdateInputs();
        }

        private void OnDisable ()
        {
            _inputActions.FlyModel.SelectItem.performed -= OnSelectItem;

            _inputActions.FlyModel.Move.performed -= OnPlayerMove;
            _inputActions.FlyModel.Move.canceled -= OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed -= OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed -= OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled -= OnPlayerChangeAltitude;

            _inputActions.FlyModel.Disable();
        }

        private void UpdateInputs ()
        {
            (bool tools, bool cursor, bool movement) state = (false, false, false);
            switch (_inputType)
            {
                case InputType.Off:
                    state = (false, false, false);
                    break;
                case InputType.MenuOnly:
                    state = (false, true, false);
                    break;
                case InputType.ToolsOnly:
                    state = (true, false, true);
                    break;
                case InputType.All:
                    state = (true, true, true);
                    break;
            }

            if (state.tools)
            {
                if (_playerTools.Count > 0)
                    _playerTools[_currentToolIndex].enabled = true;
            }
            else
            {
                foreach (var tool in _playerTools)
                    tool.enabled = false;
            }

            Cursor.visible = state.cursor;

            if (state.movement)
                _inputActions.Enable();
            else
                _inputActions.Disable();
        }

        public void OnPlayerMove (InputAction.CallbackContext obj)
        {
            var direction = obj.ReadValue<Vector2>();
            _moveDirVector = new Vector3(direction.x, _moveDirVector.y, direction.y);
        }

        public void OnPlayerChangeAltitude (InputAction.CallbackContext obj) => _moveDirVector.y = obj.ReadValue<float>();

        public void OnMoveToPoint (InputAction.CallbackContext obj)
        {
            if (Physics.Raycast(_hand.transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity))
                StartCoroutine(_moveComponent.MoveAlongTrajectory(new ReadOnlyCollection<Vector3>(new List<Vector3>(1) { hit.point })));
        }

        public void SelectTool (int index)
        {
            var lastIndex = _currentToolIndex;
            try
            {
                _playerTools[_currentToolIndex].enabled = false;
                _currentToolIndex = index;
                _playerTools[_currentToolIndex].enabled = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogError($"Wrong tool index {_currentToolIndex}");
                _currentToolIndex = lastIndex;
                _playerTools[_currentToolIndex].enabled = true;
            }
        }

        public void OnSelectItem (InputAction.CallbackContext obj) => SelectTool(Convert.ToInt32(obj.control.displayName) - 1);

        public void SetupParams (PlayerParams playerParams)
        {
            transform.position = playerParams.Position;
            transform.eulerAngles = playerParams.EulerAngles;
            _moveComponent.MovingSpeed = playerParams.MovingSpeed;
            _moveComponent.RotationSpeed = playerParams.RotationSpeed;
            CreateTool(playerParams.ToolConfigs);
        }

        //ToDo : Remove array from CustomizableExtension.CallDownloadParams
        public PlayerParams DownloadParams () =>
            new PlayerParams(transform.position, transform.eulerAngles, _moveComponent.MovingSpeed, _moveComponent.RotationSpeed,
            _playerTools.Select(tool => new ToolConfig(tool.GetType(), CustomizableExtension.CallDownloadParams(tool)[0])).ToArray());

        public override InputType InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;
                UpdateInputs();
            }
        }
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
