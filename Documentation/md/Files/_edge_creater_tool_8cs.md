---
title: Assets/Player/Scripts/PlayerTools/EdgeCreaterTool.cs


---

# Assets/Player/Scripts/PlayerTools/EdgeCreaterTool.cs







## Namespaces

| Name           |
| -------------- |
| **[PlayerInputControls](Namespaces/namespace_player_input_controls.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[PlayerInputControls::EdgeCreaterToolParams](Classes/class_player_input_controls_1_1_edge_creater_tool_params.md)**  |
| class | **[PlayerInputControls::EdgeCreaterTool](Classes/class_player_input_controls_1_1_edge_creater_tool.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
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

using Grpah3DVisualizer;

using SupportComponents;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputControls
{
    public class EdgeCreaterToolParams : ToolParams
    {
        public IReadOnlyList<Type> EdgeTypes { get; private set; }
        public EdgeCreaterToolParams (IReadOnlyList<Type> edgeTypes)
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
    public class EdgeCreaterTool : PlayerTool, ICustomizable<EdgeCreaterToolParams>
    {
        private enum State
        {
            None,
            Selecting,
            LinkChanging
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

        private void CallChangeEdgeType (InputAction.CallbackContext obj) => ChangeIndex(Mathf.RoundToInt(obj.ReadValue<float>()));

        private void CallSelectFirstPoint (InputAction.CallbackContext obj) => SelectFirstPoint();

        private void CallCreateEdge (InputAction.CallbackContext obj)
        {
            SelectSecondPoint();
            CreateEdge();
        }

        private void CallDeleteEdge (InputAction.CallbackContext obj)
        {
            SelectSecondPoint();
            DeleteEdge();
        }

        public void SelectFirstPoint ()
        {
            if (_state == State.None)
            {
                _firstVertex = RayCast(_rayCastRange).transform?.GetComponent<Vertex>();
                if (_firstVertex != null)
                    _state = State.Selecting;
            }
        }

        public void SelectSecondPoint ()
        {
            if (_state == State.Selecting)
            {
                _secondVertex = RayCast(_rayCastRange).transform?.GetComponent<Vertex>();
                if (_secondVertex != null)
                    _state = State.LinkChanging;
            }
        }

        public void DeleteEdge ()
        {
            if (_state == State.LinkChanging)
            {
                try
                {
                    _firstVertex.UnLink(_secondVertex, _edgeTypes[_typeIndex]);
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
        }

        public void CreateEdge ()
        {
            if (_state == State.LinkChanging)
            {
                try
                {
                    _firstVertex.Link(_secondVertex, _edgeTypes[_typeIndex], _linkParameters);
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
        }

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_actionMapName);
            var createEdgeAction = _inputActions.AddAction(_createEdgeActionName, InputActionType.Button, "<Mouse>/leftButton");
            var deleteEdgeAction = _inputActions.AddAction(_deleteEdgeActionName, InputActionType.Button, "<Mouse>/rightButton");
            var changeEdgeTypeAction = _inputActions.AddAction(_changeRangeActionName, InputActionType.Button);
            changeEdgeTypeAction.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            createEdgeAction.performed += CallSelectFirstPoint;
            createEdgeAction.canceled += CallCreateEdge;

            deleteEdgeAction.performed += CallSelectFirstPoint;
            deleteEdgeAction.canceled += CallDeleteEdge;

            changeEdgeTypeAction.started += CallChangeEdgeType;
        }

        public void ChangeIndex (int deltaIndex) => _typeIndex = (_typeIndex + deltaIndex) < 0 ? _edgeTypes.Count - 1 : (_typeIndex + deltaIndex) % _edgeTypes.Count;

        public void SetupParams (EdgeCreaterToolParams parameters) => _edgeTypes = parameters.EdgeTypes.ToList();

        public EdgeCreaterToolParams DownloadParams () => new EdgeCreaterToolParams(_edgeTypes);
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (����)