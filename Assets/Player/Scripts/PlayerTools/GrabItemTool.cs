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
using System.Collections;
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

using Yuzu;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Tool for dragging objects with <see cref="MovementComponent"/>.
    /// </summary>
    [CustomizableGrandType(typeof(GrabItemToolParams))]
    public sealed class GrabItemTool : AbstractPlayerTool, ICustomizable<GrabItemToolParams>
    {
        #region Input names PC
        private const string _changeRangeActionPCName = "ChangeRangeActionPC";
        private const string _grabActionPCName = "GrabItemActionPC";
        #endregion Input names PC

        #region Input names VR
        private const string _changeRangeActionVRName = "ChangeRangeActionVR";
        private const string _grabActionVRName = "GrabItemActionVR";
        #endregion Input names VR

        [SerializeField]
        private float _capturedRange = 1000;

        private Coroutine? _changeRangeCoroutine;

        private bool _isCapturedObject = false;

        private bool _isChangingRange;

        private IMoveable? _moveable;

        [SerializeField]
        private float _rangeChangeSpeed = 30;

        [SerializeField]
        private float _rayCastRange = 1000;

        private Transform _transform;
        public float CapturedRange { get => _capturedRange; set => _capturedRange = value; }
        public float RangeChangeSpeed { get => _rangeChangeSpeed; set => _rangeChangeSpeed = value; }

        private void Awake ()
        {
            _transform = transform;
            _moveable = null;
            _isCapturedObject = false;

            _isChangingRange = false;
        }

        private void CallFreeItem (InputAction.CallbackContext obj) => FreeItem();

        private void CallGrabItem (InputAction.CallbackContext obj) => GrabItem();

        private void CallStartChangingRange (InputAction.CallbackContext obj) => StartChangingRange();

        private void CallStopChangingRange (InputAction.CallbackContext obj) => StopChangingRange();

        private IEnumerator ChangingRangeCoroutine ()
        {
            var changeRangeAction = _inputActionsPC.FindAction(_changeRangeActionPCName);
            while (_isChangingRange)
            {
                ChangeRange(changeRangeAction.ReadValue<float>());
                yield return null;
            }

            yield return null;
        }

        private void LateUpdate ()
        {
            if (_isCapturedObject && _moveable != null)
            {
                _moveable.GlobalCoordinates = _transform.position + _transform.TransformDirection(new Vector3(0, 0, _capturedRange));
            }
        }

        private void OnDisable ()
        {
            if (_changeRangeCoroutine != null)
                StopChangingRange();
            _inputActionsPC?.Disable();
            FreeItem();
        }

        private void OnEnable ()
        {
            _inputActionsPC?.Enable();
        }

        public void ChangeRange (float normalizedDelta) => _capturedRange = Mathf.Max(0, _capturedRange + normalizedDelta * Time.deltaTime * RangeChangeSpeed);

        public GrabItemToolParams DownloadParams (Dictionary<Guid, object> writeCache)
        {
            Debug.LogWarning($"GrabItemTool.DownloadParams not implemented");
            return new GrabItemToolParams();
        }

        public void FreeItem ()
        {
            _moveable = null;
            _isCapturedObject = false;
        }

        public void GrabItem ()
        {
            if (!_isCapturedObject)
            {
                var hit = RayCast(_rayCastRange);

                if (hit.transform != null)
                {
                    _moveable = hit.transform.GetComponent<IMoveable>();
                    _capturedRange = Vector3.Distance(hit.transform.position, _transform.position);
                    _isCapturedObject = true;
                }
            }
        }

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            base.RegisterEvents(inputActions);

            #region Bind PC input
            var grabItemActionPC = _inputActionsPC.AddAction(_grabActionPCName, InputActionType.Button, "<Mouse>/leftButton");
            var changeRangeActionPC = _inputActionsPC.AddAction(_changeRangeActionPCName, InputActionType.Button);
            changeRangeActionPC.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            grabItemActionPC.performed += CallGrabItem;
            grabItemActionPC.canceled += CallFreeItem;
            changeRangeActionPC.performed += CallStartChangingRange;
            changeRangeActionPC.canceled += CallStopChangingRange;
            #endregion Bind PC input

            #region Bind VR input
            var grabItemActionVR = _inputActionsVR.AddAction(_grabActionVRName, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");
            grabItemActionVR.performed += CallGrabItem;
            grabItemActionVR.canceled += CallFreeItem;
            #endregion Bind VR input
        }

        public void SetupParams (GrabItemToolParams parameters) => Debug.LogWarning($"GrabItemTool.SetupParams not implemented");

        public void StartChangingRange ()
        {
            _isChangingRange = true;
            _changeRangeCoroutine = StartCoroutine(ChangingRangeCoroutine());
        }

        public void StopChangingRange ()
        {
            _isChangingRange = false;
            StopCoroutine(_changeRangeCoroutine);
        }
    }

    /// <summary>
    /// Class that describes <see cref="GrabItemTool"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class GrabItemToolParams : AbstractToolParams
    { }
}