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

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Simple realization of <see cref="AbstractEdge"/>.
    /// </summary>
    [CustomizableGrandType(typeof(SpriteEdgeParameters))]
    public class SpriteEdge : AbstractEdge, ICustomizable<SpriteEdgeParameters>
    {
        protected const string _arrowTexturePath = "Textures/Arrow";
        protected const string _cutoff = "_Cutoff";
        protected const string _edgeShaderPath = "Custom/EdgeShader";
        protected const string _lineTexturePath = "Textures/Line";
        protected static Texture2D _defaultArrowTexture;
        protected static Texture2D _defaultLineTexture;
        protected static Shader _shader;

        [SerializeField]
        protected Texture2D _arrowTexture;

        protected LineRenderer _lineRenderer;

        [SerializeField]
        protected Texture2D _lineTexture;

        public Texture2D ArrowTexture
        {
            get => _arrowTexture;
            set => _arrowTexture = value;
        }

        public Texture2D LineTexture
        {
            get => _lineTexture;
            set => _lineTexture = value;
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
            _lineRenderer.sharedMaterial = _material;
            _material = _lineRenderer.sharedMaterial;
            _lineRenderer.positionCount = 2;
            _lineRenderer.useWorldSpace = false;

            _sourceOffsetDist = 1f;
            _targetOffsetDist = 1f;
            _type = EdgeType.Unidirectional;
        }

        private void OnDestroy () => UnsubscribeOnVerticesEvents();

        public new SpriteEdgeParameters DownloadParams (Dictionary<Guid, object> writeCache) =>
            new((this as ICustomizable<EdgeParameters>).DownloadParams(writeCache), ArrowTexture, LineTexture);

        public void SetupParams (SpriteEdgeParameters parameters)
        {
            SetupParams((EdgeParameters) parameters);
            ArrowTexture = parameters.ArrowTexture != null ? parameters.ArrowTexture : ArrowTexture;
            LineTexture = parameters.LineTexture != null ? parameters.LineTexture : LineTexture;

            UpdateEdge();
        }

        public override void UpdateCoordinates ()
        {
            if (Visibility is EdgeVisibility.DependOnVertices or EdgeVisibility.Visible)
            {
                _transform.position = AdjacentVertices.MiddlePoint;
                var from = AdjacentVertices.FromVertex.transform.position + (AdjacentVertices.UnitVector * SourceOffsetDist);
                var to = AdjacentVertices.ToVertex.transform.position - (AdjacentVertices.UnitVector * TargetOffsetDist);
                from -= _transform.position;
                to -= _transform.position;
                _lineRenderer.SetPosition(0, from);
                _lineRenderer.SetPosition(1, to);
                _lineRenderer.widthCurve = new(new Keyframe(0, Width), new Keyframe(1, Width));
            }
        }

        public override void UpdateType () => _material.mainTexture = Type switch
        {
            EdgeType.Unidirectional => ArrowTexture,
            EdgeType.Bidirectional => LineTexture,
            _ => throw new NotImplementedException(),
        };

        public override void UpdateVisibility ()
        {
            _lineRenderer.enabled = Visibility switch
            {
                EdgeVisibility.Hidden => false,
                EdgeVisibility.Visible => true,
                EdgeVisibility.DependOnVertices => _adjacentVertices.FromVertex.Visibility && _adjacentVertices.ToVertex.Visibility,
                _ => throw new NotImplementedException()
            };
            UpdateCoordinates();
        }
    }

    [Serializable]
    [YuzuAll]
    public class SpriteEdgeParameters : EdgeParameters
    {
        public Texture2D? ArrowTexture { get; protected set; }
        public Texture2D? LineTexture { get; protected set; }

        public SpriteEdgeParameters (SpringParameters springParameters,
                                     float sourceOffsetDist = 1f,
                                     float targetOffsetDist = 1f,
                                     float width = 1f,
                                     EdgeVisibility visibility = EdgeVisibility.DependOnVertices,
                                     string? id = default,
                                     Texture2D? arrowTexture = null,
                                     Texture2D? lineTexture = null) :
            base(null, springParameters, sourceOffsetDist, targetOffsetDist, width, visibility, id) =>
            (ArrowTexture, LineTexture) = (arrowTexture, lineTexture);

        public SpriteEdgeParameters (EdgeParameters edgeParameters,
                                     Texture2D? arrowTexture = default,
                                     Texture2D? lineTexture = default) :
            this(edgeParameters.SpringParameters,
                 edgeParameters.SourceOffsetDist,
                 edgeParameters.TargetOffsetDist,
                 edgeParameters.Width,
                 edgeParameters.Visibility,
                 edgeParameters.ObjectId,
                 arrowTexture,
                 lineTexture)
        { }
    }
}