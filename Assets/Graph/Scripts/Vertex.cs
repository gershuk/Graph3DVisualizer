using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
{
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

    public class Vertex : MonoBehaviour
    {
        private Transform _transform;
        public BillboardControler BillboardControler { get; private set; }

        private void Awake ()
        {
            _transform = GetComponent<Transform>();
            BillboardControler = GetComponent<BillboardControler>();
        }
    }
}
