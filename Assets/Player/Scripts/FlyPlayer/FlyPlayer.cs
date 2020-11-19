using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControls
{
    public readonly struct PlayerParams
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        public float MovingSpeed { get; }
        public float RotationSpeed { get; }
        public ToolConfig[] ToolConfigs { get; }

        public PlayerParams (Vector3 position, Vector3 eulerAngles, float movingSpeed, float rotationSpeed, ToolConfig[] toolConfigs)
        {
            Position = position;
            EulerAngles = eulerAngles;
            RotationSpeed = rotationSpeed;
            MovingSpeed = movingSpeed;
            ToolConfigs = toolConfigs ?? throw new ArgumentNullException(nameof(toolConfigs));
        }
    }

    [RequireComponent(typeof(MoveComponent))]
    sealed public class FlyPlayer : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _moveDirVector;
        private FlyControls _inputActions;
        private MoveComponent _moveComponent;
        private List<PlayerTool> _playerItems;
        private int _currentToolIndex = 0;
        private GameObject _hand;

        private void Awake ()
        {
            _transform = transform;

            _hand = new GameObject("Hand");
            _hand.transform.parent = _transform;
            _hand.transform.localPosition = new Vector3(2, -2, 0);
            _hand.AddComponent(typeof(LaserPointer));

            _playerItems = new List<PlayerTool>();

            _inputActions = new FlyControls();
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void CreateTool (params ToolConfig[] toolsConfig)
        {
            var clonedList = _playerItems.GetRange(0, _playerItems.Count);
            try
            {
                foreach (var config in toolsConfig)
                {
                    var newTool = ((PlayerTool) _hand.AddComponent(config.ToolType));
                    newTool.RegisterEvents(_inputActions);
                    newTool.SetUpTool(config.ToolParams);
                    _playerItems.Add(newTool);
                    _playerItems[_playerItems.Count - 1].enabled = false;
                }

                SelectTool(0);
            }
            catch
            {
                _playerItems = clonedList;
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
                _playerItems[_currentToolIndex].enabled = false;
                _currentToolIndex = index;
                _playerItems[_currentToolIndex].enabled = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogError($"Wrong tool index {_currentToolIndex}");
                _currentToolIndex = lastIndex;
                _playerItems[_currentToolIndex].enabled = true;
            }
        }

        public void OnSelectItem (InputAction.CallbackContext obj) => SelectTool(Convert.ToInt32(obj.control.displayName) - 1);

        public void SetUpPlayer (in PlayerParams playerParams)
        {
            transform.position = playerParams.Position;
            transform.eulerAngles = playerParams.EulerAngles;
            _moveComponent.MovingSpeed = playerParams.MovingSpeed;
            _moveComponent.RotationSpeed = playerParams.RotationSpeed;
            CreateTool(playerParams.ToolConfigs);
        }
    }
}