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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Support class for edge serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public struct LinkInfo
    {
        public EdgeParameters EdgeParameters { get; set; }
        public Type EdgeType { get; set; }
        public string FirstVertexId { get; set; }
        public string SecondVertexId { get; set; }

        public LinkInfo (string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters)
        {
            FirstVertexId = firstVertexId;
            SecondVertexId = secondVertexId;
            EdgeType = edgeType;
            EdgeParameters = edgeParameters;
        }

        public static implicit operator (string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters) (LinkInfo value) =>
            (value.FirstVertexId, value.SecondVertexId, value.EdgeType, value.EdgeParameters);

        public static implicit operator LinkInfo ((string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters) value) =>
            new LinkInfo(value.firstVertexId, value.secondVertexId, value.edgeType, value.edgeParameters);

        public void Deconstruct (out string firstVertexId, out string secondVertexId, out Type edgeType, out EdgeParameters edgeParameters)
        {
            firstVertexId = FirstVertexId;
            secondVertexId = SecondVertexId;
            edgeType = EdgeType;
            edgeParameters = EdgeParameters;
        }

        public override bool Equals (object obj) => obj is LinkInfo other &&
                   FirstVertexId == other.FirstVertexId &&
                   SecondVertexId == other.SecondVertexId &&
                   EqualityComparer<Type>.Default.Equals(EdgeType, other.EdgeType) &&
                   EqualityComparer<EdgeParameters>.Default.Equals(EdgeParameters, other.EdgeParameters);

        public override int GetHashCode ()
        {
            var hashCode = -1737920732;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstVertexId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecondVertexId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(EdgeType);
            hashCode = hashCode * -1521134295 + EqualityComparer<EdgeParameters>.Default.GetHashCode(EdgeParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Support class for vertex serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public struct VertexInfo
    {
        public AbstractVertexParameters VertexParameters { get; set; }
        public Type VertexType { get; set; }

        public VertexInfo (Type vertexType, AbstractVertexParameters vertexParameters) => (VertexType, VertexParameters) = (vertexType, vertexParameters);

        public static implicit operator (Type vertexType, AbstractVertexParameters vertexParameters) (VertexInfo value) => (value.VertexType, value.VertexParameters);

        public static implicit operator VertexInfo ((Type vertexType, AbstractVertexParameters vertexParameters) value) => new VertexInfo(value.vertexType, value.vertexParameters);

        public void Deconstruct (out Type vertexType, out AbstractVertexParameters vertexParameters)
        {
            vertexType = VertexType;
            vertexParameters = VertexParameters;
        }

        public override bool Equals (object obj) => obj is VertexInfo other &&
                   EqualityComparer<Type>.Default.Equals(VertexType, other.VertexType) &&
                   EqualityComparer<AbstractVertexParameters>.Default.Equals(VertexParameters, other.VertexParameters);

        public override int GetHashCode ()
        {
            var hashCode = -2103449114;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(VertexType);
            hashCode = hashCode * -1521134295 + EqualityComparer<AbstractVertexParameters>.Default.GetHashCode(VertexParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Abstract class that describes graph component.
    /// </summary>
    [CustomizableGrandType(typeof(GraphParameters))]
    [RequireComponent(typeof(MovementComponent))]
    public abstract class AbstractGraph : AbstractGraphObject, ICustomizable<GraphParameters>
    {
        private bool _isChanging = false;
        protected Transform _transform;
        public abstract MovementComponent MovementComponent { get; protected set; }
        public abstract string? Name { get; set; }

        public abstract int VertexesCount { get; }

        private IEnumerator ForceBasedLayoutCoroutine ()
        {
            _isChanging = true;
            const float eps = 1E-1f;
            const float speed = 5E-1f;
            var list = new List<(AbstractVertex abstractVertex, Vector3 force)>();
            var vertexMustMove = false;
            while (!vertexMustMove)
            {
                vertexMustMove = true;
                foreach (var vertex in GetVertexes())
                {
                    var sumForce = Vector3.zero;

                    foreach (var link in vertex.IncomingLinks.Concat(vertex.OutgoingLinks))
                    {
                        var dir = link.AdjacentVertex.transform.position - vertex.transform.position;
                        sumForce += (dir.magnitude - link.Edge.SpringParameters.Length) * link.Edge.SpringParameters.StiffnessCoefficient * dir.normalized;
                    }

                    if (sumForce.magnitude > eps)
                    {
                        list.Add((vertex, sumForce));
                        vertexMustMove = false;
                    }
                }
                foreach (var (veretx, force) in list)
                {
                    veretx.MovementComponent.GlobalCoordinates += force * speed * Time.deltaTime;
                }
                list.Clear();
                yield return null;
            }
            _isChanging = false;
        }

        public abstract bool ContainsVertex (string id);

        public abstract bool DeleteVeretex (string id);

        public GraphParameters DownloadParams (Dictionary<Guid, object> writeCache)
        {
            var vertexParameters = new List<VertexInfo>();
            var links = new List<LinkInfo>();

            foreach (var vertex in GetVertexes())
            {
                vertexParameters.Add((vertex.GetType(), (AbstractVertexParameters) CustomizableExtension.CallDownloadParams(vertex, writeCache)));

                foreach (var outgoingLink in vertex.OutgoingLinks)
                {
                    links.Add((vertex.Id, outgoingLink.AdjacentVertex.Id, outgoingLink.Edge.GetType(), (EdgeParameters) CustomizableExtension.CallDownloadParams(outgoingLink.Edge, writeCache)));
                }
            }

            return new GraphParameters(MovementComponent.GlobalCoordinates, Name, vertexParameters, links, Id);
        }

        public abstract AbstractVertex GetVertexById (string id);

        public abstract IReadOnlyList<AbstractVertex> GetVertexes ();

        public void SetupParams (GraphParameters parameters)
        {
            MovementComponent.GlobalCoordinates = parameters.Position;

            Name = parameters.Name;

            Id = parameters.ObjectId;

            if (parameters.VertexParameters != null)
            {
                foreach (var (vertexType, vertexParameters) in parameters.VertexParameters)
                {
                    SpawnVertex(vertexType, vertexParameters);
                }
            }

            if (parameters.Links != null)
            {
                foreach (var (firstVertexId, secondVertexId, edgeType, edgeParams) in parameters.Links)
                {
                    GetVertexById(firstVertexId).Link(GetVertexById(secondVertexId), edgeType, edgeParams);
                }
            }
        }

        public abstract TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters)
                    where TVertex : AbstractVertex, new()
                    where TParams : AbstractVertexParameters;

        public abstract AbstractVertex SpawnVertex (Type vertexType, AbstractVertexParameters parameters);

        [ContextMenu("ForceBasedLayout")]
        public void StartForceBasedLayout ()
        {
            if (!_isChanging)
                StartCoroutine(ForceBasedLayoutCoroutine());
        }
    }

    /// <summary>
    /// Class that describes default graph parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class GraphParameters : AbstractGraphObjectParameters
    {
        public List<LinkInfo>? Links { get; protected set; }
        public string? Name { get; protected set; }
        public Vector3 Position { get; protected set; }
        public List<VertexInfo>? VertexParameters { get; protected set; }

        public GraphParameters (Vector3 position = default,
                                string? name = default,
                                List<VertexInfo>? vertexParameters = default,
                                List<LinkInfo>? links = default,
                                string? id = default) : base(id)
        {
            Position = position;
            Name = name;
            Links = links;
            if (vertexParameters != null)
            {
                VertexParameters = vertexParameters;
                foreach (var (vertexType, _) in VertexParameters)
                {
                    if (!vertexType.IsSubclassOf(typeof(AbstractVertex)))
                        throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);
                }
            }
        }
    }
}