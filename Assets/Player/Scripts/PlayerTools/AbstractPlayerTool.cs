// This file is part of Graph3DVisualizer.
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

using Graph3DVisualizer.Customizable;

using UnityEngine;
using UnityEngine.InputSystem;

using Yuzu;

using static UnityEngine.Physics;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Сlass that describes the data needed when creating a new player tool. <seealso cref="AbstractPlayer.GiveNewTool(ToolConfig[])"/>
    /// </summary>
    [Serializable]
    [YuzuAll]
    public struct ToolConfig
    {
        public ToolParams ToolParams { get; set; }
        public Type ToolType { get; set; }

        public ToolConfig (Type toolType, ToolParams toolParams)
        {
            if (!toolType.IsSubclassOf(typeof(AbstractPlayerTool)))
                throw new Exception($"{toolType} is not subclass of PlayerTool");
            ToolType = toolType ?? throw new ArgumentNullException(nameof(toolType));
            ToolParams = toolParams;
        }
    }

    /// <summary>
    /// Abstract class that describes the player's tool for interacting with the world.
    /// </summary>
    public abstract class AbstractPlayerTool : MonoBehaviour, ICustomizable<ToolParams>
    {
        private bool _enable = true;

        private bool _isVR = false;

        [SerializeField]
        private float _rayCastRange = 1000;

        #region Input PC
        protected const string _inputActionMapPCName = "InputActionMapPC";
        protected InputActionMap _inputActionsPC;
        #endregion Input PC

        #region Input VR
        protected const string _inputActionMapVRName = "InputActionMapVR";
        protected InputActionMap _inputActionsVR;
        #endregion Input VR

        public virtual bool Enable
        {
            get => _enable;
            set
            {
                _enable = value;
                UpdateInput();
            }
        }

        public virtual bool IsVR
        {
            get => _isVR;
            set
            {
                _isVR = value;
                UpdateInput();
            }
        }

        public float RayCastRange { get => _rayCastRange; set => _rayCastRange = value; }

        protected void OnDisable () => Enable = false;

        protected void OnEnable () => Enable = true;

        protected RaycastHit RayCast ()
        {
            Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, RayCastRange);
            return hit;
        }

        protected void UpdateInput ()
        {
            if (_enable && _isVR)
                _inputActionsVR?.Enable();
            else
                _inputActionsVR?.Disable();

            if (_enable && !_isVR)
                _inputActionsPC?.Enable();
            else
                _inputActionsPC?.Disable();
        }

        public ToolParams DownloadParams (Dictionary<Guid, object> writeCache) => new(IsVR);

        AbstractCustomizableParameter ICustomizable.DownloadParams (Dictionary<Guid, object> writeCache) => throw new NotImplementedException();

        public virtual void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActionsPC = new(_inputActionMapPCName);
            _inputActionsVR = new(_inputActionMapVRName);
        }

        public void SetupParams (ToolParams parameters) => IsVR = parameters.IsVR;

        public void SetupParams (object parameters) => throw new NotImplementedException();

        public void SetupParams (AbstractCustomizableParameter parameters) => throw new NotImplementedException();
    }

    /// <summary>
    /// A class that describes default player's tool parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class ToolParams : AbstractCustomizableParameter
    {
        public bool IsVR { get; protected set; }
        public float RayCastRange { get; protected set; }

        public ToolParams (bool isVR = false, float rayCastRange = 1000, Guid? parameterId = default) :
            base(parameterId) => (IsVR, RayCastRange) = (isVR, rayCastRange);
    }
}