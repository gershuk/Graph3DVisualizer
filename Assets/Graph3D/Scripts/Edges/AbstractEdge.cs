﻿// This file is part of Graph3DVisualizer.
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
    /// <summary>
    /// A class for describing adjacent vertexes.
    /// </summary>
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

        public float Distance => Vector3.Distance(FromVertex.transform.position, ToVertex.transform.position);

        public Vector3 MiddlePoint => (FromVertex.transform.position + ToVertex.transform.position) / 2;

        public Vector3 UnitVector => (ToVertex.transform.position - FromVertex.transform.position) / Distance;
    }

    /// <summary>
    /// Abstarct class for describing visual part of the graph edge.
    /// </summary>
    [CustomizableGrandType(Type = typeof(EdgeParameters))]
    public abstract class AbstractEdge : AbstractGraphObject, ICustomizable<EdgeParameters>
    {
        protected AdjacentVertices _adjacentVertices;
        protected GameObject _gameObject;
        protected float _sourceOffsetDist;
        protected float _targetOffsetDist;
        protected Transform _transform;
        protected EdgeType _type;
        protected EdgeVisibility _visibility;
        protected float _width;

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

        public virtual float SourceOffsetDist
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

        public virtual float TargetOffsetDist
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

        public virtual EdgeType Type
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

        public virtual EdgeVisibility Visibility
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

        public virtual float Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    UpdateType();
                }
            }
        }

        public EdgeParameters DownloadParams () => new EdgeParameters(SourceOffsetDist, TargetOffsetDist, Width, Visibility, Id);

        public void SetupParams (EdgeParameters parameters)
        {
            Id = parameters.Id;

            SourceOffsetDist = parameters.SourceOffsetDist;
            TargetOffsetDist = parameters.TargetOffsetDist;
            Width = parameters.Width;

            Visibility = parameters.Visibility;

            UpdateEdge();
        }

        public abstract void UpdateCoordinates ();

#if UNITY_EDITOR

        [ContextMenu("UpdateEdge")]
        private void CallUpdateEdge () => UpdateEdge();

#endif

        public void UpdateEdge ()
        {
            UpdateType();
            UpdateVisibility();
            UpdateCoordinates();
        }

        public abstract void UpdateType ();

        public abstract void UpdateVisibility ();

        private void OnDestroyed (UnityEngine.Object obj) => Destroy(gameObject);

        private void OnMove (Vector3 arg1, UnityEngine.Object arg2) => UpdateCoordinates();

        private void OnVisibilityChange (bool visibility, UnityEngine.Object obj) => UpdateVisibility();

        protected virtual void SubscribeOnVerticesEvents ()
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

        protected virtual void UnsubscribeOnVerticesEvents ()
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
    }

    /// <summary>
    /// A class that describes default edge parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class EdgeParameters : AbstractGraphObjectParameters
    {
        public float SourceOffsetDist { get; set; }
        public float TargetOffsetDist { get; set; }
        public EdgeVisibility Visibility { get; set; }
        public float Width { get; set; }

        public EdgeParameters (float sourceOffsetDist = 1f, float targetOffsetDist = 1f, float width = 1f, EdgeVisibility visibility = EdgeVisibility.DependOnVertices, string id = null) : base(id)
        {
            SourceOffsetDist = sourceOffsetDist;
            TargetOffsetDist = targetOffsetDist;
            Width = width;
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