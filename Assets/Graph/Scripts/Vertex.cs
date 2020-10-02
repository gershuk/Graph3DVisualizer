using System;
using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
{
    public class LinkNotFoundException : Exception
    {

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

    [RequireComponent(typeof(BillboardControler))]
    public class Vertex : MonoBehaviour, IMove, IVisibile, IDestoryed
    {
        [SerializeField]
        private GameObject _edgePrefab;
        private Transform _transform;
        private List<Link> _incomingLinks;
        private List<Link> _outgoingLinks;
        private bool _visible;

        public BillboardControler BillboardControler { get; private set; }

        public event Action<Vector3, UnityEngine.Object> OnMove;
        public event Action<UnityEngine.Object> OnDestroyed;
        public event Action<bool,UnityEngine.Object> OnVisibleChange;

        private void Awake ()
        {
            _visible = true;
            _edgePrefab = _edgePrefab == null ? (GameObject) Resources.Load("Prefabs/Edge") : _edgePrefab;
            _incomingLinks = new List<Link>();
            _outgoingLinks = new List<Link>();
            _transform = GetComponent<Transform>();
            BillboardControler = GetComponent<BillboardControler>();
        }

        private void OnDestroy () => OnDestroyed?.Invoke(this);

        private T RemoveLinkFromArray<T> (List<Link> links, Vertex toVertex) where T : Edge
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

        private T CreateEdge<T> (EdgeParameters parameters) where T : Edge, new()
        {
            var edge = Instantiate(_edgePrefab, parameters.Position, Quaternion.identity, _transform.parent).AddComponent<T>();
            edge.SetUpEdge(parameters);
            return edge;
        }

        public void SetUpVertex (VertexParameters vertexParameters)
        {
            _transform.position = vertexParameters.Position;
            _transform.rotation = vertexParameters.Rotation;
        }

        public void SetGlobalCoordinates (Vector3 coordinates)
        {
            _transform.position = coordinates;
            OnMove?.Invoke(coordinates, this);
        }

        public void SetLocalCoordinates (Vector3 coordinates)
        {
            _transform.localPosition = coordinates;
            OnMove?.Invoke(coordinates, this);
        }

        public void Link<T> (Vertex toVertex) where T : Edge
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


            var parameters = new EdgeParameters((_transform.position + toVertex._transform.position) / 2, 1, 1, new AdjacentVertices(this, toVertex), null, null);
            edge = (edge == null) ? CreateEdge<Edge>(parameters) : edge;
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));
        }

        public void UnLink<T> (Vertex toVertex) where T : Edge
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

        public void SetVisibility (bool value)
        {
            if (_visible != value)
            {
                _visible = value;
                BillboardControler.SetVisibility(value);
                OnVisibleChange?.Invoke(value, this);
            }
        }

        public bool GetVisibility () => _visible;
    }
}
