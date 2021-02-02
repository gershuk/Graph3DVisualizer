using System;
using System.Collections;
using System.Collections.Generic;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControls
{
    public class ClickToolParams : ToolParams
    {
        public GameObject Owner { get; private set; }

        public ClickToolParams (GameObject owner) => Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    [RequireComponent(typeof(LaserPointer))]
    public class ClickTool : PlayerTool, ICustomizable<ClickToolParams>
    {
        private const string _inputActionName = "ClickObjectActionMap";
        private const string _selectActionName = "ClickObjectAction";
        private GameObject _owner;
        private ClickableObject _clickableObject;

        [SerializeField]
        private float _rayCastRange = 1000;

        private InputActionMap _inputActions;
        private LaserPointer _laserPointer;

        public float RayCastRange { get => _rayCastRange; set => _rayCastRange = value; }

        private void Awake ()
        {
            _laserPointer = GetComponent<LaserPointer>();
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

        private void CallClick (InputAction.CallbackContext obj) => Click();

        public void Click ()
        {
            //if (_clickableObject)
            //    _clickableObject.SetDisabled();

            _clickableObject = RayCast(_rayCastRange).transform?.GetComponent<ClickableObject>();
            if (_clickableObject)
                _clickableObject.Click(_owner);
        }

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_inputActionName);
            var selectItemAction = _inputActions.AddAction(_selectActionName, InputActionType.Button, "<Mouse>/leftButton");

            selectItemAction.canceled += CallClick;
        }

        public ClickToolParams DownloadParams () => new ClickToolParams(_owner);
        public void SetupParams (ClickToolParams parameters) => _owner = parameters.Owner;
    }
}
