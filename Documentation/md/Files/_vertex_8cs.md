---
title: Assets/Graph3D/Scripts/Vertex.cs


---

# Assets/Graph3D/Scripts/Vertex.cs







## Namespaces

| Name           |
| -------------- |
| **[Grpah3DVisualizer](Namespaces/namespace_grpah3_d_visualizer.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Grpah3DVisualizer::LinkNotFoundException](Classes/class_grpah3_d_visualizer_1_1_link_not_found_exception.md)**  |
| struct | **[Grpah3DVisualizer::LinkParameters](Classes/struct_grpah3_d_visualizer_1_1_link_parameters.md)**  |
| class | **[Grpah3DVisualizer::Link](Classes/class_grpah3_d_visualizer_1_1_link.md)**  |
| class | **[Grpah3DVisualizer::VertexParameters](Classes/class_grpah3_d_visualizer_1_1_vertex_parameters.md)**  |
| class | **[Grpah3DVisualizer::Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualizer
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

    public class VertexParameters
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public BillboardParameters ImageParameters { get; }
        public BillboardParameters SelectFrameParameters { get; }

        public VertexParameters (Vector3 position, Quaternion rotation, BillboardParameters imageParameters, BillboardParameters selectFrameParameters)
            => (Position, Rotation, ImageParameters, SelectFrameParameters) = (position, rotation, imageParameters, selectFrameParameters);
    }

    [RequireComponent(typeof(BillboardController))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(SphereCollider))]
    public class Vertex : MonoBehaviour, IVisibile, IDestructible, ISelectable, ICustomizable<VertexParameters>
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
            edge.SetupParams(parameters);
            return edge;
        }

        public void SetMainImage (BillboardParameters billboardParameters)
        {
            if (_mainImageId == null)
                _mainImageId = _billboardControler.CreateBillboard(billboardParameters, "MainImage", "Vertex image");
            else
                _billboardControler.GetBillboard(_mainImageId).SetupParams(billboardParameters);
            UpdateColliderRange();
        }

        public void SetSelectFrame (BillboardParameters billboardParameters)
        {
            if (_selectFrameId == null)
                _selectFrameId = _billboardControler.CreateBillboard(billboardParameters, "SelectFrameImage", "Vertex select frame");
            else
                _billboardControler.GetBillboard(_selectFrameId).SetupParams(billboardParameters);
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

            var edgeParameters = new EdgeParameters(new AdjacentVertices(this, toVertex), linkParameters.SourceOffsetDist, linkParameters.TargetOffsetDist,
                 linkParameters.ArrowTexture, linkParameters.LineTexture);

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

        public void SetupParams (VertexParameters parameters)
        {
            (_transform.position, _transform.rotation) = (parameters.Position, parameters.Rotation);
            SetMainImage(parameters.ImageParameters);
            SetSelectFrame(parameters.SelectFrameParameters);
        }

        public VertexParameters DownloadParams () => new VertexParameters(_transform.position, _transform.rotation,
            _billboardControler.GetBillboard(_mainImageId).DownloadParams(), _billboardControler.GetBillboard(_selectFrameId).DownloadParams());

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
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
