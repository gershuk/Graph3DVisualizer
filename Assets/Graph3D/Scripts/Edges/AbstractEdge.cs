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
    /// <summary>
    /// A class for describing adjacent vertexes.
    /// </summary>
    public readonly struct AdjacentVertices
    {
        public float Distance => Vector3.Distance(FromVertex.transform.position, ToVertex.transform.position);
        public AbstractVertex FromVertex { get; }
        public Vector3 MiddlePoint => (FromVertex.transform.position + ToVertex.transform.position) / 2;
        public AbstractVertex ToVertex { get; }

        public Vector3 UnitVector => (ToVertex.transform.position - FromVertex.transform.position) / Distance;

        public AdjacentVertices (AbstractVertex fromVertex, AbstractVertex toVertex)
        {
            FromVertex = fromVertex != null ? fromVertex : throw new ArgumentNullException(nameof(fromVertex));
            ToVertex = toVertex != null ? toVertex : throw new ArgumentNullException(nameof(toVertex));
        }

        public void Deconstruct (out AbstractVertex fromVertex, out AbstractVertex toVertex) => (fromVertex, toVertex) = (FromVertex, ToVertex);
    }

    public struct SpringParameters
    {
        public float Length { get; set; }
        public float StiffnessCoefficient { get; set; }

        public SpringParameters (float stiffnessCoefficient, float length) => (StiffnessCoefficient, Length) = (stiffnessCoefficient, length);
    }

    [Serializable]
    [YuzuAll]
    public abstract class AbstarctEdgeMaterialParameters : AbstractCustomizableParameter
    {
        [YuzuExclude]
        public Shader Shader { get; }

        public bool UseCache { get; protected set; }

        protected AbstarctEdgeMaterialParameters (Shader shader, bool useCache = default, Guid? id = default) : base(id) => (Shader, UseCache) = (shader, useCache);
    }

    /// <summary>
    /// Abstarct class for describing visual part of the graph edge.
    /// </summary>
    [CustomizableGrandType(typeof(EdgeParameters))]
    [RequireComponent(typeof(LineRenderer))]
    public abstract class AbstractEdge : AbstractGraphObject, ICustomizable<EdgeParameters>
    {
        protected AdjacentVertices _adjacentVertices;
        protected GameObject _gameObject;
        protected Material _material;
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

        public Guid? CacheGuid { get; private set; }

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

        public SpringParameters SpringParameters { get; protected set; } = new SpringParameters(1, 40);

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

        [ContextMenu("UpdateEdge")]
        private void CallUpdateEdge () => UpdateEdge();

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
                    vertex.MovementComponent.ObjectPositionChanged += OnMove;
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
                    vertex.MovementComponent.ObjectPositionChanged -= OnMove;
                }
            }
        }

        public EdgeParameters DownloadParams (Dictionary<Guid, object> writeCache) => new EdgeParameters(null, SpringParameters, SourceOffsetDist, TargetOffsetDist, Width, Visibility, Id);

        public void SetupParams (EdgeParameters parameters)
        {
            Id = parameters.ObjectId;

            SourceOffsetDist = parameters.SourceOffsetDist;
            TargetOffsetDist = parameters.TargetOffsetDist;
            Width = parameters.Width;

            Visibility = parameters.Visibility;

            if (parameters.AbstarctEdgeMaterialParameters == null)
                throw new NullReferenceException();

            if (parameters.AbstarctEdgeMaterialParameters.UseCache)
                CacheGuid = parameters.AbstarctEdgeMaterialParameters.Id;

            if (parameters.AbstarctEdgeMaterialParameters.UseCache && CacheForCustomizableObjects.TryGetValue(parameters.AbstarctEdgeMaterialParameters, out var custimizableObject))
            {
                _material = (Material) custimizableObject!;
            }
            else
            {
                _material = new Material(parameters.AbstarctEdgeMaterialParameters.Shader) { enableInstancing = true };

                if (parameters.AbstarctEdgeMaterialParameters.UseCache)
                    CacheForCustomizableObjects.Add(parameters.AbstarctEdgeMaterialParameters, _material);
            }

            SpringParameters = parameters.SpringParameters;

            UpdateEdge();
        }

        public abstract void UpdateCoordinates ();

        public void UpdateEdge ()
        {
            UpdateType();
            UpdateVisibility();
            UpdateCoordinates();
        }

        public abstract void UpdateType ();

        public abstract void UpdateVisibility ();
    }

    /// <summary>
    /// A class that describes default edge parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class EdgeParameters : AbstractGraphObjectParameters
    {
        public AbstarctEdgeMaterialParameters? AbstarctEdgeMaterialParameters { get; protected set; }
        public float SourceOffsetDist { get; protected set; }
        public SpringParameters SpringParameters { get; protected set; }
        public float TargetOffsetDist { get; protected set; }
        public EdgeVisibility Visibility { get; protected set; }
        public float Width { get; protected set; }

        public EdgeParameters (AbstarctEdgeMaterialParameters? abstarctEdgeMaterialParameters, SpringParameters springParameters = default, float sourceOffsetDist = 1f, float targetOffsetDist = 1f, float width = 1f,
            EdgeVisibility visibility = EdgeVisibility.DependOnVertices, string? id = default) : base(id)
        {
            AbstarctEdgeMaterialParameters = abstarctEdgeMaterialParameters;
            SpringParameters = springParameters;
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