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
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Support class for edge serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    [YuzuAlias("LinkInfo")]
    public struct LinkInfo
    {
        public EdgeParameters edgeParameters;
        public Type edgeType;
        public string firstVertexId;
        public string secondVertexId;

        public LinkInfo (string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters)
        {
            this.firstVertexId = firstVertexId;
            this.secondVertexId = secondVertexId;
            this.edgeType = edgeType;
            this.edgeParameters = edgeParameters;
        }

        public static implicit operator (string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters) (LinkInfo value)
        {
            return (value.firstVertexId, value.secondVertexId, value.edgeType, value.edgeParameters);
        }

        public static implicit operator LinkInfo ((string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters) value)
        {
            return new LinkInfo(value.firstVertexId, value.secondVertexId, value.edgeType, value.edgeParameters);
        }

        public void Deconstruct (out string firstVertexId, out string secondVertexId, out Type edgeType, out EdgeParameters edgeParameters)
        {
            firstVertexId = this.firstVertexId;
            secondVertexId = this.secondVertexId;
            edgeType = this.edgeType;
            edgeParameters = this.edgeParameters;
        }

        public override bool Equals (object obj)
        {
            return obj is LinkInfo other &&
                   firstVertexId == other.firstVertexId &&
                   secondVertexId == other.secondVertexId &&
                   EqualityComparer<Type>.Default.Equals(edgeType, other.edgeType) &&
                   EqualityComparer<EdgeParameters>.Default.Equals(edgeParameters, other.edgeParameters);
        }

        public override int GetHashCode ()
        {
            var hashCode = -1737920732;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(firstVertexId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(secondVertexId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(edgeType);
            hashCode = hashCode * -1521134295 + EqualityComparer<EdgeParameters>.Default.GetHashCode(edgeParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Support class for vertex serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    [YuzuAlias("VertexInfo")]
    public struct VertexInfo
    {
        public VertexParameters vertexParameters;
        public Type vertexType;

        public VertexInfo (Type vertexType, VertexParameters vertexParameters)
        {
            this.vertexType = vertexType;
            this.vertexParameters = vertexParameters;
        }

        public static implicit operator (Type vertexType, VertexParameters vertexParameters) (VertexInfo value)
        {
            return (value.vertexType, value.vertexParameters);
        }

        public static implicit operator VertexInfo ((Type vertexType, VertexParameters vertexParameters) value)
        {
            return new VertexInfo(value.vertexType, value.vertexParameters);
        }

        public void Deconstruct (out Type vertexType, out VertexParameters vertexParameters)
        {
            vertexType = this.vertexType;
            vertexParameters = this.vertexParameters;
        }

        public override bool Equals (object obj)
        {
            return obj is VertexInfo other &&
                   EqualityComparer<Type>.Default.Equals(vertexType, other.vertexType) &&
                   EqualityComparer<VertexParameters>.Default.Equals(vertexParameters, other.vertexParameters);
        }

        public override int GetHashCode ()
        {
            var hashCode = -2103449114;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(vertexType);
            hashCode = hashCode * -1521134295 + EqualityComparer<VertexParameters>.Default.GetHashCode(vertexParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Abstract class that describes graph component.
    /// </summary>
    [CustomizableGrandType(Type = typeof(GraphParameters))]
    public abstract class AbstractGraph : AbstractGraphObject, ICustomizable<GraphParameters>
    {
        protected Transform _transform;

        [SerializeField]
        protected GameObject _vertexPrefab;

        public abstract int VertexesCount { get; }

        public abstract bool ContainsVertex (string id);

        public abstract bool DeleteVeretex (string id);

        //ToDo get links
        public GraphParameters DownloadParams ()
        {
            var vertexParameters = new List<VertexInfo>();
            var links = new List<LinkInfo>();

            foreach (var vertex in GetVertexes())
            {
                vertexParameters.Add((vertex.GetType(), (VertexParameters) CustomizableExtension.CallDownloadParams(vertex)));

                foreach (var outgoingLink in vertex.OutgoingLinks)
                {
                    links.Add((vertex.Id, outgoingLink.AdjacentVertex.Id, outgoingLink.Edge.GetType(), (EdgeParameters) CustomizableExtension.CallDownloadParams(outgoingLink.Edge)));
                }

                //foreach (var incomingLink in vertex.IncomingLinks)
                //{
                //    links.Add((incomingLink.AdjacentVertex.Id, vertex.Id, incomingLink.Edge.GetType(), (EdgeParameters) CustomizableExtension.CallDownloadParams(incomingLink.Edge)));
                //}
            }

            return new GraphParameters(vertexParameters.ToArray(), links, Id);
        }

        public abstract AbstractVertex GetVertexById (string id);

        public abstract IReadOnlyList<AbstractVertex> GetVertexes ();

        public void SetupParams (GraphParameters parameters)
        {
            Id = parameters.Id;

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
                    where TParams : VertexParameters;

        public abstract AbstractVertex SpawnVertex (Type vertexType, VertexParameters parameters);
    }

    /// <summary>
    /// Class that describes default graph parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    public class GraphParameters : AbstractGraphObjectParameters
    {
        public List<LinkInfo> Links { get; }
        public VertexInfo[] VertexParameters { get; }

        public GraphParameters (VertexInfo[] vertexParameters = default,
                                List<LinkInfo> links = default,
                                string id = null) : base(id)
        {
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