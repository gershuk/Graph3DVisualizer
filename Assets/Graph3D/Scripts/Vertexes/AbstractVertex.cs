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
using System.Linq;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Graph3D
{
    /// <summary>
    /// Abstract class that describes vertex component.
    /// </summary>
    [RequireComponent(typeof(MovementComponent))]
    [CustomizableGrandType(Type = typeof(VertexParameters))]
    public abstract class AbstractVertex : AbstractGraphObject, IVisibile, IDestructible, ICustomizable<VertexParameters>
    {
        protected BillboardController _billboardControler;

        [SerializeField]
        protected GameObject _edgePrefab;

        protected List<Link> _incomingLinks;
        protected List<Link> _outgoingLinks;
        protected Transform _transform;
        protected bool _visible = true;

        public abstract event Action<UnityEngine.Object> Destroyed;

        public abstract event Action<bool, UnityEngine.Object> VisibleChanged;

        public virtual IList<BillboardId> ImageIds { get; set; }
        public IReadOnlyList<Link> IncomingLinks => _incomingLinks;
        public abstract MovementComponent MovementComponent { get; protected set; }
        public IReadOnlyList<Link> OutgoingLinks => _outgoingLinks;
        public abstract bool Visibility { get; set; }

        private TEdge CreateEdge<TEdge, TParameters> (TParameters parameters, AbstractVertex toVertex) where TEdge : AbstractEdge where TParameters : EdgeParameters
        {
            var edge = Instantiate(_edgePrefab, _transform.position, Quaternion.identity, _transform.parent).AddComponent<TEdge>();
            edge.AdjacentVertices = new AdjacentVertices(this, toVertex);
            (edge as ICustomizable<TParameters>).SetupParams(parameters);
            return edge;
        }

        private AbstractEdge CreateEdge (EdgeParameters parameters, Type edgeType, AbstractVertex toVertex)
        {
            if (!edgeType.IsSubclassOf(typeof(AbstractEdge)))
                throw new WrongTypeInCustomizableParameterException(typeof(AbstractEdge), edgeType);
            var edge = (AbstractEdge) Instantiate(_edgePrefab, _transform.position, Quaternion.identity, _transform.parent).AddComponent(edgeType);
            edge.AdjacentVertices = new AdjacentVertices(this, toVertex);
            CustomizableExtension.CallSetUpParams(edge, parameters);
            return edge;
        }

        protected void CheckLinkForCorrectness (AbstractVertex toVertex, Type edgeType)
        {
            if (toVertex == this)
                throw new Exception("It is forbidden to create edges from a vertex to the same vertex.");

            if (_transform.parent != toVertex._transform.parent)
                throw new Exception("The vertices are in different graphs");

            foreach (var link in _outgoingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == edgeType)
                {
                    throw new Exception("Link already exist");
                }
            }
        }

        protected AbstractEdge FindOppositeEdge (AbstractVertex toVertex, Type edgeType)
        {
            AbstractEdge edge = null;

            foreach (var link in _incomingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == edgeType)
                {
                    edge = link.Edge;
                    edge.Type = EdgeType.Bidirectional;
                    break;
                }
            }

            return edge;
        }

        protected AbstractEdge RemoveLinkFromArray (List<Link> links, AbstractVertex toVertex, Type edgeType)
        {
            for (var i = 0; i < links.Count; ++i)
            {
                if (links[i].AdjacentVertex == toVertex && links[i].Edge.GetType() == edgeType)
                {
                    var link = links[i];
                    links[i] = links[links.Count - 1];
                    links.RemoveAt(links.Count - 1);
                    return link.Edge;
                }
            }
            throw new LinkNotFoundException();
        }

        public virtual BillboardId AddImage (BillboardParameters billboardParameters, string name, string description) =>
            _billboardControler.CreateBillboard(billboardParameters, name, description);

        public virtual void DeleteImage (BillboardId billboardId) => _billboardControler.DeleteBillboard(billboardId);

        public VertexParameters DownloadParams () => new VertexParameters(ImageIds.Select(id => (_billboardControler.GetBillboard(id).DownloadParams())).ToArray(), _transform.position, _transform.rotation, Id);

        public abstract Vector2 GetImageSize (BillboardId id);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0029:Используйте выражение объединения", Justification = "<Ожидание>")]
        public TEdge Link<TEdge, TParameters> (AbstractVertex toVertex, TParameters edgeParameters) where TEdge : AbstractEdge where TParameters : EdgeParameters
        {
            CheckLinkForCorrectness(toVertex, typeof(TEdge));
            var edge = (TEdge) FindOppositeEdge(toVertex, typeof(TEdge));
            edge = edge != null ? edge : CreateEdge<TEdge, TParameters>(edgeParameters, toVertex);
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));
            return edge;
        }

        public AbstractEdge Link (AbstractVertex toVertex, Type edgeType, EdgeParameters edgeParameters)
        {
            CheckLinkForCorrectness(toVertex, edgeType);
            var edge = FindOppositeEdge(toVertex, edgeType);
            edge = edge != null ? edge : CreateEdge(edgeParameters, edgeType, toVertex);
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));
            return edge;
        }

        public abstract void SetImageSize (BillboardId id, Vector2 vector2);

        public virtual void SetupParams (VertexParameters parameters)
        {
            Id = parameters.Id;

            (_transform.position, _transform.rotation) = (parameters.Position, parameters.Rotation);
            foreach (var param in parameters.ImageParameters)
                ImageIds.Add(AddImage(param, param.Name, param.Description));
        }

        public void UnLink<TEdge> (AbstractVertex toVertex) where TEdge : AbstractEdge => UnLink(toVertex, typeof(SpriteEdge));

        public void UnLink (AbstractVertex toVertex, Type edgeType)
        {
            toVertex.RemoveLinkFromArray(toVertex._incomingLinks, this, edgeType);
            var edge = RemoveLinkFromArray(_outgoingLinks, toVertex, edgeType);

            if (edge.Type == EdgeType.Bidirectional)
            {
                edge.Type = EdgeType.Unidirectional;
                edge.AdjacentVertices = new AdjacentVertices(toVertex, this);
            }
            else
            {
                Destroy(edge.gameObject);
            }
        }
    }

    /// <summary>
    /// Abstract class that describes unidirectional link between to current and adjacent vertexes.
    /// </summary>
    public class Link
    {
        public AbstractVertex AdjacentVertex { get; }
        public AbstractEdge Edge { get; }

        public Link (AbstractVertex adjacentVertex, AbstractEdge edge)
        {
            AdjacentVertex = adjacentVertex != null ? adjacentVertex : throw new ArgumentNullException(nameof(adjacentVertex));
            Edge = edge != null ? edge : throw new ArgumentNullException(nameof(edge));
        }
    }

    public class LinkNotFoundException : Exception
    {
        public LinkNotFoundException () : base()
        {
        }

        public LinkNotFoundException (string message) : base(message)
        {
        }

        public LinkNotFoundException (string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Class that describes default vertex parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class VertexParameters : AbstractGraphObjectParameters
    {
        public BillboardParameters[] ImageParameters { get; protected set; }
        public Vector3 Position { get; protected set; }
        public Quaternion Rotation { get; protected set; }

        public VertexParameters (BillboardParameters[] imageParameters, Vector3 position = default, Quaternion rotation = default, string id = null) : base(id)
            => (ImageParameters, Position, Rotation) = (imageParameters, position, rotation);
    }
}