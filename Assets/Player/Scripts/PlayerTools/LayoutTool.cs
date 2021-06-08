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
using Graph3DVisualizer.Graph3D;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Graph3DVisualizer.PlayerInputControls
{
    [CustomizableGrandType(typeof(SelectItemToolParams))]
    public class LayoutTool : AbstractPlayerTool
    {
        #region Input names PC
        private const string _changeLayoutTypePCName = "ChangeLayoutTypePC";
        private const string _doLayoutActionPCName = "DoLayoutActionPC";
        #endregion Input names PC

        #region Input names VR
        private const string _changeLayoutTypeVRName = "ChangeLayoutTypeVR";
        private const string _doLayoutActionVRName = "DoLayoutActionVR";
        #endregion Input names VR

        private IReadOnlyList<Action<AbstractGraph>> _layoutFunctions;
        private int _layoutTypeIndex;

        private static void StartForceBasedLayout (AbstractGraph abstractGraph) => abstractGraph.StartForceBasedLayout();

        private static void StartPyramidLayout (AbstractGraph abstractGraph) => abstractGraph.StartPyramidLayout();

        private static void StartSpectralLayout (AbstractGraph abstractGraph) => abstractGraph.StartSpectralLayout();

        private void Awake ()
        {
            _layoutTypeIndex = 0;
            _layoutFunctions = new List<Action<AbstractGraph>>(3)
            {
                StartForceBasedLayout,
                StartPyramidLayout,
                StartSpectralLayout,
            };
        }

        private void CallChangeColor (InputAction.CallbackContext obj) => ChangeColor(Math.Sign(Mathf.RoundToInt(obj.ReadValue<float>())));

        private void CallSelectItem (InputAction.CallbackContext obj) => SelectItem();

        public void ChangeColor (int deltaIndex) => _layoutTypeIndex = (_layoutTypeIndex + deltaIndex) < 0 ? _layoutFunctions.Count - 1 : (_layoutTypeIndex + deltaIndex) % _layoutFunctions.Count;

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            base.RegisterEvents(inputActions);

            #region Bind PC input
            var doLayoutActionPC = _inputActionsPC.AddAction(_doLayoutActionPCName, InputActionType.Button, "<Mouse>/leftButton");
            var changeLayoutTypePC = _inputActionsPC.AddAction(_changeLayoutTypePCName, InputActionType.Button);
            changeLayoutTypePC.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");
            doLayoutActionPC.canceled += CallSelectItem;
            changeLayoutTypePC.started += CallChangeColor;
            #endregion Bind PC input

            #region Bind VR input
            var doLayoutActionVR = _inputActionsVR.AddAction(_doLayoutActionVRName, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");
            var changeLayoutTypeVR = _inputActionsVR.AddAction(_changeLayoutTypeVRName, InputActionType.Value, "<ViveController>{RightHand}/trackpad/x");
            doLayoutActionVR.canceled += CallSelectItem;
            changeLayoutTypeVR.started += CallChangeColor;
            #endregion Bind VR input
        }

        public void SelectItem ()
        {
            var graph = RayCast().transform?.GetComponent<AbstractVertex>()?.transform.parent.GetComponent<AbstractGraph>();
            if (graph != null)
            {
                _layoutFunctions[_layoutTypeIndex].Invoke(graph);
            }
        }
    }
}