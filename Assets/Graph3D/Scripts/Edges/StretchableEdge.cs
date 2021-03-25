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

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    [RequireComponent(typeof(LineRenderer))]
    [CustomizableGrandType(typeof(StretchableEdgeParameters))]
    public sealed class StretchableEdge : AbstractEdge, ICustomizable<StretchableEdgeParameters>
    {
        private const string _edgeShaderPath = "Custom/MonoColorSurface";
        private const string _shaderColor = "_Color";

        [SerializeField]
        private float _headLength;

        private LineRenderer _lineRenderer;

        public static Shader Shader { get; set; } = Shader.Find(_edgeShaderPath);

        public Color Color
        {
            get => _material.GetColor(_shaderColor);
            set => _material.SetColor(_shaderColor, value);
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
            _lineRenderer = GetComponent<LineRenderer>();
            //_material = new Material(_shader);
            //_material.enableInstancing = true;
            //Color = Color.yellow;

            //_lineRenderer = GetComponent<LineRenderer>();
            //if (_lineRenderer == null)
            //    _lineRenderer = gameObject.AddComponent<LineRenderer>();
            //_lineRenderer.sharedMaterial = _material;
            //_material = _lineRenderer.sharedMaterial;
            //_lineRenderer.positionCount = 2;
            //_lineRenderer.useWorldSpace = false;

            _sourceOffsetDist = 1f;
            _targetOffsetDist = 1f;
            Type = EdgeType.Unidirectional;
        }

        public new StretchableEdgeParameters DownloadParams (Dictionary<Guid, object> writeCache)
        {
            var edgeParameters = (this as ICustomizable<EdgeParameters>).DownloadParams(writeCache);
            object edgeMaterialParameters = null;
            if (CacheGuid.HasValue)
            {
                if (!writeCache.TryGetValue(CacheGuid.Value, out edgeMaterialParameters))
                {
                    edgeMaterialParameters = new StretchableEdgeMaterialParameters(Color, true);
                    writeCache.Add(CacheGuid.Value, edgeMaterialParameters);
                }
            }
            edgeMaterialParameters ??= new StretchableEdgeMaterialParameters(color: Color);
            return new StretchableEdgeParameters((StretchableEdgeMaterialParameters) edgeMaterialParameters, HeadLength, edgeParameters);
        }

        public void SetupParams (StretchableEdgeParameters parameters)
        {
            if (parameters.AbstarctEdgeMaterialParameters == null)
                throw new NullReferenceException();

            SetupParams((EdgeParameters) parameters);

            Color = (parameters.AbstarctEdgeMaterialParameters as StretchableEdgeMaterialParameters)!.Color;

            _lineRenderer.sharedMaterial = _material;
            _material = _lineRenderer.sharedMaterial;
            _lineRenderer.positionCount = 2;
            _lineRenderer.useWorldSpace = false;

            //Do not use the property to avoid unnecessary update
            _headLength = parameters.HeadLength;

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

    [YuzuAll]
    [Serializable]
    public class StretchableEdgeMaterialParameters : AbstarctEdgeMaterialParameters
    {
        public Color Color { get; protected set; }

        public StretchableEdgeMaterialParameters (Color color = default, bool useCache = default, Guid? id = default) : base(StretchableEdge.Shader, useCache, id) => Color = color;

        //Need for serialization in Json to ignore the Shader property in the AbstarctEdgeMaterialParameters class.
        public StretchableEdgeMaterialParameters () : base(StretchableEdge.Shader)
        { }
    }

    [Serializable]
    [YuzuAll]
    public class StretchableEdgeParameters : EdgeParameters
    {
        public float HeadLength { get; protected set; }

        public StretchableEdgeParameters (StretchableEdgeMaterialParameters stretchableEdgeMaterialParameters, float headLength = 1f,
            float sourceOffsetDist = 1f, float targetOffsetDist = 1f, float width = 1f,
            EdgeVisibility visibility = EdgeVisibility.DependOnVertices, string? id = default) :
            base(stretchableEdgeMaterialParameters, sourceOffsetDist, targetOffsetDist, width, visibility, id) => HeadLength = headLength;

        public StretchableEdgeParameters (StretchableEdgeMaterialParameters stretchableEdgeMaterialParameters, float headLength, EdgeParameters edgeParameters) :
            this(stretchableEdgeMaterialParameters, headLength, edgeParameters.SourceOffsetDist, edgeParameters.TargetOffsetDist, edgeParameters.Width,
                edgeParameters.Visibility, edgeParameters.ObjectId)
        { }
    }
}