---
title: Assets/Graph3D/Scripts/Edge.cs


---

# Assets/Graph3D/Scripts/Edge.cs







## Namespaces

| Name           |
| -------------- |
| **[Grpah3DVisualizer](Namespaces/namespace_grpah3_d_visualizer.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[Grpah3DVisualizer::AdjacentVertices](Classes/struct_grpah3_d_visualizer_1_1_adjacent_vertices.md)**  |
| class | **[Grpah3DVisualizer::EdgeParameters](Classes/class_grpah3_d_visualizer_1_1_edge_parameters.md)**  |
| class | **[Grpah3DVisualizer::Edge](Classes/class_grpah3_d_visualizer_1_1_edge.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualizer
{
    public enum EdgeVisibility
    {
        Hidden = 0,
        Visible = 1,
        DependOnVertices = 2,
    }

    public enum EdgeType
    {
        Unidirectional = 0,
        Bidirectional = 1,
    }

    public readonly struct AdjacentVertices
    {
        public Vertex FromVertex { get; }
        public Vertex ToVertex { get; }

        public AdjacentVertices (Vertex fromVertex, Vertex toVertex)
        {
            FromVertex = fromVertex != null ? fromVertex : throw new ArgumentNullException(nameof(fromVertex));
            ToVertex = toVertex != null ? toVertex : throw new ArgumentNullException(nameof(toVertex));
        }

        public void Deconstruct (out Vertex fromVertex, out Vertex toVertex) => (fromVertex, toVertex) = (FromVertex, ToVertex);

        public float GetDistance () => Vector3.Distance(FromVertex.transform.position, ToVertex.transform.position);

        public Vector3 GetUnitVector () => (ToVertex.transform.position - FromVertex.transform.position) / GetDistance();

        public Vector3 GetMiddlePoint () => (FromVertex.transform.position + ToVertex.transform.position) / 2;
    }

    public class EdgeParameters
    {
        public AdjacentVertices AdjacentVertices { get; }
        public float SourceOffsetDist { get; }
        public float TargetOffsetDist { get; }
        public Texture2D ArrowTexture { get; }
        public Texture2D LineTexture { get; }
        public EdgeVisibility Visibility { get; }

        public EdgeParameters (in AdjacentVertices adjacentVertices, float sourceOffsetDist, float targetOffsetDist,
            Texture2D arrowTexture = null, Texture2D lineTexture = null, EdgeVisibility visibility = EdgeVisibility.DependOnVertices)
        {
            AdjacentVertices = adjacentVertices;
            SourceOffsetDist = sourceOffsetDist;
            TargetOffsetDist = targetOffsetDist;
            ArrowTexture = arrowTexture;
            LineTexture = lineTexture;
            Visibility = visibility;
        }
    }

    [RequireComponent(typeof(LineRenderer))]
    public class Edge : MonoBehaviour, ICustomizable<EdgeParameters>
    {
        private const string _cutoff = "_Cutoff";
        private static Shader _shader;
        private static Texture2D _defaultArrowTexture;
        private static Texture2D _defaultLineTexture;

        [SerializeField]
        private Texture2D _lineTexture;
        [SerializeField]
        private Texture2D _arrowTexture;

        private Transform _transform;

        private Material _material;
        private LineRenderer _lineRenderer;

        private float _sourceOffsetDist;
        private float _targetOffsetDist;
        private EdgeType _type;
        private AdjacentVertices _adjacentVertices;
        private EdgeVisibility _visibility;

        public AdjacentVertices AdjacentVertices
        {
            get => _adjacentVertices;
            set
            {
                if (!_adjacentVertices.Equals(value))
                {
                    UnsubscribeOnVerticesEvents();
                    _adjacentVertices = value;
                    SubscribeOnVerticesEvents();
                    UpdateCoordinates();
                }
            }
        }

        public EdgeType Type
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

        public float SourceOffsetDist
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

        public float TargetOffsetDist
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

        public EdgeVisibility Visibility
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

        public Texture2D LineTexture
        {
            get => _lineTexture;
            set => _lineTexture = value;
        }

        public Texture2D ArrowTexture
        {
            get => _arrowTexture;
            set => _arrowTexture = value;
        }

        private void Awake ()
        {
            _transform = GetComponent<Transform>();

            _defaultArrowTexture = _defaultArrowTexture == null ? (Texture2D) Resources.Load("Textures/Arrow") : _defaultArrowTexture;
            _defaultLineTexture = _defaultLineTexture == null ? (Texture2D) Resources.Load("Textures/Line") : _defaultLineTexture;

            ArrowTexture = _defaultArrowTexture;
            LineTexture = _defaultLineTexture;

            _shader = _shader == null ? Shader.Find("Custom/EdgeShader") : _shader;
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

        private void OnMove (Vector3 arg1, UnityEngine.Object arg2) => UpdateCoordinates();
        private void OnVisibilityChange (bool visibility, UnityEngine.Object obj) => UpdateVisibility();
        private void OnDestroyed (UnityEngine.Object obj) => Destroy(gameObject);

        private void SubscribeOnVerticesEvents ()
        {
            var vertexes = new[] { AdjacentVertices.FromVertex, AdjacentVertices.ToVertex };
            foreach (var vertex in vertexes)
            {
                if (vertex != null)
                {
                    vertex.Destroyed += OnDestroyed;
                    vertex.VisibleChanged += OnVisibilityChange;
                    vertex.MoveComponent.ObjectMoved += OnMove;
                }
            }
        }

        private void UnsubscribeOnVerticesEvents ()
        {
            var vertexes = new[] { AdjacentVertices.FromVertex, AdjacentVertices.ToVertex };
            foreach (var vertex in vertexes)
            {
                if (vertex != null)
                {
                    vertex.Destroyed -= OnDestroyed;
                    vertex.VisibleChanged -= OnVisibilityChange;
                    vertex.MoveComponent.ObjectMoved -= OnMove;
                }
            }
        }

        private void OnDestroy () => UnsubscribeOnVerticesEvents();

        public void UpdateType ()
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

        public void UpdateCoordinates ()
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

        public void UpdateVisibility ()
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

        public void UpdateEdge ()
        {
            UpdateType();
            UpdateVisibility();
            UpdateCoordinates();
        }

        public void SetupParams (EdgeParameters parameters)
        {
            _adjacentVertices = parameters.AdjacentVertices;
            SubscribeOnVerticesEvents();

            _sourceOffsetDist = parameters.SourceOffsetDist;
            _targetOffsetDist = parameters.TargetOffsetDist;

            ArrowTexture = parameters.ArrowTexture != null ? parameters.ArrowTexture : ArrowTexture;
            LineTexture = parameters.LineTexture != null ? parameters.LineTexture : LineTexture;

            _visibility = parameters.Visibility;

            UpdateEdge();
        }

        public EdgeParameters DownloadParams () => new EdgeParameters(_adjacentVertices, _sourceOffsetDist, _targetOffsetDist, ArrowTexture, LineTexture, _visibility);

    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
