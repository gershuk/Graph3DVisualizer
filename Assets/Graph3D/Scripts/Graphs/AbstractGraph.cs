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

        private IEnumerator ForceBasedLayoutCoroutine (int maxIterationCount = 10000)
        {
            if (_isChanging)
                yield break;

            const float maxSpeed = 1e5F;
            const int maxCounter = 400;
            const float eps = 0.1f;
            var vertexesMustMove = true;
            var vertexes = GetVertexes();
            var speeds = new Vector3[vertexes.Count];
            var iterationCounter = 0;

            static void CalcForce (AbstractVertex vertex, ref Vector3 speed)
            {
                var sumForce = Vector3.zero;

                foreach (var link in vertex.IncomingLinks.Concat(vertex.OutgoingLinks))
                {
                    var dir = link.AdjacentVertex.transform.position - vertex.transform.position;
                    sumForce += (dir.magnitude - link.Edge.SpringParameters.Length) * link.Edge.SpringParameters.StiffnessCoefficient * dir.normalized;
                }

                speed += sumForce / vertex.Weight * Time.deltaTime;

                if (speed.magnitude > maxSpeed)
                    speed = speed.normalized * maxSpeed;
            }

            _isChanging = true;
            try
            {
                while (vertexesMustMove && iterationCounter < maxIterationCount)
                {
                    ++iterationCounter;
                    vertexesMustMove = false;
                    for (var i = 0; i < vertexes.Count; ++i)
                    {
                        CalcForce(vertexes[i], ref speeds[i]);
                        if (i != 0 && i % maxCounter == 0)
                            yield return null;
                    }

                    for (var j = 0; j < vertexes.Count; ++j)
                    {
                        if (speeds[j].magnitude > eps)
                        {
                            vertexes[j].MovementComponent.GlobalCoordinates += speeds[j] * Time.deltaTime;
                            speeds[j] = Vector3.zero;
                            vertexesMustMove = true;
                        }

                        if (j != 0 && j % maxCounter == 0)
                            yield return null;
                    }
                    yield return null;
                }
            }
            finally
            {
                _isChanging = false;
            }
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

        public void StartForceBasedLayout (int maxIterationCount = 10000)
        {
            if (!_isChanging)
                StartCoroutine(ForceBasedLayoutCoroutine(maxIterationCount));
        }

        [ContextMenu("ForceBasedLayout")]
        public void StartFroceBaseFromMenu () => StartForceBasedLayout();

        [ContextMenu("SpectralLayout")]
        public void StartSpectralLayout ()
        {
            if (_isChanging)
                return;
            _isChanging = true;
            try
            {
                var vertexes = GetVertexes();
                var idToIndexDictionary = new Dictionary<string, int>(vertexes.Count);
                for (var i = 0; i < vertexes.Count; i++)
                {
                    var vertex = vertexes[i];
                    idToIndexDictionary[vertex.Id] = i;
                }
                var matrix = new alglib.complex[vertexes.Count, vertexes.Count];
                for (var i = 0; i < vertexes.Count; i++)
                {
                    var vertex = vertexes[i];
                    var adjVertexSet = new HashSet<AbstractVertex>(vertex.IncomingLinks.Concat(vertex.OutgoingLinks).Select(l => l.AdjacentVertex));
                    matrix[i, i] = adjVertexSet.Count;
                    foreach (var adjVeretex in adjVertexSet)
                    {
                        matrix[i, idToIndexDictionary[adjVeretex.Id]] = -1;
                        matrix[idToIndexDictionary[adjVeretex.Id], i] = -1;
                    }
                }

                alglib.hmatrixevd(matrix, vertexes.Count, 1, true, out var d, out var z);

                var scale = vertexes.Count * 60;
                for (var i = 0; i < vertexes.Count; ++i)
                {
                    vertexes[i].MovementComponent.LocalCoordinates = new Vector3((float) z[i, vertexes.Count - 1].x, (float) z[i, vertexes.Count - 2].x, (float) z[i, vertexes.Count - 3].x) * scale;
                }
            }
            finally
            {
                _isChanging = false;
            }
        }

        #region PyramidLayout

        private class VertexRing
        {
            public List<VertexRing>? ChildRings { get; set; }
            public AbstractVertex Owner { get; private set; }

            public VertexRing? Parent { get; private set; }

            public float Radius { get; private set; }

            public VertexRing (AbstractVertex owner, VertexRing? parent, float radius = 0f) => (Owner, Parent, Radius) = (owner, parent, radius);

            public void CalcRadius (float minRingRadius = 1f, float reserve = 0.05f)
            {
                Radius = minRingRadius;
                if (ChildRings != null && ChildRings.Count > 1)
                {
                    var maxChildRadius = 0f;
                    foreach (var ring in ChildRings)
                        maxChildRadius = Mathf.Max(maxChildRadius, ring.Radius);

                    Radius = maxChildRadius / Mathf.Sin(Mathf.PI / ChildRings.Count) + reserve;
                }

                if (ChildRings != null && ChildRings.Count == 1)
                    Radius = ChildRings[0].Radius;
            }

            public void FindChildren (HashSet<AbstractVertex>? bannedAbstractVertexes = default)
            {
                var adjVertexSet = new HashSet<AbstractVertex>(Owner.IncomingLinks.Concat(Owner.OutgoingLinks).Select(l => l.AdjacentVertex));
                ChildRings = new List<VertexRing>();
                foreach (var vertex in adjVertexSet)
                {
                    if (bannedAbstractVertexes == null || !bannedAbstractVertexes.Contains(vertex))
                        ChildRings.Add(new VertexRing(vertex, this, 0));
                }
            }
        }

        public void PyramidLayout (AbstractVertex? firstVertex = default, float minRingRadius = 15f, float hight = 20f)
        {
            if (_isChanging)
                return;
            _isChanging = true;
            try
            {
                var visitedVertex = new HashSet<AbstractVertex>();

                if (firstVertex == null)
                    firstVertex = GetVertexes()[0];
                else if (firstVertex.transform.parent != _transform.parent)
                    throw new Exception();

                F(firstVertex);

                foreach (var vertex in GetVertexes())
                {
                    if (!visitedVertex.Contains(vertex))
                        F(vertex);
                }

                void F (AbstractVertex startVertex)
                {
                    var rings = new List<VertexRing>(VertexesCount);
                    var l = 0;
                    var r = 0;

                    rings.Add(new VertexRing(startVertex, null));
                    visitedVertex.Add(startVertex);
                    while (l <= r)
                    {
                        rings[l].FindChildren(visitedVertex);
                        if (rings[l].ChildRings != null)
                        {
                            for (var i = 0; i < rings[l].ChildRings.Count; ++i)
                            {
                                ++r;
                                visitedVertex.Add(rings[l].ChildRings[i].Owner);
                                rings.Add(rings[l].ChildRings[i]);
                            }
                        }
                        ++l;
                    }

                    for (var i = r; i >= 0; --i)
                        rings[i].CalcRadius(minRingRadius);
                    foreach (var baseRing in rings)
                    {
                        if (baseRing.ChildRings != null)
                        {
                            if (baseRing.ChildRings.Count == 1)
                            {
                                baseRing.ChildRings[0].Owner.MovementComponent.GlobalCoordinates = baseRing.Owner.MovementComponent.GlobalCoordinates + new Vector3(0, hight);
                                continue;
                            }

                            for (var i = 0; i < baseRing.ChildRings.Count; i++)
                            {
                                var childRing = baseRing.ChildRings[i];
                                var localPos = new Vector3(baseRing.Radius * Mathf.Cos(2 * Mathf.PI / baseRing.ChildRings.Count * i),
                                                           hight,
                                                           baseRing.Radius * Mathf.Sin(2 * Mathf.PI / baseRing.ChildRings.Count * i));
                                childRing.Owner.MovementComponent.GlobalCoordinates = baseRing.Owner.MovementComponent.GlobalCoordinates + localPos;
                            }
                        }
                    }
                }
            }
            finally
            {
                _isChanging = false;
            }
        }

        [ContextMenu("PyramidLayout")]
        public void StartPyramidLayout () => PyramidLayout();

        #endregion PyramidLayout
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