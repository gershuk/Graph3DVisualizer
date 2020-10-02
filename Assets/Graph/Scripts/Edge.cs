using System;
using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
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

        public void Deconstruct (out Vertex fromVertex, out Vertex toVertex)
        {
            fromVertex = FromVertex;
            toVertex = ToVertex;
        }

        public float GetDistance () => Vector3.Distance(FromVertex.transform.position, ToVertex.transform.position);

        public Vector3 GetUnitVector () => (ToVertex.transform.position - FromVertex.transform.position) / GetDistance();

        public Vector3 GetMiddlePoint () => (FromVertex.transform.position + ToVertex.transform.position) / 2;
    }

    public readonly struct EdgeParameters
    {
        public Vector3 Position { get; }
        public float SourceOffsetDist { get; }
        public float TargetOffsetDist { get; }
        public AdjacentVertices AdjacentVertices { get; }
        public Texture2D ArrowTexture { get; }
        public Texture2D LineTexture { get; }
        public EdgeVisibility Visibility { get; }

        public EdgeParameters (Vector3 position, float sourceOffsetDist, float targetOffsetDist, AdjacentVertices adjacentVertices,
            Texture2D arrowTexture = null, Texture2D lineTexture = null, EdgeVisibility visibility = EdgeVisibility.DependOnVertices)
        {
            Position = position;
            SourceOffsetDist = sourceOffsetDist;
            TargetOffsetDist = targetOffsetDist;
            AdjacentVertices = adjacentVertices;
            ArrowTexture = arrowTexture;
            LineTexture = lineTexture;
            Visibility = visibility;
        }
    }

    [RequireComponent(typeof(LineRenderer))]
    public class Edge : MonoBehaviour
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

        public void UpdateEdge ()
        {
            UpdateType();
            UpdateVisibility();
            UpdateCoordinates();
        }

        public void SetUpEdge (EdgeParameters edgeParameters)
        {
            _transform.position = edgeParameters.Position;
            _adjacentVertices = edgeParameters.AdjacentVertices;
            SubscribeOnVerticesEvents();
            _sourceOffsetDist = edgeParameters.SourceOffsetDist;
            _targetOffsetDist = edgeParameters.TargetOffsetDist;
            _arrowTexture = edgeParameters.ArrowTexture != null ? edgeParameters.ArrowTexture : _arrowTexture;
            _lineTexture = edgeParameters.LineTexture != null ? edgeParameters.LineTexture : _lineTexture;
            _visibility = edgeParameters.Visibility;
            UpdateEdge();
        }

        private void OnDestroy () => UnsubscribeOnVerticesEvents();

        private void Awake ()
        {
            _transform = GetComponent<Transform>();

            _shader = _shader == null ? Shader.Find("Custom/EdgeShader") : _shader;
            _material = new Material(_shader) { mainTexture = _lineTexture };
            _material.SetFloat(_cutoff, 0.8f);

            _defaultArrowTexture = _defaultArrowTexture == null ? (Texture2D) Resources.Load("Textures/Arrow") : _defaultArrowTexture;
            _defaultLineTexture = _defaultLineTexture == null ? (Texture2D) Resources.Load("Textures/Line") : _defaultLineTexture;

            _arrowTexture = _defaultArrowTexture;
            _lineTexture = _defaultLineTexture;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material = _material;
            _lineRenderer.positionCount = 2;

            _sourceOffsetDist = 1f;
            _targetOffsetDist = 1f;
            _type = EdgeType.Unidirectional;
        }

        private void UpdateType ()
        {
            switch (_type)
            {
                case EdgeType.Unidirectional:
                    _material.mainTexture = _arrowTexture;
                    break;
                case EdgeType.Bidirectional:
                    _material.mainTexture = _lineTexture;
                    break;
            }
        }

        private void UpdateCoordinates ()
        {
            if (_visibility == EdgeVisibility.DependOnVertices)
            {
                _transform.position = _adjacentVertices.GetMiddlePoint();
                var from = _adjacentVertices.FromVertex.transform.position + _adjacentVertices.GetUnitVector() * _sourceOffsetDist;
                var to = _adjacentVertices.ToVertex.transform.position - _adjacentVertices.GetUnitVector() * _targetOffsetDist;
                _lineRenderer.SetPosition(0, from);
                _lineRenderer.SetPosition(1, to);
            }
        }

        private void UpdateVisibility ()
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
                    _lineRenderer.enabled = _adjacentVertices.FromVertex.GetVisibility() && _adjacentVertices.ToVertex.GetVisibility();
                    break;
            }

            UpdateCoordinates();
        }

        private void SubscribeOnVerticesEvents ()
        {
            var vertexes = new[] { AdjacentVertices.FromVertex, AdjacentVertices.ToVertex };
            foreach (var vertex in vertexes)
            {
                if (vertex != null)
                {
                    vertex.OnDestroyed += Vertex_OnDestroyed;
                    vertex.OnVisibleChange += Vertex_OnVisibilityChange;
                    vertex.OnMove += Vertex_OnMove;
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
                    vertex.OnDestroyed -= Vertex_OnDestroyed;
                    vertex.OnVisibleChange -= Vertex_OnVisibilityChange;
                    vertex.OnMove -= Vertex_OnMove;
                }
            }
        }

        private void Vertex_OnMove (Vector3 arg1, UnityEngine.Object arg2) => UpdateCoordinates();
        private void Vertex_OnVisibilityChange (bool visibility, UnityEngine.Object obj) => UpdateVisibility();
        private void Vertex_OnDestroyed (UnityEngine.Object obj) => Destroy(gameObject);
    }
}
