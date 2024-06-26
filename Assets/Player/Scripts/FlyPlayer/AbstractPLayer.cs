﻿// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.Player.HUD;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Abstract class that describes player.
    /// </summary>
    [CustomizableGrandType(typeof(PlayerParameters))]
    public abstract class AbstractPlayer : MonoBehaviour, ICustomizable<PlayerParameters>
    {
        protected int _currentToolIndex = 0;
        protected bool _cursorEnable;
        protected MovementComponent _moveComponent;
        protected List<AbstractPlayerTool> _playerTools = new();
        protected bool _toolsEnable;

        public event Action<AbstractPlayerTool>? NewToolSelected;

        protected bool CursorEnable
        {
            get => _cursorEnable;
            set
            {
                if (_cursorEnable == value)
                    return;

                _cursorEnable = value;
                Cursor.visible = _cursorEnable;
            }
        }

        protected virtual bool MovementEnable { get; set; }

        protected virtual bool ToolsEnable
        {
            get => _toolsEnable;
            set
            {
                if (_toolsEnable == value)
                    return;

                _toolsEnable = value;

                if (_playerTools.Count == 0)
                    return;

                if (value)
                {
                    if (_playerTools.Count > 0)
                        _playerTools[_currentToolIndex].enabled = true;
                }
                else
                {
                    foreach (var tool in _playerTools)
                        tool.enabled = false;
                }
            }
        }

        public int CurrentToolIndex { get => _currentToolIndex; set => SelectTool(value); }
        public IReadOnlyList<AbstractPlayerTool> GetToolsList => _playerTools;
        public abstract HUDController HUDController { get; }
        public abstract bool IsVr { get; set; }
        public abstract string SceneInfo { get; set; }

        protected abstract void GiveNewTool (params ToolConfig[] toolsConfig);

        public PlayerParameters DownloadParams (Dictionary<Guid, object> writeCache)
        {
            PlayerParameters playerParameters = new(transform.position,
                                                    transform.eulerAngles,
                                                    _moveComponent.MovingSpeed,
                                                    _moveComponent.RotationSpeed,
                                                    IsVr,
                                                    _playerTools.Select(tool =>
                                                    new ToolConfig(tool.GetType(),
                                                                   (ToolParams) CustomizableExtension.CallDownloadParams(tool, writeCache)))
                                                                                                     .ToArray(),
                             sceneInfo: SceneInfo);
            return playerParameters;
        }

        AbstractCustomizableParameter ICustomizable.DownloadParams (Dictionary<Guid, object> writeCache) => throw new NotImplementedException();

        public void SelectTool (int index)
        {
            if (!ToolsEnable)
                return;

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
            _moveComponent.GlobalEulerAngles = playerParams.EulerAngles;
            SceneInfo = playerParams.SceneInfo;
            GiveNewTool(playerParams.ToolConfigs);
            IsVr = playerParams.IsVr;
        }

        public void SetupParams (object parameters) => throw new NotImplementedException();

        public void SetupParams (AbstractCustomizableParameter parameters) => throw new NotImplementedException();
    }

    /// <summary>
    /// A class that describes default player parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class PlayerParameters : AbstractCustomizableParameter
    {
        public Vector3 EulerAngles { get; protected set; }
        public bool IsVr { get; protected set; }
        public float MovingSpeed { get; protected set; }
        public Vector3 Position { get; protected set; }
        public float RotationSpeed { get; protected set; }
        public string SceneInfo { get; protected set; }
        public ToolConfig[] ToolConfigs { get; protected set; }

        public PlayerParameters (Vector3 position = default,
                                 Vector3 eulerAngles = default,
                                 float movingSpeed = 10,
                                 float rotationSpeed = 10,
                                 bool isVR = false,
                                 ToolConfig[]? toolConfigs = default,
                                 Guid? parameterId = default,
                                 string sceneInfo = "No Info") : base(parameterId)
        {
            Position = position;
            EulerAngles = eulerAngles;
            RotationSpeed = rotationSpeed;
            MovingSpeed = movingSpeed;
            IsVr = isVR;
            ToolConfigs = toolConfigs ?? new ToolConfig[0];
            SceneInfo = sceneInfo;
        }
    }

    /// <summary>
    /// Player input type.
    /// </summary>
    public enum InputType
    {
        Off = 0,
        MenuOnly = 1,
        ToolsOnly = 2,
        All = 3,
    }
}