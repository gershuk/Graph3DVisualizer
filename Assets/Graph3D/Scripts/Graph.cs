using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
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
