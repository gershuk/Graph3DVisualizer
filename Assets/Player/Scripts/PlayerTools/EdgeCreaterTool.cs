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
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.Graph3D;

using UnityEngine;
using UnityEngine.InputSystem;

using Yuzu;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Tool for creating links between vertexes.
    /// </summary>
    [CustomizableGrandType(typeof(EdgeCreaterToolParams))]
    public sealed class EdgeCreaterTool : AbstractPlayerTool, ICustomizable<EdgeCreaterToolParams>
    {
        private enum State
        {
            None,
            Selecting,
            LinkChanging
        }

        #region Input names PC
        private const string _changeRangeActionPCName = "ChangeEdgeTypeActionPC";
        private const string _createEdgeActionPCName = "CreateEdgeActionPC";
        private const string _deleteEdgeActionPCName = "DeleteEdgeActionPC";
        #endregion Input names PC

        #region Input names VR
        private const string _changeRangeActionVRName = "ChangeEdgeTypeActionVR";
        private const string _createEdgeActionVRName = "CreateEdgeActionVR";
        private const string _deleteEdgeActionVRName = "DeleteEdgeActionVR";
        #endregion Input names VR

        private List<(Type type, EdgeParameters parameters)> _edgeData = new List<(Type, EdgeParameters)>();

        private BillboardVertex? _firstVertex;

        private BillboardVertex? _secondVertex;
        private State _state;
        private int _typeIndex;

        private void Awake () => _state = State.None;

        private void CallChangeEdgeType (InputAction.CallbackContext obj) => ChangeIndex(Mathf.RoundToInt(obj.ReadValue<float>()));

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

        private void CallSelectFirstPoint (InputAction.CallbackContext obj) => SelectFirstPoint();

        public void ChangeIndex (int deltaIndex) => _typeIndex = (_typeIndex + deltaIndex) < 0 ? _edgeData.Count - 1 : (_typeIndex + deltaIndex) % _edgeData.Count;

        public void CreateEdge ()
        {
            if (_state == State.LinkChanging)
            {
                try
                {
                    _firstVertex!.Link(_secondVertex!, _edgeData[_typeIndex].type, _edgeData[_typeIndex].parameters);
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

        public void DeleteEdge ()
        {
            if (_state == State.LinkChanging)
            {
                try
                {
                    _firstVertex!.UnLink(_secondVertex!, _edgeData[_typeIndex].type);
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

        public new EdgeCreaterToolParams DownloadParams (Dictionary<Guid, object> writeCache) => new EdgeCreaterToolParams(_edgeData, (this as ICustomizable<ToolParams>).DownloadParams(writeCache));

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            base.RegisterEvents(inputActions);

            #region Bind PC input
            var createEdgeActionPC = _inputActionsPC.AddAction(_createEdgeActionPCName, InputActionType.Button, "<Mouse>/leftButton");
            var deleteEdgeActionPC = _inputActionsPC.AddAction(_deleteEdgeActionPCName, InputActionType.Button, "<Mouse>/rightButton");
            var changeEdgeTypeActionPC = _inputActionsPC.AddAction(_changeRangeActionPCName, InputActionType.Button);
            changeEdgeTypeActionPC.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");

            createEdgeActionPC.performed += CallSelectFirstPoint;
            createEdgeActionPC.canceled += CallCreateEdge;

            deleteEdgeActionPC.performed += CallSelectFirstPoint;
            deleteEdgeActionPC.canceled += CallDeleteEdge;

            changeEdgeTypeActionPC.started += CallChangeEdgeType;
            #endregion Bind PC input

            #region Bind VR input
            var createEdgeActionVR = _inputActionsVR.AddAction(_createEdgeActionVRName, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");
            createEdgeActionVR.performed += CallSelectFirstPoint;
            createEdgeActionVR.canceled += CallCreateEdge;
            #endregion Bind VR input
        }

        public void SelectFirstPoint ()
        {
            if (_state == State.None)
            {
                _firstVertex = RayCast().transform?.GetComponent<BillboardVertex>();
                if (_firstVertex != null)
                    _state = State.Selecting;
            }
        }

        public void SelectSecondPoint ()
        {
            if (_state == State.Selecting)
            {
                _secondVertex = RayCast().transform?.GetComponent<BillboardVertex>();
                if (_secondVertex != null)
                    _state = State.LinkChanging;
            }
        }

        public void SetupParams (EdgeCreaterToolParams parameters)
        {
            (this as ICustomizable<ToolParams>).SetupParams(parameters);
            _edgeData = parameters.EdgeTypes.ToList();
        }
    }

    /// <summary>
    /// Class that describes <see cref="EdgeCreaterTool"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class EdgeCreaterToolParams : ToolParams
    {
        public IReadOnlyList<(Type, EdgeParameters)> EdgeTypes { get; protected set; }

        public EdgeCreaterToolParams (IReadOnlyList<(Type, EdgeParameters)> edgeTypes, bool isVR = false, float rayCastRange = 1000) : base(isVR, rayCastRange)
        {
            EdgeTypes = edgeTypes ?? throw new ArgumentNullException(nameof(edgeTypes));
            foreach (var (type, parameters) in EdgeTypes)
            {
                if (!type.IsSubclassOf(typeof(AbstractEdge)))
                    throw new WrongTypeInCustomizableParameterException(typeof(AbstractEdge), type);
            }
        }

        public EdgeCreaterToolParams (IReadOnlyList<(Type, EdgeParameters)> edgeTypes, ToolParams toolParams) : this(edgeTypes, toolParams.IsVR, toolParams.RayCastRange)
        {
        }
    }
}