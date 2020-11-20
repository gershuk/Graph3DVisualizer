using System;
using System.Collections.Generic;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualser
{
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

        public VertexParameters (Vector3 position, Quaternion rotation) => (Position, Rotation) = (position, rotation);
    }

    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(SphereCollider))]
    public class Vertex : MonoBehaviour, IVisibile, IDestructible, ISelectable
    {
        [SerializeField]
        private GameObject _edgePrefab;
        private List<Link> _incomingLinks;
        private List<Link> _outgoingLinks;
        private bool _visible = true;
        private bool _isSelected = true;
        private BillboardId _mainImageId;
        private BillboardId _selectFrameId;
        private SphereCollider _sphereCollider;
        private Transform _transform;

        public event Action<UnityEngine.Object> Destroyed;
        public event Action<bool, UnityEngine.Object> VisibleChanged;
        public event Action<UnityEngine.Object, bool> SelectedChanged;
        public event Action<UnityEngine.Object, bool> HighlightedChanged;

        private BillboardController _billboardControler;
        public MoveComponent MoveComponent { get; private set; }

        public bool Visibility
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    _billboardControler.Visibility = value;
                    VisibleChanged?.Invoke(value, this);
                }
            }
            get => _visible;
        }

        private void Awake ()
        {
            _transform = transform;
            _sphereCollider = GetComponent<SphereCollider>();
            _visible = true;
            _edgePrefab = _edgePrefab == null ? (GameObject) Resources.Load("Prefabs/Edge") : _edgePrefab;
            _incomingLinks = new List<Link>();
            _outgoingLinks = new List<Link>();
            _billboardControler = GetComponent<BillboardController>();
            MoveComponent = GetComponent<MoveComponent>();
        }

        private void OnDestroy () => Destroyed?.Invoke(this);

        private void UpdateColliderRange ()
        {
            var newRadius = _sphereCollider.radius;
            if (_mainImageId != null)
            {
                var mainImage = _billboardControler.GetBillboard(_mainImageId);
                newRadius = Mathf.Max(mainImage.ScaleX / 2, mainImage.ScaleY / 2);
            }
            if (_selectFrameId != null)
            {
                var selectFrame = _billboardControler.GetBillboard(_selectFrameId);
                newRadius = Mathf.Max(selectFrame.ScaleX / 2, selectFrame.ScaleY / 2, newRadius);
            }
            _sphereCollider.radius = newRadius;
        }

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

        private Edge CreateEdge (in EdgeParameters parameters, Type edgeType)
        {
            if (!edgeType.IsSubclassOf(typeof(Edge)) && edgeType != typeof(Edge))
                throw new Exception($"This type {edgeType.FullName} is not inherited from an Edge");
            var edge = (Edge) Instantiate(_edgePrefab, _transform.position, Quaternion.identity, _transform.parent).AddComponent(edgeType);
            edge.SetUpEdge(parameters);
            return edge;
        }

        public void SetUpVertex (in VertexParameters vertexParameters)
            => (_transform.position, _transform.rotation) = (vertexParameters.Position, vertexParameters.Rotation);

        public void SetMainImage (in BillboardParameters billboardParameters)
        {
            if (_mainImageId != null)
                _billboardControler.DeleteBillboard(_mainImageId);
            _mainImageId = _billboardControler.CreateBillboard(billboardParameters, "MainImage", "Vertex image");
            UpdateColliderRange();
        }

        public void SetSelectFrame (in BillboardParameters billboardParameters)
        {
            if (_selectFrameId != null)
                _billboardControler.DeleteBillboard(_selectFrameId);
            _selectFrameId = _billboardControler.CreateBillboard(billboardParameters, "SelectFrameImage", "Vertex select frame");
            UpdateColliderRange();
        }

        public Edge Link (Vertex toVertex, Type edgeType, in LinkParameters linkParameters)
        {
            if (toVertex == this)
                throw new Exception($"It is forbidden to create edges from a vertex to the same vertex.");

            if (_transform.parent != toVertex._transform.parent)
                throw new Exception($"The vertices are in different graphs");

            foreach (var link in _outgoingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == edgeType)
                {
                    throw new Exception("Link already exist");
                }
            }

            Edge edge = null;

            foreach (var link in _incomingLinks)
            {
                if (link.AdjacentVertex == toVertex && link.Edge.GetType() == edgeType)
                {
                    edge = link.Edge;
                    edge.Type = EdgeType.Bidirectional;
                    break;
                }
            }

            var edgeParameters = new EdgeParameters(linkParameters.SourceOffsetDist, linkParameters.TargetOffsetDist,
                new AdjacentVertices(this, toVertex), linkParameters.ArrowTexture, linkParameters.LineTexture);

            edge = edge != null ? edge : CreateEdge(edgeParameters, edgeType);
            _outgoingLinks.Add(new Link(toVertex, edge));
            toVertex._incomingLinks.Add(new Link(this, edge));

            return edge;
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

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    if (value)
                        _billboardControler.EnableBillboard(_selectFrameId);
                    else
                        _billboardControler.DisableBillboard(_selectFrameId);

                    SelectedChanged?.Invoke(this, value);
                }
            }
        }

        public bool IsHighlighted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Color SelectFrameColor
        {
            get => _billboardControler.GetBillboard(_selectFrameId).MonoColor;
            set => _billboardControler.GetBillboard(_selectFrameId).MonoColor = value;
        }

        public Vector2 SetSelectFrameSize
        {
            get => new Vector2(_billboardControler.GetBillboard(_selectFrameId).ScaleX, _billboardControler.GetBillboard(_selectFrameId).ScaleY);
            set
            {
                _billboardControler.GetBillboard(_selectFrameId).ScaleX = value.x;
                _billboardControler.GetBillboard(_selectFrameId).ScaleY = value.y;
                UpdateColliderRange();
            }
        }

        public Vector2 SetMainImageSize
        {
            get => new Vector2(_billboardControler.GetBillboard(_mainImageId).ScaleX, _billboardControler.GetBillboard(_mainImageId).ScaleY);
            set
            {
                _billboardControler.GetBillboard(_mainImageId).ScaleX = value.x;
                _billboardControler.GetBillboard(_mainImageId).ScaleY = value.y;
                UpdateColliderRange();
            }
        }
    }
}
