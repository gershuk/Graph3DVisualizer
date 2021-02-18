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

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractEdge"/>.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class Edge : AbstractEdge
    {
        private const string _arrowTexturePath = "Textures/Arrow";
        private const string _cutoff = "_Cutoff";
        private const string _edgeShaderPath = "Custom/EdgeShader";
        private const string _lineTexturePath = "Textures/Line";
        private static Texture2D _defaultArrowTexture;
        private static Texture2D _defaultLineTexture;
        private static Shader _shader;
        private LineRenderer _lineRenderer;
        private Material _material;

        public override Texture2D ArrowTexture
        {
            get => _arrowTexture;
            set => _arrowTexture = value;
        }

        public override Texture2D LineTexture
        {
            get => _lineTexture;
            set => _lineTexture = value;
        }

        public override float SourceOffsetDist
        {
            get => _sourceOffsetDist;
            set
            {
                if (_sourceOffsetDist != value)
                {
                    _sourceOffsetDist = value;
                    UpdateCoordinates();
                }
            }
        }

        public override float TargetOffsetDist
        {
            get => _targetOffsetDist;
            set
            {
                if (_targetOffsetDist != value)
                {
                    _targetOffsetDist = value;
                    UpdateCoordinates();
                }
            }
        }

        public override EdgeType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    UpdateType();
                }
            }
        }

        public override EdgeVisibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    UpdateVisibility();
                }
            }
        }

        private void Awake ()
        {
            _transform = GetComponent<Transform>();

            _defaultArrowTexture = _defaultArrowTexture == null ? Resources.Load<Texture2D>(_arrowTexturePath) : _defaultArrowTexture;
            _defaultLineTexture = _defaultLineTexture == null ? Resources.Load<Texture2D>(_lineTexturePath) : _defaultLineTexture;

            ArrowTexture = _defaultArrowTexture;
            LineTexture = _defaultLineTexture;

            _shader = _shader == null ? Shader.Find(_edgeShaderPath) : _shader;
            _material = new Material(_shader) { mainTexture = LineTexture };
            _material.SetFloat(_cutoff, 0.8f);

            _lineRenderer = GetComponent<LineRenderer>();
            if (_lineRenderer == null)
                _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.sharedMaterial = _material;
            _material = _lineRenderer.sharedMaterial;
            _lineRenderer.positionCount = 2;
            _lineRenderer.useWorldSpace = false;

            _sourceOffsetDist = 1f;
            _targetOffsetDist = 1f;
            _type = EdgeType.Unidirectional;
        }

        private void OnDestroy () => UnsubscribeOnVerticesEvents();

        private void OnDestroyed (UnityEngine.Object obj) => Destroy(gameObject);

        private void OnMove (Vector3 arg1, UnityEngine.Object arg2) => UpdateCoordinates();

        private void OnVisibilityChange (bool visibility, UnityEngine.Object obj) => UpdateVisibility();

        protected override void SubscribeOnVerticesEvents ()
        {
            var vertexes = new[] { AdjacentVertices.FromVertex, AdjacentVertices.ToVertex };
            foreach (var vertex in vertexes)
            {
                if (vertex != null)
                {
                    vertex.Destroyed += OnDestroyed;
                    vertex.VisibleChanged += OnVisibilityChange;
                    vertex.MovementComponent.ObjectMoved += OnMove;
                }
            }
        }

        protected override void UnsubscribeOnVerticesEvents ()
        {
            var vertexes = new[] { AdjacentVertices.FromVertex, AdjacentVertices.ToVertex };
            foreach (var vertex in vertexes)
            {
                if (vertex != null)
                {
                    vertex.Destroyed -= OnDestroyed;
                    vertex.VisibleChanged -= OnVisibilityChange;
                    vertex.MovementComponent.ObjectMoved -= OnMove;
                }
            }
        }

        public override void UpdateCoordinates ()
        {
            if (_visibility == EdgeVisibility.DependOnVertices)
            {
                _transform.position = _adjacentVertices.GetMiddlePoint();
                var from = _adjacentVertices.FromVertex.transform.position + _adjacentVertices.GetUnitVector() * _sourceOffsetDist;
                var to = _adjacentVertices.ToVertex.transform.position - _adjacentVertices.GetUnitVector() * _targetOffsetDist;
                from -= _transform.position;
                to -= _transform.position;
                _lineRenderer.SetPosition(0, from);
                _lineRenderer.SetPosition(1, to);
            }
        }

        public override void UpdateEdge ()
        {
            UpdateType();
            UpdateVisibility();
            UpdateCoordinates();
        }

        public override void UpdateType ()
        {
            switch (_type)
            {
                case EdgeType.Unidirectional:
                    _material.mainTexture = ArrowTexture;
                    break;

                case EdgeType.Bidirectional:
                    _material.mainTexture = LineTexture;
                    break;
            }
        }

        public override void UpdateVisibility ()
        {
            switch (_visibility)
            {
                case EdgeVisibility.Hidden:
                    _lineRenderer.enabled = false;
                    break;

                case EdgeVisibility.Visible:
                    _lineRenderer.enabled = true;
                    break;

                case EdgeVisibility.DependOnVertices:
                    _lineRenderer.enabled = _adjacentVertices.FromVertex.Visibility && _adjacentVertices.ToVertex.Visibility;
                    break;
            }

            UpdateCoordinates();
        }
    }
}