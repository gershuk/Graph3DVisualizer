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
    /// The tool allows you to select objects with components that implement the <see cref="ISelectable"/>.
    /// </summary>
    [CustomizableGrandType(typeof(SelectItemToolParams))]
    public sealed class SelectItemTool : AbstractPlayerTool, ICustomizable<SelectItemToolParams>
    {
        #region Input names PC
        private const string _changeColorActionPCName = "ChangeColorActionPC";
        private const string _selectActionPCName = "SelectItemActionPC";
        #endregion Input names PC

        #region Input names VR
        private const string _changeColorActionVRName = "ChangeColorActionVR";
        private const string _selectActionVRName = "SelectItemActionVR";
        #endregion Input names VR

        private int _colorIndex;
        private IReadOnlyList<Color> _colors = new List<Color>(1) { Color.red };

        private void Awake () => _colorIndex = 0;

        private void CallChangeColor (InputAction.CallbackContext obj) => ChangeColor(Math.Sign(Mathf.RoundToInt(obj.ReadValue<float>())));

        private void CallSelectItem (InputAction.CallbackContext obj) => SelectItem();

        public void ChangeColor (int deltaIndex) => _colorIndex = (_colorIndex + deltaIndex) < 0 ? _colors.Count - 1 : (_colorIndex + deltaIndex) % _colors.Count;

        public new SelectItemToolParams DownloadParams (Dictionary<Guid, object> writeCache) => new SelectItemToolParams(_colors, (this as ICustomizable<ToolParams>).DownloadParams(writeCache));

        public override void RegisterEvents (IInputActionCollection inputActions)
        {
            base.RegisterEvents(inputActions);

            #region Bind PC input
            var selectItemActionPC = _inputActionsPC.AddAction(_selectActionPCName, InputActionType.Button, "<Mouse>/leftButton");
            var changeColorActionPC = _inputActionsPC.AddAction(_changeColorActionPCName, InputActionType.Button);
            changeColorActionPC.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/e").With("Negative", "<Keyboard>/q");
            selectItemActionPC.canceled += CallSelectItem;
            changeColorActionPC.started += CallChangeColor;
            #endregion Bind PC input

            #region Bind VR input
            var selectItemActionVR = _inputActionsVR.AddAction(_selectActionVRName, InputActionType.Button, "<XRInputV1::HTC::HTCViveControllerOpenXR>{RightHand}/triggerpressed");
            var changeColorActionVR = _inputActionsVR.AddAction(_changeColorActionVRName, InputActionType.Value, "<ViveController>{RightHand}/trackpad/x");
            selectItemActionVR.canceled += CallSelectItem;
            changeColorActionVR.started += CallChangeColor;
            #endregion Bind VR input
        }

        public void SelectItem ()
        {
            const float eps = 1e-4f;
            var selectableComponent = RayCast().transform?.GetComponent<ISelectable>();
            if (selectableComponent != null)
            {
                if (selectableComponent.IsSelected)
                {
                    //The shader can distort colors, so the comparison is performed with eps.
                    if (Mathf.Abs(selectableComponent.SelectFrameColor.r - _colors[_colorIndex].r) < eps &&
                        Mathf.Abs(selectableComponent.SelectFrameColor.g - _colors[_colorIndex].g) < eps &&
                        Mathf.Abs(selectableComponent.SelectFrameColor.b - _colors[_colorIndex].b) < eps &&
                        Mathf.Abs(selectableComponent.SelectFrameColor.a - _colors[_colorIndex].a) < eps
                       )
                    {
                        selectableComponent.IsSelected = false;
                    }
                    else
                    {
                        selectableComponent.SelectFrameColor = _colors[_colorIndex];
                    }
                }
                else
                {
                    selectableComponent.IsSelected = true;
                    selectableComponent.SelectFrameColor = _colors[_colorIndex];
                }
            }
        }

        public void SetupParams (SelectItemToolParams parameters)
        {
            (this as ICustomizable<ToolParams>).SetupParams(parameters);
            _colors = parameters.Colors;
        }
    }

    /// <summary>
    /// Class that describes <see cref="SelectItemTool"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class SelectItemToolParams : ToolParams
    {
        public IReadOnlyList<Color> Colors { get; protected set; }

        public SelectItemToolParams (IReadOnlyList<Color> colors, bool isVR = false, float rayCastRange = 1000) : base(isVR, rayCastRange)
            => Colors = colors ?? throw new ArgumentNullException(nameof(colors));

        public SelectItemToolParams (IReadOnlyList<Color> colors, ToolParams toolParams) : this(colors, toolParams.IsVR, toolParams.RayCastRange)
        { }
    }
}