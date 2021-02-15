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

namespace Graph3DVisualizer.Graph3D
{
    public readonly struct AdjacentVertices
    {
        public AbstractVertex FromVertex { get; }
        public AbstractVertex ToVertex { get; }

        public AdjacentVertices (AbstractVertex fromVertex, AbstractVertex toVertex)
        {
            FromVertex = fromVertex != null ? fromVertex : throw new ArgumentNullException(nameof(fromVertex));
            ToVertex = toVertex != null ? toVertex : throw new ArgumentNullException(nameof(toVertex));
        }

        public void Deconstruct (out AbstractVertex fromVertex, out AbstractVertex toVertex) => (fromVertex, toVertex) = (FromVertex, ToVertex);

        public float GetDistance () => Vector3.Distance(FromVertex.transform.position, ToVertex.transform.position);

        public Vector3 GetMiddlePoint () => (FromVertex.transform.position + ToVertex.transform.position) / 2;

        public Vector3 GetUnitVector () => (ToVertex.transform.position - FromVertex.transform.position) / GetDistance();
    }

    [CustomizableGrandType(Type = typeof(EdgeParameters))]
    public abstract class AbstractEdge : AbstractGraphObject, ICustomizable<EdgeParameters>
    {
        protected AdjacentVertices _adjacentVertices;

        [SerializeField]
        protected Texture2D _arrowTexture;

        protected GameObject _gameObject;

        [SerializeField]
        protected Texture2D _lineTexture;

        protected float _sourceOffsetDist;
        protected float _targetOffsetDist;
        protected Transform _transform;
        protected EdgeType _type;
        protected EdgeVisibility _visibility;

        public virtual AdjacentVertices AdjacentVertices
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

        public abstract Texture2D ArrowTexture { get; set; }
        public abstract Texture2D LineTexture { get; set; }
        public abstract float SourceOffsetDist { get; set; }
        public abstract float TargetOffsetDist { get; set; }
        public abstract EdgeType Type { get; set; }
        public abstract EdgeVisibility Visibility { get; set; }

        protected abstract void SubscribeOnVerticesEvents ();

        protected abstract void UnsubscribeOnVerticesEvents ();

        public EdgeParameters DownloadParams () => new EdgeParameters(_sourceOffsetDist, _targetOffsetDist, ArrowTexture, LineTexture, _visibility, Id);

        public void SetupParams (EdgeParameters parameters)
        {
            Id = parameters.Id;

            _sourceOffsetDist = parameters.SourceOffsetDist;
            _targetOffsetDist = parameters.TargetOffsetDist;

            ArrowTexture = parameters.ArrowTexture != null ? parameters.ArrowTexture : ArrowTexture;
            LineTexture = parameters.LineTexture != null ? parameters.LineTexture : LineTexture;

            _visibility = parameters.Visibility;

            UpdateEdge();
        }

        public abstract void UpdateCoordinates ();

        public abstract void UpdateEdge ();

        public abstract void UpdateType ();

        public abstract void UpdateVisibility ();
    }

    public class EdgeParameters : AbstractGraphObjectParameters
    {
        public Texture2D ArrowTexture { get; }
        public Texture2D LineTexture { get; }
        public float SourceOffsetDist { get; }
        public float TargetOffsetDist { get; }
        public EdgeVisibility Visibility { get; }

        public EdgeParameters (float sourceOffsetDist = 1f, float targetOffsetDist = 1f,
            Texture2D arrowTexture = null, Texture2D lineTexture = null, EdgeVisibility visibility = EdgeVisibility.DependOnVertices, string id = null) : base(id)
        {
            SourceOffsetDist = sourceOffsetDist;
            TargetOffsetDist = targetOffsetDist;
            ArrowTexture = arrowTexture;
            LineTexture = lineTexture;
            Visibility = visibility;
        }
    }

    public enum EdgeType
    {
        Unidirectional = 0,
        Bidirectional = 1,
    }

    public enum EdgeVisibility
    {
        Hidden = 0,
        Visible = 1,
        DependOnVertices = 2,
    }
}