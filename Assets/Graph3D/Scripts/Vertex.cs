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

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractVertex"/>.
    /// </summary>
    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(SphereCollider))]
    public class Vertex : AbstractVertex
    {
        private const string _edgePrefabPath = "Prefabs/Edge";

        protected SphereCollider _sphereCollider;

        public override event Action<UnityEngine.Object> Destroyed;

        public override event Action<bool, UnityEngine.Object> VisibleChanged;

        public override MovementComponent MovementComponent { get; protected set; }

        public override Vector2 SetMainImageSize
        {
            get => new Vector2(_billboardControler.GetBillboard(_mainImageId).ScaleX, _billboardControler.GetBillboard(_mainImageId).ScaleY);
            set
            {
                _billboardControler.GetBillboard(_mainImageId).ScaleX = value.x;
                _billboardControler.GetBillboard(_mainImageId).ScaleY = value.y;
                UpdateColliderRange();
            }
        }

        public override bool Visibility
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    _billboardControler.Visibility = value;
                    VisibleChanged?.Invoke(value, this);
                }
            }
            get => _visible;
        }

        private void Awake ()
        {
            _transform = transform;
            _sphereCollider = GetComponent<SphereCollider>();
            _visible = true;
            _edgePrefab = _edgePrefab == null ? Resources.Load<GameObject>(_edgePrefabPath) : _edgePrefab;
            _incomingLinks = new List<Link>();
            _outgoingLinks = new List<Link>();
            _billboardControler = GetComponent<BillboardController>();
            MovementComponent = GetComponent<MovementComponent>();
        }

        private void OnDestroy () => Destroyed?.Invoke(this);

        protected virtual void UpdateColliderRange ()
        {
            var newRadius = _sphereCollider.radius;
            if (_mainImageId != null)
            {
                var mainImage = _billboardControler.GetBillboard(_mainImageId);
                newRadius = Mathf.Max(mainImage.ScaleX / 2, mainImage.ScaleY / 2);
            }
            _sphereCollider.radius = newRadius;
        }

        public override void SetMainImage (BillboardParameters billboardParameters)
        {
            if (_mainImageId == null)
                _mainImageId = _billboardControler.CreateBillboard(billboardParameters, "MainImage", "Vertex image");
            else
                _billboardControler.GetBillboard(_mainImageId).SetupParams(billboardParameters);
            UpdateColliderRange();
        }
    }
}