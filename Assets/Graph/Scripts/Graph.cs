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

        public Vertex SpawnVertex<T> (in VertexParameters vertexParameters, in BillboardParameters billboardParameters) where T :Vertex
        {
            var vertex = Instantiate(_vertexPrefab, vertexParameters.Position, vertexParameters.Rotation, _transform);
            var vertexComponent = vertex.gameObject.AddComponent<T>();
            var billboardControler = vertexComponent.BillboardControler;

            billboardControler.SetUpBillboard(billboardParameters);
            
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
