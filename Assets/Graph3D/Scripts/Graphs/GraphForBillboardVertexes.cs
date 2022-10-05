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
using System.Linq;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractGraph"/>.
    /// </summary>
    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [CustomizableGrandType(typeof(GraphParameters))]
    public class GraphForBillboardVertexes : AbstractGraph
    {
        protected readonly Dictionary<string, AbstractVertex> _vertexes = new();
        protected BillboardController _billboardController;
        protected BillboardId _imageId;
        protected MeshFilter _meshFilter;
        protected MovementComponent _movementComponent;
        protected string? _name;

        protected SphereCollider _sphereCollider;

        [Obsolete]
        protected TextTextureFactory _textTextureFactory;

        public bool ColliderEnable
        {
            get => _sphereCollider.enabled;
            set => _sphereCollider.enabled = value;
        }

        public override MovementComponent MovementComponent
        {
            get => _movementComponent;
            protected set => _movementComponent = value;
        }

        [Obsolete]
        public override string? Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;

                if (_imageId != null)
                    _billboardController.DeleteBillboard(_imageId);

                if (_name != null)
                {
                    var text = _textTextureFactory.MakeTextTexture(_name, true);
                    var scale = new Vector2(40, text.height * 1.0f / text.width * 40);
                    var textParameters = new BillboardParameters(text, Vector4.zero, scale, isMonoColor: true, monoColor: Color.white);
                    _imageId = _billboardController.CreateBillboard(textParameters);
                    _sphereCollider.radius = Mathf.Max(scale.x / 2, scale.y / 2);
                }
            }
        }

        public override int VertexesCount => _vertexes.Count;

        private GameObject CreateVertexBody ()
        {
            GameObject vertex = new("Vertex");
            vertex.transform.parent = _transform;
            var meshRender = vertex.AddComponent<MeshRenderer>();
            meshRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRender.receiveShadows = false;
            meshRender.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            meshRender.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            meshRender.motionVectorGenerationMode = MotionVectorGenerationMode.Object;
            meshRender.allowOcclusionWhenDynamic = true;
            return vertex;
        }

        [Obsolete]
        protected override void Awake ()
        {
            base.Awake();
            _meshFilter = GetComponent<MeshFilter>();
            _billboardController = GetComponent<BillboardController>();
            _sphereCollider = GetComponent<SphereCollider>();
            _textTextureFactory = new(FontsGenerator.GetOrCreateFont("Arial", 32), 0);
        }

        public override bool ContainsVertex (string id) => _vertexes.ContainsKey(id);

        public override bool DeleteVeretex (string id)
        {
            var result = _vertexes.Remove(id);
            if (result)
                Destroy(GetVertexById(id).gameObject);
            return result;
        }

        public override AbstractVertex GetVertexById (string id) => _vertexes[id];

        public override IReadOnlyList<AbstractVertex> GetVertexes () => _vertexes.Values.ToArray();

        public override TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters)
        {
            var vertexComponent = CreateVertexBody().AddComponent<TVertex>();
            ((ICustomizable<TParams>) vertexComponent).SetupParams(vertexParameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }

        public override AbstractVertex SpawnVertex (Type vertexType, AbstractVertexParameters parameters)
        {
            if (!vertexType.IsSubclassOf(typeof(AbstractVertex)))
                throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);

            var vertexComponent = (AbstractVertex) CreateVertexBody().AddComponent(vertexType);
            CustomizableExtension.CallSetUpParams(vertexComponent, parameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }
    }
}