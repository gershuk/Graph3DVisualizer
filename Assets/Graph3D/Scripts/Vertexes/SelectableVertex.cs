// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Realization of <see cref="BillboardVertex"/> with selection support.
    /// </summary>
    [CustomizableGrandType(typeof(SelectableVertexParameters))]
    public class SelectableVertex : BillboardVertex, ICustomizable<SelectableVertexParameters>, ISelectable
    {
        protected const string _edgePrefabPath = "Prefabs/Edge";
        protected bool _isSelected = true;
        protected BillboardId _selectFrameId;

        //ToDo : Add highlight effect
        public event Action<UnityEngine.Object, bool>? HighlightedChanged;

        public event Action<UnityEngine.Object, bool>? SelectedChanged;

        public bool IsHighlighted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    if (value)
                        _billboardControler.EnableBillboard(_selectFrameId);
                    else
                        _billboardControler.DisableBillboard(_selectFrameId);

                    SelectedChanged?.Invoke(this, value);
                }
            }
        }

        public override MovementComponent? MovementComponent { get; protected set; }

        public Color SelectFrameColor
        {
            get => _billboardControler.GetBillboard(_selectFrameId).MonoColor;
            set => _billboardControler.GetBillboard(_selectFrameId).MonoColor = value;
        }

        public Vector2 SetSelectFrameSize
        {
            get => new(_billboardControler.GetBillboard(_selectFrameId).ScaleX,
                       _billboardControler.GetBillboard(_selectFrameId).ScaleY);
            set
            {
                _billboardControler.GetBillboard(_selectFrameId).ScaleX = value.x;
                _billboardControler.GetBillboard(_selectFrameId).ScaleY = value.y;
                UpdateColliderRange();
            }
        }

        protected override void UpdateColliderRange ()
        {
            var newRadius = 0f;
            foreach (var id in ImageIds)
            {
                var image = _billboardControler.GetBillboard(id);
                newRadius = Mathf.Max(image.ScaleX / 2, image.ScaleY / 2, newRadius);
            }

            if (_selectFrameId != null)
            {
                var selectFrame = _billboardControler.GetBillboard(_selectFrameId);
                newRadius = Mathf.Max(selectFrame.ScaleX / 2, selectFrame.ScaleY / 2, newRadius);
            }
            _sphereCollider.radius = newRadius;
        }

        public new SelectableVertexParameters DownloadParams (Dictionary<Guid, object> writeCache) =>
            new((this as ICustomizable<BillboardVertexParameters>).DownloadParams(writeCache),
                (BillboardParameters) CustomizableExtension.CallDownloadParams(_billboardControler.GetBillboard(_selectFrameId),
                                                                               writeCache), IsSelected, Id);

        public void SetSelectFrame (BillboardParameters billboardParameters)
        {
            if (_selectFrameId == null)
                _selectFrameId = _billboardControler.CreateBillboard(billboardParameters);
            else
                _billboardControler.GetBillboard(_selectFrameId).SetupParams(billboardParameters);
            UpdateColliderRange();
        }

        public void SetupParams (SelectableVertexParameters parameters)
        {
            SetupParams((BillboardVertexParameters) parameters);
            SetSelectFrame(parameters.SelectFrameParameters);
            IsSelected = parameters.IsSelected;

            UpdateColliderRange();
        }
    }

    /// <summary>
    /// Class that describes <see cref="SelectableVertex"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class SelectableVertexParameters : BillboardVertexParameters
    {
        public bool IsSelected { get; protected set; }
        public BillboardParameters SelectFrameParameters { get; protected set; }

        public SelectableVertexParameters (BillboardParameters[] imageParameters,
                                           BillboardParameters selectFrameParameters,
                                           Vector3 position = default,
                                           Vector3 eulerAngles = default,
                                           bool isSelected = false,
                                           string? id = default) :
                base(imageParameters, position, eulerAngles, id) =>
                (SelectFrameParameters, IsSelected) = (selectFrameParameters, isSelected);

        public SelectableVertexParameters (BillboardVertexParameters vertexParameters,
                                           BillboardParameters selectFrameParameters,
                                           bool isSelected = false,
                                           string? id = default) :
                                           this(vertexParameters.ImageParameters,
                                                selectFrameParameters,
                                                vertexParameters.Position,
                                                vertexParameters.EulerAngles,
                                                isSelected,
                                                id)
        { }
    }
}