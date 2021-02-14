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
using System.Linq;

using Grpah3DVisualizer.Customizable;
using Grpah3DVisualizer.SupportComponents;

using UnityEngine;

namespace Grpah3DVisualizer.PlayerInputControls
{
    public enum InputType
    {
        Off = 0,
        MenuOnly = 1,
        ToolsOnly = 2,
        All = 3,
    }

    public class PlayerParameters : CustomizableParameter
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        public float MovingSpeed { get; }
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

    public abstract class AbstractPlayer : MonoBehaviour, ICustomizable<PlayerParameters>
    {
        protected InputType _inputType;
        protected MovementComponent _moveComponent;
        protected List<PlayerTool> _playerTools = new List<PlayerTool>();
        protected int _currentToolIndex = 0;

        public event Action<PlayerTool> NewToolSelected;

        public abstract InputType InputType { get; set; }
        public int CurrentToolIndex { get => _currentToolIndex; set => SelectTool(value); }

        protected abstract void CreateTool (params ToolConfig[] toolsConfig);

        public IReadOnlyList<PlayerTool> GetToolsList => _playerTools;

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
            transform.eulerAngles = playerParams.EulerAngles;
            _moveComponent.MovingSpeed = playerParams.MovingSpeed;
            _moveComponent.RotationSpeed = playerParams.RotationSpeed;
            CreateTool(playerParams.ToolConfigs);
        }

        public PlayerParameters DownloadParams () =>
            new PlayerParameters(transform.position, transform.eulerAngles, _moveComponent.MovingSpeed, _moveComponent.RotationSpeed,
            _playerTools.Select(tool => new ToolConfig(tool.GetType(), CustomizableExtension.CallDownloadParams<ToolParams>(tool).ToArray())).ToArray());
    }
}
