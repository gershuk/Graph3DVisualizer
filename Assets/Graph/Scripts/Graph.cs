using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
{
    [ExecuteInEditMode]
    public class Graph : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vertexPrefab;
        private Transform _transform;
        private HashSet<Vertex> _vertexes;

        private void Awake ()
        {
            _vertexPrefab = _vertexPrefab == null ? (GameObject) Resources.Load("Prefabs/Vertex") : _vertexPrefab;
            _transform = GetComponent<Transform>();
            _vertexes = new HashSet<Vertex>();
        }

        public Vertex SpawnVertex<T> (in VertexParameters parameters) where T :Vertex
        {
            var vertex = Instantiate(_vertexPrefab, parameters.Position, parameters.Rotation, _transform);
            var vertexComponent = vertex.gameObject.AddComponent<T>();
            var billboardControler = vertexComponent.BillboardControler;

            billboardControler.SetBillboardTexture(parameters.Images, parameters.Width, parameters.Height);
            billboardControler.ScaleX = parameters.ScaleX;
            billboardControler.ScaleY = parameters.ScaleY;
            billboardControler.Cutoff = parameters.Cutoff;

            _vertexes.Add(vertexComponent);
            return vertexComponent;
        }

        public bool ContainsVertex (Vertex vertex) => _vertexes.Contains(vertex);

        public bool DeleteVeretex (Vertex vertex)
        {
            var result = _vertexes.Remove(vertex);
            if (result)
                Destroy(vertex.gameObject);
            return result;
        }
    }
}
