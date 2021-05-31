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
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractGraph"/>.
    /// </summary>
    [CustomizableGrandType(typeof(GraphParameters))]
    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Graph : AbstractGraph
    {
        private MovementComponent _movementComponent;

        [SerializeField]
        protected static Mesh _vertexMesh;

        protected readonly Dictionary<string, AbstractVertex> _vertexes = new Dictionary<string, AbstractVertex>();
        protected BillboardController _billboardController;
        protected BillboardId _imageId;
        protected MeshFilter _meshFilter;

        protected string? _name;

        protected SphereCollider _sphereCollider;

        protected TextTextureFactory _textTextureFactory;
        public override MovementComponent MovementComponent { get => _movementComponent; protected set => _movementComponent = value; }

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
                    var scale = new Vector2(10, text.height * 1.0f / text.width * 10);
                    var textParameters = new BillboardParameters(text, Vector4.zero, scale);
                    _imageId = _billboardController.CreateBillboard(textParameters);
                    _sphereCollider.radius = Mathf.Max(scale.x / 2, scale.y / 2);
                }
            }
        }

        public override int VertexesCount => _vertexes.Count;

        private static Mesh CreateQuadMesh ()
        {
            const float width = 0.5f;
            const float height = 0.5f;
            var mesh = new Mesh();

            var vertices = new Vector3[4]
            {
            new Vector3(-width, -height, 0),
            new Vector3(width, -height, 0),
            new Vector3(-width, height, 0),
            new Vector3(width, height, 0)
            };
            mesh.vertices = vertices;

            var tris = new int[6]
            {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
            };
            mesh.triangles = tris;

            var normals = new Vector3[4]
            {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
            };
            mesh.normals = normals;

            var uv = new Vector2[4]
            {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
            };
            mesh.uv = uv;

            return mesh;
        }

        private void Awake ()
        {
            MovementComponent = GetComponent<MovementComponent>();
            _vertexMesh ??= CreateQuadMesh();
            _transform = GetComponent<Transform>();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.sharedMesh = _vertexMesh;
            GetComponent<MeshRenderer>().sharedMaterials = new Material[0];
            _vertexMesh = _meshFilter.sharedMesh;
            _billboardController = GetComponent<BillboardController>();
            _sphereCollider = GetComponent<SphereCollider>();
            _textTextureFactory = new TextTextureFactory(FontsGenerator.GetOrCreateFont("Arial", 32), 0);
        }

        private GameObject CreateVertexBody ()
        {
            var vertex = new GameObject("Vertex");
            vertex.transform.parent = _transform;

            var meshRender = vertex.AddComponent<MeshRenderer>();
            meshRender.sharedMaterials = new Material[0];
            meshRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRender.receiveShadows = false;
            //meshRender.receiveGI = ReceiveGI.LightProbes;
            meshRender.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            meshRender.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            meshRender.motionVectorGenerationMode = MotionVectorGenerationMode.Object;
            meshRender.allowOcclusionWhenDynamic = true;

            var meshFilter = vertex.AddComponent<MeshFilter>();

            meshFilter.sharedMesh = _vertexMesh;
            _vertexMesh = meshFilter.sharedMesh;

            return vertex;
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
            var vertex = CreateVertexBody();
            var vertexComponent = vertex.gameObject.AddComponent<TVertex>();
            (vertexComponent as ICustomizable<TParams>).SetupParams(vertexParameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }

        public override AbstractVertex SpawnVertex (Type vertexType, AbstractVertexParameters parameters)
        {
            if (!vertexType.IsSubclassOf(typeof(AbstractVertex)))
                throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);

            var vertex = CreateVertexBody();
            var vertexComponent = (AbstractVertex) vertex.gameObject.AddComponent(vertexType);
            CustomizableExtension.CallSetUpParams(vertexComponent, parameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }
    }
}