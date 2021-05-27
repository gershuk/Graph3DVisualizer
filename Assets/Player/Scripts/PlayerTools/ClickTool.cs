﻿// This file is part of Graph3DVisualizer.
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
    [RequireComponent(typeof(LaserPointer))]
    [CustomizableGrandType(typeof(ClickToolParams))]
    public class ClickTool : AbstractPlayerTool, ICustomizable<ClickToolParams>
    {
        private const string _inputActionName = "ClickObjectActionMap";
        private const string _selectActionNamePC = "ClickObjectActionPC";
        private const string _selectActionNameVR = "ClickObjectActionVR";
        private AbstractClickableObject? _clickableObject;
        private GameObject _owner;

        [SerializeField]
        private float _rayCastRange = 1000;

        public float RayCastRange { get => _rayCastRange; set => _rayCastRange = value; }

        private void Awake ()
        {
            //due to problems with serialization, you have to search for it from inside
            _owner = transform.parent.gameObject;
        }

        private void CallClick (InputAction.CallbackContext obj) => Click();

        private void OnDisable ()
        {
            _inputActions?.Disable();
        }

        private void OnEnable ()
        {
            _inputActions?.Enable();
        }

        public void Click ()
        {
            //if (_clickableObject)
            //    _clickableObject.SetDisabled();

            _clickableObject = RayCast(_rayCastRange).transform?.GetComponent<AbstractClickableObject>();
            if (_clickableObject != null)
                _clickableObject.Click(_owner);
        }

        public ClickToolParams DownloadParams (Dictionary<Guid, object> writeCache) => new ClickToolParams();

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            _inputActions = new InputActionMap(_inputActionName);
            var selectItemActionPC = _inputActions.AddAction(_selectActionNamePC, InputActionType.Button, "<Mouse>/leftButton");
            var selectItemActionVR = _inputActions.AddAction(_selectActionNameVR, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");

            selectItemActionPC.canceled += CallClick;
            selectItemActionVR.canceled += CallClick;
        }

        public void SetupParams (ClickToolParams parameters)
        {
        }
    }

    /// <summary>
    /// Class that describes <see cref="ClickTool"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class ClickToolParams : AbstractToolParams
    {
        //public GameObject Owner { get; private set; }

        //public ClickToolParams (GameObject owner) => Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }
}