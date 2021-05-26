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

#nullable enable

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;
using UnityEngine.XR.Interaction.Toolkit;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Simple realization of <see cref="AbstractPlayer"/> for keyboard/mouse controls.
    /// </summary>
    [RequireComponent(typeof(MovementComponent))]
    public sealed class FlyPlayer : AbstractPlayer
    {
        [SerializeField]
        private GameObject _head;

        [SerializeField]
        private FlyControls _inputActions;

        private bool _isVr = true;

        [SerializeField]
        private UnityEngine.XR.Interaction.Toolkit.XRController _leftController;

        [SerializeField]
        private GameObject _leftHand;

        [SerializeField]
        private XRRayInteractor _leftRayInteractor;

        private Vector3 _moveDirVector;

        [SerializeField]
        private XRRig _rig;

        [SerializeField]
        private UnityEngine.XR.Interaction.Toolkit.XRController _rightController;

        [SerializeField]
        private GameObject _rightHand;

        [SerializeField]
        private XRRayInteractor _rightRayInteractor;

        [SerializeField]
        private TrackedPoseDriver _trackedPoseDriver;

        public bool IsVr
        {
            get => _isVr;
            set
            {
                if (_isVr == value)
                    return;

                _isVr = value;
                _rig.enabled = _isVr;
                _trackedPoseDriver.enabled = _isVr;
                _leftController.enabled = _isVr;
                _rightController.enabled = _isVr;
                _leftRayInteractor.enabled = _isVr;
                _rightRayInteractor.enabled = _isVr;
            }
        }

        private void Awake ()
        {
            //Controls is a resource, so it can't be loaded from the constructor.
            _inputActions = new FlyControls();
            _moveComponent = GetComponent<MovementComponent>();

            CursorEnable = false;
            MovementEnable = true;
            ToolsEnable = true;

            IsVr = false;
        }

        private void LateUpdate ()
        {
            if (MovementEnable)
            {
                _moveComponent.Rotate(_inputActions.FlyModel.LookRotation.ReadValue<Vector2>(), Time.deltaTime);
                _moveComponent.Translate(_moveDirVector, Time.deltaTime, _head.transform);
            }
        }

        private void OnChangeInputType (InputAction.CallbackContext obj) => (CursorEnable, ToolsEnable, MovementEnable) = (!CursorEnable, !ToolsEnable, !MovementEnable);

        private void OnDisable ()
        {
            _inputActions.FlyModel.SelectItem.performed -= OnSelectItem;

            _inputActions.FlyModel.Move.performed -= OnPlayerMove;
            _inputActions.FlyModel.Move.canceled -= OnPlayerMove;

            _inputActions.FlyModel.MoveFromTrackPad.performed -= OnPlayerMove;
            _inputActions.FlyModel.MoveFromTrackPad.canceled -= OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed -= OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed -= OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled -= OnPlayerChangeAltitude;

            _inputActions.FlyModel.ChangeInputType.performed -= OnChangeInputType;

            _inputActions.FlyModel.Disable();
        }

        private void OnEnable ()
        {
            _inputActions.FlyModel.SelectItem.performed += OnSelectItem;

            _inputActions.FlyModel.Move.performed += OnPlayerMove;
            _inputActions.FlyModel.Move.canceled += OnPlayerMove;

            _inputActions.FlyModel.MoveFromTrackPad.performed += OnPlayerMove;
            _inputActions.FlyModel.MoveFromTrackPad.canceled += OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed += OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed += OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled += OnPlayerChangeAltitude;

            _inputActions.FlyModel.ChangeInputType.performed += OnChangeInputType;

            _inputActions.FlyModel.Enable();
        }

        protected override void GiveNewTool (params ToolConfig[] toolsConfig)
        {
            if (toolsConfig.Length == 0)
                return;

            var clonedList = _playerTools.GetRange(0, _playerTools.Count);
            try
            {
                foreach (var config in toolsConfig)
                {
                    var newTool = ((AbstractPlayerTool) _rightHand.AddComponent(config.ToolType));
                    newTool.RegisterEvents(_inputActions);

                    try
                    {
                        if (config.ToolParams != null)
                            CustomizableExtension.CallSetUpParams(newTool, config.ToolParams);
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

        public void OnMoveToPoint (InputAction.CallbackContext obj)
        {
            if (Physics.Raycast(_leftHand.transform.position, _leftHand.transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity) && MovementEnable)
                StartCoroutine(_moveComponent.MoveAlongTrajectory(new List<Vector3>(1) { hit.point }));
        }

        public void OnPlayerChangeAltitude (InputAction.CallbackContext obj) => _moveDirVector.y = obj.ReadValue<float>();

        public void OnPlayerMove (InputAction.CallbackContext obj)
        {
            var direction = obj.ReadValue<Vector2>().normalized;
            _moveDirVector = new Vector3(direction.x, _moveDirVector.y, direction.y);
        }

        public void OnSelectItem (InputAction.CallbackContext obj) => SelectTool(Convert.ToInt32(obj.control.displayName) - 1);
    }
}