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

using System.Collections;

using Grpah3DVisualizer.SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Grpah3DVisualizer.PlayerInputControls
{
    [RequireComponent(typeof(LaserPointer))]
    public class GrabItemTool : PlayerTool
    {
        private const string _actionMapName = "GrabItemActionMap";
        private const string _changeRangeActionName = "ChangeRangeAction";
        private const string _grabActionName = "GrabItemAction";

        [SerializeField]
        private float _capturedRange = 1000;

        private Coroutine _changeRangeCoroutine;

        private InputActionMap _inputActions;

        private bool _isCapturedObject = false;

        private bool _isChangingRange;

        private LaserPointer _laserPointer;

        private IMoveable _moveable = null;

        [SerializeField]
        private float _rangeChangeSpeed = 30;

        [SerializeField]
        private float _rayCastRange = 1000;

        private Transform _transform;
        public float CapturedRange { get => _capturedRange; set => _capturedRange = value; }
        public float RangeChangeSpeed { get => _rangeChangeSpeed; set => _rangeChangeSpeed = value; }

        private void Awake ()
        {
            _laserPointer = GetComponent<LaserPointer>();
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
            var changeRangeAction = _inputActions.FindAction(_changeRangeActionName);
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
            _inputActions?.Disable();
            _laserPointer.LaserState = LaserState.Off;
            FreeItem();
        }

        private void OnEnable ()
        {
            _inputActions?.Enable();
            _laserPointer.LaserState = LaserState.On;
            _laserPointer.Range = _rayCastRange;
        }

        public void ChangeRange (float normalizedDelta) => _capturedRange = Mathf.Max(0, _capturedRange + normalizedDelta * Time.deltaTime * RangeChangeSpeed);

        public void FreeItem ()
        {
            _moveable = null;
            _isCapturedObject = false;
            _laserPointer.Range = _rayCastRange;
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
                    _laserPointer.Range = _capturedRange;
                    _isCapturedObject = true;
                }
            }
        }

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_actionMapName);
            var grabItemAction = _inputActions.AddAction(_grabActionName, InputActionType.Button, "<Mouse>/leftButton");
            var changeRangeAction = _inputActions.AddAction(_changeRangeActionName, InputActionType.Button);
            changeRangeAction.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            grabItemAction.performed += CallGrabItem;
            grabItemAction.canceled += CallFreeItem;

            changeRangeAction.performed += CallStartChangingRange;
            changeRangeAction.canceled += CallStopChangingRange;
        }

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
}