using System;
using System.Collections.Generic;
using System.Linq;

using Grpah3DVisualser;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControls
{
    public class EdgeCreaterToolParams : ToolParams
    {
        public IReadOnlyCollection<Type> EdgeTypes { get; private set; }
        public EdgeCreaterToolParams (IReadOnlyCollection<Type> edgeTypes)
        {
            EdgeTypes = edgeTypes ?? throw new ArgumentNullException(nameof(edgeTypes));
            foreach (var type in EdgeTypes)
            {
                if (!type.IsSubclassOf(typeof(Edge)) && type != typeof(Edge))
                    throw new Exception($"{type} isn't subclass of Edge");
            }
        }
    }

    [RequireComponent(typeof(LaserPointer))]
    public class EdgeCreaterTool : PlayerTool
    {
        private enum State
        {
            None,
            Selecting,
        }

        private const string _createEdgeActionName = "CreateEdgeAction";
        private const string _deleteEdgeActionName = "DeleteEdgeAction";
        private const string _changeRangeActionName = "ChangeEdgeType";
        private const string _actionMapName = "EdgeCreaterActionMap";

        [SerializeField]
        private float _rayCastRange = 1000;
        private InputActionMap _inputActions;
        private LaserPointer _laserPointer;
        private List<Type> _edgeTypes;
        private int _typeIndex;
        private Vertex _firstVertex;
        private Vertex _secondVertex;
        private LinkParameters _linkParameters;
        private State _state;

        private void Awake ()
        {
            _state = State.None;
            _edgeTypes = new List<Type>();
            _laserPointer = GetComponent<LaserPointer>();
            _linkParameters = new LinkParameters(6, 6);
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

        private void ChangeEdgeTypeAction_started (InputAction.CallbackContext obj) => ChangeIndex(Mathf.RoundToInt(obj.ReadValue<float>()));

        private void SelectVertexAction_performed (InputAction.CallbackContext obj)
        {
            if (_state == State.None)
            {
                _firstVertex = RayCast(_rayCastRange).transform?.GetComponent<Vertex>();
                _state = State.Selecting;
            }
        }

        private void SelectVertexAction_canceled (InputAction.CallbackContext obj)
        {
            try
            {
                _secondVertex = RayCast(_rayCastRange).transform?.GetComponent<Vertex>();
                if (_secondVertex != null)
                    _firstVertex?.Link(_secondVertex, _edgeTypes[_typeIndex], _linkParameters);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                _state = State.None;
            }
        }

        private void DeleteEdgeAction_canceled (InputAction.CallbackContext obj)
        {
            try
            {
                _secondVertex = RayCast(_rayCastRange).transform?.GetComponent<Vertex>();
                if (_secondVertex != null)
                    _firstVertex?.UnLink(_secondVertex, _edgeTypes[_typeIndex]);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            finally
            {
                _state = State.None;
            }
        }


        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_actionMapName);
            var createEdgeAction = _inputActions.AddAction(_createEdgeActionName, InputActionType.Button, "<Mouse>/leftButton");
            var deleteEdgeAction = _inputActions.AddAction(_deleteEdgeActionName, InputActionType.Button, "<Mouse>/rightButton");
            var changeEdgeTypeAction = _inputActions.AddAction(_changeRangeActionName, InputActionType.Button);
            changeEdgeTypeAction.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            createEdgeAction.performed += SelectVertexAction_performed;
            createEdgeAction.canceled += SelectVertexAction_canceled;

            deleteEdgeAction.performed += SelectVertexAction_performed;
            deleteEdgeAction.canceled += DeleteEdgeAction_canceled;

            changeEdgeTypeAction.started += ChangeEdgeTypeAction_started;
        }

        public void ChangeIndex (int deltaIndex) => _typeIndex = (_typeIndex + deltaIndex) < 0 ? _edgeTypes.Count - 1 : (_typeIndex + deltaIndex) % _edgeTypes.Count;

        public override void SetUpTool (ToolParams toolParams) => _edgeTypes = (toolParams as EdgeCreaterToolParams).EdgeTypes.ToList();
    }
}
