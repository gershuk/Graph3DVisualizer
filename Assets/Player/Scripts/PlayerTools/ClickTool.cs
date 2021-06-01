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

using Yuzu;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// Tool for working with 3d menu components.
    /// </summary>
    [CustomizableGrandType(typeof(ClickToolParams))]
    public sealed class ClickTool : AbstractPlayerTool, ICustomizable<ClickToolParams>
    {
        #region Input names PC
        private const string _selectActionNamePC = "ClickObjectActionPC";
        #endregion Input names PC

        #region Input names VR
        private const string _selectActionNameVR = "ClickObjectActionVR";
        #endregion Input names VR

        private AbstractClickableObject? _clickableObject;
        private GameObject _owner;

        private void Awake ()
        {
            //due to problems with serialization, you have to search for it from inside
            _owner = transform.parent.gameObject;
        }

        private void CallClick (InputAction.CallbackContext obj) => Click();

        public void Click ()
        {
            //if (_clickableObject)
            //    _clickableObject.SetDisabled();

            _clickableObject = RayCast().transform?.GetComponent<AbstractClickableObject>();
            if (_clickableObject != null)
                _clickableObject.Click(_owner);
        }

        public new ClickToolParams DownloadParams (Dictionary<Guid, object> writeCache) => new ClickToolParams((this as ICustomizable<ToolParams>).DownloadParams(writeCache));

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            base.RegisterEvents(inputActions);

            #region Bind PC input
            var selectItemActionPC = _inputActionsPC.AddAction(_selectActionNamePC, InputActionType.Button, "<Mouse>/leftButton");
            selectItemActionPC.canceled += CallClick;
            #endregion Bind PC input

            #region Bind VR input
            var selectItemActionVR = _inputActionsVR.AddAction(_selectActionNameVR, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");
            selectItemActionVR.canceled += CallClick;
            #endregion Bind VR input
        }

        public void SetupParams (ClickToolParams parameters) => (this as ICustomizable<ToolParams>).SetupParams(parameters);
    }

    /// <summary>
    /// Class that describes <see cref="ClickTool"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class ClickToolParams : ToolParams
    {
        public ClickToolParams (bool isVR = false, float rayCastRange = 1000) : base(isVR, rayCastRange)
        {
        }

        public ClickToolParams (ToolParams toolParams) : this(toolParams.IsVR, toolParams.RayCastRange)
        {
        }
    }
}