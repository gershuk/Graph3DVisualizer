---
title: Assets/Player/Scripts/FlyPlayer/AbstractPlayer.cs

---

# Assets/Player/Scripts/FlyPlayer/AbstractPlayer.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::PlayerInputControls](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)** <br>Abstract class that describes player.  |
| class | **[Graph3DVisualizer::PlayerInputControls::PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md)** <br>A class that describes default player parameters for ICustomizable<TParams>.  |




## Source code

```cpp
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
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.PlayerInputControls
{
    [CustomizableGrandType(Type = typeof(PlayerParameters))]
    [RequireComponent(typeof(Camera))]
    public abstract class AbstractPlayer : MonoBehaviour, ICustomizable<PlayerParameters>
    {
        protected int _currentToolIndex = 0;
        protected InputType _inputType;
        protected MovementComponent _moveComponent;
        protected List<AbstractPlayerTool> _playerTools = new List<AbstractPlayerTool>();

        public event Action<AbstractPlayerTool> NewToolSelected;

        public int CurrentToolIndex { get => _currentToolIndex; set => SelectTool(value); }
        public IReadOnlyList<AbstractPlayerTool> GetToolsList => _playerTools;
        public abstract InputType InputType { get; set; }

        protected abstract void GiveNewTool (params ToolConfig[] toolsConfig);

        public PlayerParameters DownloadParams () =>
            new PlayerParameters(transform.position, transform.eulerAngles, _moveComponent.MovingSpeed, _moveComponent.RotationSpeed,
            _playerTools.Select(tool => new ToolConfig(tool.GetType(), (AbstractToolParams) CustomizableExtension.CallDownloadParams(tool))).ToArray());

        public void SelectTool (int index)
        {
            var lastIndex = _currentToolIndex;
            try
            {
                _playerTools[_currentToolIndex].enabled = false;
                _currentToolIndex = index;
                _playerTools[_currentToolIndex].enabled = true;

                NewToolSelected?.Invoke(_playerTools[_currentToolIndex]);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogError($"Wrong tool index {_currentToolIndex}");
                _currentToolIndex = lastIndex;
                _playerTools[_currentToolIndex].enabled = true;
            }
        }

        public void SetupParams (PlayerParameters playerParams)
        {
            transform.position = playerParams.Position;
            _moveComponent.MovingSpeed = playerParams.MovingSpeed;
            _moveComponent.RotationSpeed = playerParams.RotationSpeed;
            _moveComponent.Rotation = playerParams.EulerAngles;
            GiveNewTool(playerParams.ToolConfigs);
        }
    }

    [Serializable]
    public class PlayerParameters : AbstractCustomizableParameter
    {
        public Vector3 EulerAngles { get; }
        public float MovingSpeed { get; }
        public Vector3 Position { get; }
        public float RotationSpeed { get; }
        public ToolConfig[] ToolConfigs { get; }

        public PlayerParameters (Vector3 position, Vector3 eulerAngles, float movingSpeed, float rotationSpeed, ToolConfig[] toolConfigs)
        {
            Position = position;
            EulerAngles = eulerAngles;
            RotationSpeed = rotationSpeed;
            MovingSpeed = movingSpeed;
            ToolConfigs = toolConfigs ?? throw new ArgumentNullException(nameof(toolConfigs));
        }
    }

    public enum InputType
    {
        Off = 0,
        MenuOnly = 1,
        ToolsOnly = 2,
        All = 3,
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (����)
