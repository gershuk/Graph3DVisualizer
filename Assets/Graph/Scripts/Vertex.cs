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
        {
            SourceOffsetDist = sourceOffsetDist;
            TargetOffsetDist = targetOffsetDist;
            ArrowTexture = arrowTexture;
            LineTexture = lineTexture;
        }
    }

    public class Link
    {
        public Vertex AdjacentVertex { get; }
        public IEdge Edge { get; }

        public Link (Vertex adjacentVertex, IEdge edge)
        {
            AdjacentVertex = adjacentVertex != null ? adjacentVertex : throw new ArgumentNullException(nameof(adjacentVertex));
            Edge = edge ?? throw new ArgumentNullException(nameof(edge));
        }
    }

    public readonly struct VertexParameters
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public VertexParameters (Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    public interface IVertex
    {
        BillboardControler BillboardControler { get; }
        MoveComponent MoveComponent { get; }
        bool Visibility { get; set; }

        T Link<T> (Vertex toVertex, LinkParameters linkParameters) where T : MonoBehaviour, IEdge, new();
        void SetUpVertex (VertexParameters vertexParameters);
        void UnLink<T> (Vertex toVertex) where T : MonoBehaviour, IEdge;
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

        public BillboardControler BillboardControler { get; private set; }
        public MoveComponent MoveComponent { get; private set; }

        public event Action<UnityEngine.Object> OnDestroyed;
        public event Action<bool, UnityEngine.Object> OnVisibleChange;

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

        private T RemoveLinkFromArray<T> (List<Link> links, Vertex toVertex) where T : IEdge
        {
            for (var i = 0; i < links.Count; ++i)
            {
                if (links[i].AdjacentVertex == toVertex && links[i].Edge.GetType() == typeof(T))
                {
                    var link = links[i];
                    links[i] = links[links.Count - 1];
                    links.RemoveAt(links.Count - 1);
                    return (T) link.Edge;
                }
            }
            throw new LinkNotFoundException();
        }

        private T CreateEdge<T> (EdgeParameters parameters) where T : MonoBehaviour, IEdge, new()
        {
            var edge = Instantiate(_edgePrefab, transform.position, Quaternion.identity, transform.parent).AddComponent<T>();
            edge.SetUpEdge(parameters);
            return edge;
        }

        public void SetUpVertex (VertexParameters vertexParameters)
        {
            transform.position = vertexParameters.Position;
            transform.rotation = vertexParameters.Rotation;
        }

        public T Link<T> (Vertex toVertex, LinkParameters linkParameters) where T : MonoBehaviour, IEdge, new()
        {
            foreach (var link in _outgoingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == typeof(T))
                {
                    throw new Exception("Link already exist");
                }
            }

            IEdge edge = null;

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

            edge = edge ?? CreateEdge<T>(edgeParameters);
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));

            return (T) edge;
        }

        public void UnLink<T> (Vertex toVertex) where T : MonoBehaviour, IEdge
        {
            toVertex.RemoveLinkFromArray<T>(toVertex._incomingLinks, this);
            var edge = RemoveLinkFromArray<T>(_outgoingLinks, toVertex);

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
    }
}
