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

        public override event Action<UnityEngine.Object>? Destroyed;

        public override event Action<bool, UnityEngine.Object>? VisibleChanged;

        public override MovementComponent MovementComponent { get; protected set; }

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
            ImageIds = new List<BillboardId>();
        }

        private void OnDestroy () => Destroyed?.Invoke(this);

        protected virtual void UpdateColliderRange ()
        {
            var newRadius = 0f;
            foreach (var id in ImageIds)
            {
                var image = _billboardControler.GetBillboard(id);
                newRadius = Mathf.Max(image.ScaleX / 2, image.ScaleY / 2, newRadius);
            }
            _sphereCollider.radius = newRadius;
        }

        public override BillboardId AddImage (BillboardParameters billboardParameters)
        {
            var res = base.AddImage(billboardParameters);
            UpdateColliderRange();
            return res;
        }

        public override void DeleteImage (BillboardId billboardId)
        {
            base.DeleteImage(billboardId);
            UpdateColliderRange();
        }

        public override Vector2 GetImageSize (BillboardId id) => new Vector2(_billboardControler.GetBillboard(id).ScaleX, _billboardControler.GetBillboard(id).ScaleY);

        public override void SetImageSize (BillboardId id, Vector2 vector2)
        {
            var billboard = _billboardControler.GetBillboard(id);
            billboard.ScaleX = vector2.x;
            billboard.ScaleY = vector2.y;
            UpdateColliderRange();
        }

        public override void SetupParams (VertexParameters parameters)
        {
            base.SetupParams(parameters);
            UpdateColliderRange();
        }
    }
}