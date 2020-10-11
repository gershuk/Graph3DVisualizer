using System;
using System.Collections.Generic;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualser
{
    public class LinkNotFoundException : Exception
    { }

    public readonly struct LinkParameters
    {
        public float SourceOffsetDist { get; }
        public float TargetOffsetDist { get; }
        public Texture2D ArrowTexture { get; }
        public Texture2D LineTexture { get; }

        public LinkParameters (float sourceOffsetDist, float targetOffsetDist, Texture2D arrowTexture = null, Texture2D lineTexture = null)
            => (SourceOffsetDist, TargetOffsetDist, ArrowTexture, LineTexture) = (sourceOffsetDist, targetOffsetDist, arrowTexture, lineTexture);
    }

    public class Link
    {
        public Vertex AdjacentVertex { get; }
        public Edge Edge { get; }

        public Link (Vertex adjacentVertex, Edge edge)
        {
            AdjacentVertex = adjacentVertex != null ? adjacentVertex : throw new ArgumentNullException(nameof(adjacentVertex));
            Edge = edge != null ? edge : throw new ArgumentNullException(nameof(edge));
        }
    }

    public readonly struct VertexParameters
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector2 Size { get; }

        public VertexParameters (Vector3 position, Quaternion rotation, Vector2 size)
            => (Position, Rotation, Size) = (position, rotation, size);
    }

    public interface IVertex
    {
        BillboardControler BillboardControler { get; }
        MoveComponent MoveComponent { get; }
        bool Visibility { get; set; }
        Vector2 Size { get; set; }

        T Link<T> (Vertex toVertex, in LinkParameters linkParameters) where T : Edge, new();
        void SetUpVertex (in VertexParameters vertexParameters);
        void UnLink (Vertex toVertex, Type type);
    }

    [RequireComponent(typeof(BillboardControler))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(SphereCollider))]
    public class Vertex : MonoBehaviour, IVertex, IVisibile, IDestructible
    {
        [SerializeField]
        private GameObject _edgePrefab;
        private List<Link> _incomingLinks;
        private List<Link> _outgoingLinks;
        private bool _visible;
        private Vector2 _size;

        public event Action<UnityEngine.Object> OnDestroyed;
        public event Action<bool, UnityEngine.Object> OnVisibleChange;

        //ToDo make BillboardControler privates
        public BillboardControler BillboardControler { get; private set; }
        public MoveComponent MoveComponent { get; private set; }

        public bool Visibility
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    BillboardControler.Visibility = value;
                    OnVisibleChange?.Invoke(value, this);
                }
            }
            get => _visible;
        }

        public Vector2 Size
        {
            get => _size;
            set
            {
                if (_size != value)
                {
                    _size = value;
                    BillboardControler.ScaleX = _size.x;
                    BillboardControler.ScaleY = _size.y;
                }
            }
        }

        private void Awake ()
        {
            _visible = true;
            _edgePrefab = _edgePrefab == null ? (GameObject) Resources.Load("Prefabs/Edge") : _edgePrefab;
            _incomingLinks = new List<Link>();
            _outgoingLinks = new List<Link>();
            BillboardControler = GetComponent<BillboardControler>();
            MoveComponent = GetComponent<MoveComponent>();
        }

        private void OnDestroy () => OnDestroyed?.Invoke(this);

        private Edge RemoveLinkFromArray (List<Link> links, Vertex toVertex, Type edgeType)
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

        private T CreateEdge<T> (in EdgeParameters parameters) where T : Edge, new()
        {
            var edge = Instantiate(_edgePrefab, transform.position, Quaternion.identity, transform.parent).AddComponent<T>();
            edge.SetUpEdge(parameters);
            return edge;
        }

        public void SetUpVertex (in VertexParameters vertexParameters)
            => (transform.position, transform.rotation) = (vertexParameters.Position, vertexParameters.Rotation);

        public T Link<T> (Vertex toVertex, in LinkParameters linkParameters) where T : Edge, new()
        {
            foreach (var link in _outgoingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == typeof(T))
                {
                    throw new Exception("Link already exist");
                }
            }

            Edge edge = null;

            foreach (var link in _incomingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == typeof(T))
                {
                    edge = link.Edge;
                    edge.Type = EdgeType.Bidirectional;
                    break;
                }
            }

            var edgeParameters = new EdgeParameters(linkParameters.SourceOffsetDist, linkParameters.TargetOffsetDist,
                new AdjacentVertices(this, toVertex), linkParameters.ArrowTexture, linkParameters.LineTexture);

            edge = edge != null ? edge : CreateEdge<T>(edgeParameters);
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));

            return (T) edge;
        }

        public void UnLink (Vertex toVertex, Type edgeType)
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
}
