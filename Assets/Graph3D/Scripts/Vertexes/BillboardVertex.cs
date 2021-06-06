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

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractVertex"/>.
    /// </summary>
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(SphereCollider))]
    [CustomizableGrandType(typeof(BillboardVertexParameters))]
    public class BillboardVertex : AbstractVertex, ICustomizable<BillboardVertexParameters>
    {
        protected BillboardController _billboardControler;

        protected SphereCollider _sphereCollider;

        [SerializeField]
        public Texture2D texture2D;

        public override event Action<UnityEngine.Object>? Destroyed;

        public override event Action<bool, UnityEngine.Object>? VisibleChanged;

        public virtual IList<BillboardId> ImageIds { get; protected set; } = new List<BillboardId>();
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

        public BillboardId AddImage (BillboardParameters billboardParameters)
        {
            var res = _billboardControler.CreateBillboard(billboardParameters);
            UpdateColliderRange();
            return res;
        }

        public void DeleteImage (BillboardId billboardId)
        {
            _billboardControler.DeleteBillboard(billboardId);
            UpdateColliderRange();
        }

        public new BillboardVertexParameters DownloadParams (Dictionary<Guid, object> writeCache) =>
                                            new BillboardVertexParameters(ImageIds.Select(id => (_billboardControler.GetBillboard(id).DownloadParams(writeCache))).ToArray(),
                (this as ICustomizable<AbstractVertexParameters>).DownloadParams(writeCache));

        public virtual Vector2 GetImageSize (BillboardId id) => new Vector2(_billboardControler.GetBillboard(id).ScaleX, _billboardControler.GetBillboard(id).ScaleY);

        public virtual void SetImageSize (BillboardId id, Vector2 vector2)
        {
            var billboard = _billboardControler.GetBillboard(id);
            billboard.ScaleX = vector2.x;
            billboard.ScaleY = vector2.y;
            UpdateColliderRange();
        }

        public void SetupParams (BillboardVertexParameters parameters)
        {
            base.SetupParams(parameters);
            foreach (var param in parameters.ImageParameters)
                ImageIds.Add(AddImage(param));
            UpdateColliderRange();
        }
    }

    /// <summary>
    /// Class that describes default vertex parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class BillboardVertexParameters : AbstractVertexParameters
    {
        public BillboardParameters[] ImageParameters { get; protected set; }

        public BillboardVertexParameters (BillboardParameters[] imageParameters, Vector3 position = default, Vector3 eulerAngles = default, string? id = default) : base(position, eulerAngles, id)
            => ImageParameters = imageParameters;

        public BillboardVertexParameters (BillboardParameters[] imageParameters, AbstractVertexParameters abstractVertexParameters) :
            this(imageParameters, abstractVertexParameters.Position, abstractVertexParameters.EulerAngles, abstractVertexParameters.Id.ToString())
        { }
    }
}