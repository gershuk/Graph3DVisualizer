using System.Collections.Generic;
using System.Collections.ObjectModel;

using PlayerInputControls;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControllers
{
    [RequireComponent(typeof(MoveComponent))]
    public class FlyPlayer : MonoBehaviour
    {
        private Vector3 _moveDirVector;
        private FlyControls _inputActions;
        private MoveComponent _moveComponent;

        private void Awake ()
        {
            Cursor.visible = false;
            _inputActions = new FlyControls();
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void OnEnable ()
        {
            _inputActions.FlyModel.FirstItemFunction.started += OnFirstFunctionUsed;

            _inputActions.FlyModel.Move.performed += OnPlayerMove;
            _inputActions.FlyModel.Move.canceled += OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed += OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed += OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled += OnPlayerChangeAltitude;

            _inputActions.FlyModel.Enable();
        }

        private void OnDisable ()
        {
            _inputActions.FlyModel.FirstItemFunction.started -= OnFirstFunctionUsed;

            _inputActions.FlyModel.Move.performed -= OnPlayerMove;
            _inputActions.FlyModel.Move.canceled -= OnPlayerMove;

            _inputActions.FlyModel.MoveToPoint.performed -= OnMoveToPoint;

            _inputActions.FlyModel.ChangeAltitude.performed -= OnPlayerChangeAltitude;
            _inputActions.FlyModel.ChangeAltitude.canceled -= OnPlayerChangeAltitude;

            _inputActions.FlyModel.Disable();
        }

        public void OnPlayerMove (InputAction.CallbackContext obj)
        {
            var direction = obj.ReadValue<Vector2>();
            _moveDirVector = new Vector3(direction.x, _moveDirVector.y, direction.y);
        }

        public void OnPlayerChangeAltitude (InputAction.CallbackContext obj) => _moveDirVector.y = obj.ReadValue<float>();

        public void OnMoveToPoint (InputAction.CallbackContext obj)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity))
                StartCoroutine(_moveComponent.MoveAlongTrajectory(new ReadOnlyCollection<Vector3>(new List<Vector3>(1) { hit.point })));
        }

        public void OnFirstFunctionUsed (InputAction.CallbackContext obj) { }

        private void LateUpdate ()
        {
            _moveComponent.Rotate(_inputActions.FlyModel.LookRotation.ReadValue<Vector2>(), Time.deltaTime);
            _moveComponent.Translate(_moveDirVector, Time.deltaTime);
        }
    }
}