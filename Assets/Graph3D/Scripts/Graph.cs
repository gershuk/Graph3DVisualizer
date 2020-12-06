// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
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

using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualizer
{
    public class Graph : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vertexPrefab;
        private Transform _transform;
        private HashSet<Vertex> _vertexes;

        public HashSet<Vertex> Vertexes { get => _vertexes; private set => _vertexes = value; }

        private void Awake ()
        {
            _vertexPrefab = _vertexPrefab == null ? (GameObject) Resources.Load("Prefabs/Vertex") : _vertexPrefab;
            _transform = GetComponent<Transform>();
            Vertexes = new HashSet<Vertex>();
        }

        public T SpawnVertex<T> (VertexParameters vertexParameters) where T : Vertex, new()
        {
            var vertex = Instantiate(_vertexPrefab, vertexParameters.Position, vertexParameters.Rotation, _transform);
            var vertexComponent = vertex.gameObject.AddComponent<T>();

            vertexComponent.SetMainImage(vertexParameters.ImageParameters);
            vertexComponent.SetSelectFrame(vertexParameters.SelectFrameParameters);

            vertexComponent.IsSelected = false;

            Vertexes.Add(vertexComponent);
            return vertexComponent;
        }

        public bool ContainsVertex (Vertex vertex) => Vertexes.Contains(vertex);

        public bool DeleteVeretex (Vertex vertex)
        {
            var result = Vertexes.Remove(vertex);
            if (result)
                Destroy(vertex.gameObject);
            return result;
        }
    }
}
