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

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    [RequireComponent(typeof(LineRenderer))]
    [CustomizableGrandType(Type = typeof(StretchableEdgeParameters))]
    public class StretchableEdge : AbstractEdge, ICustomizable<StretchableEdgeParameters>
    {
        private const string _edgeShaderPath = "Custom/MonoColorSurface";
        private const string _shaderColor = "_Color";

        private static Shader _shader;

        [SerializeField]
        private Color _color;

        [SerializeField]
        private float _headLength;

        private LineRenderer _lineRenderer;
        private Material _material;

        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    _material.SetColor(_shaderColor, value);
                }
            }
        }

        public float HeadLength
        {
            get => _headLength;
            set
            {
                if (_headLength != value)
                {
                    _headLength = value;
                    UpdateCoordinates();
                }
            }
        }

        public override float Width
        {
            get => _width;
            set => base.Width = value;
        }

        private void Awake ()
        {
            _transform = GetComponent<Transform>();

            _shader = _shader == null ? Shader.Find(_edgeShaderPath) : _shader;
            _material = new Material(_shader);
            _material.SetColor(_shaderColor, Color.yellow);
            _material.enableInstancing = true;
            Color = Color.yellow;

            _lineRenderer = GetComponent<LineRenderer>();
            if (_lineRenderer == null)
                _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.sharedMaterial = _material;
            _material = _lineRenderer.sharedMaterial;
            _lineRenderer.positionCount = 2;
            _lineRenderer.useWorldSpace = false;

            _sourceOffsetDist = 1f;
            _targetOffsetDist = 1f;
            Type = EdgeType.Unidirectional;
        }

        public new StretchableEdgeParameters DownloadParams () => new StretchableEdgeParameters((this as ICustomizable<EdgeParameters>).DownloadParams(), HeadLength, Color);

        public void SetupParams (StretchableEdgeParameters parameters)
        {
            SetupParams((EdgeParameters) parameters);

            Color = parameters.Color;
            HeadLength = parameters.HeadLength;

            UpdateEdge();
        }

        public override void UpdateCoordinates ()
        {
            if (Visibility == EdgeVisibility.DependOnVertices || Visibility == EdgeVisibility.Visible)
            {
                _transform.position = _adjacentVertices.MiddlePoint;
                var from = _adjacentVertices.FromVertex.transform.position + _adjacentVertices.UnitVector * SourceOffsetDist;
                var to = _adjacentVertices.ToVertex.transform.position - _adjacentVertices.UnitVector * TargetOffsetDist;
                from -= _transform.position;
                to -= _transform.position;
                _lineRenderer.positionCount = 4;
                var percent = HeadLength / AdjacentVertices.Distance;
                _lineRenderer.SetPositions(new Vector3[] { from, Vector3.Lerp(from, to, 0.999f - percent), Vector3.Lerp(from, to, 1 - percent), to });
            }

            UpdateType();
        }

        public override void UpdateType ()
        {
            var percent = HeadLength / AdjacentVertices.Distance;
            switch (Type)
            {
                case EdgeType.Unidirectional:
                    _lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, 0.4f * Width), new Keyframe(0.999f - percent, 0.4f * Width), new Keyframe(1 - percent, 1f * Width), new Keyframe(1, 0f));
                    break;

                case EdgeType.Bidirectional:
                    _lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, Width), new Keyframe(0.999f - percent, Width), new Keyframe(1 - percent, Width), new Keyframe(1, Width));
                    break;
            }
        }

        public override void UpdateVisibility ()
        {
            switch (Visibility)
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

    [Serializable]
    [YuzuAll]
    public class StretchableEdgeParameters : EdgeParameters
    {
        public Color Color { get; protected set; }
        public float HeadLength { get; protected set; }

        public StretchableEdgeParameters (float sourceOffsetDist = 1f, float targetOffsetDist = 1f, float width = 1f, float headLength = 1f, Color color = default,
            EdgeVisibility visibility = EdgeVisibility.DependOnVertices, string id = null) : base(sourceOffsetDist, targetOffsetDist, width, visibility, id) => (Color, HeadLength) = (color, headLength);

        public StretchableEdgeParameters (EdgeParameters edgeParameters, float headLength = 1f, Color color = default) :
            this(edgeParameters.SourceOffsetDist, edgeParameters.TargetOffsetDist, edgeParameters.Width, headLength, color, edgeParameters.Visibility, edgeParameters.Id)
        { }
    }
}