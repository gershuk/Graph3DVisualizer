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

        public int Width { get; }
        public int Height { get; }

        public float ScaleX { get; }
        public float ScaleY { get; }

        public float Cutoff { get; }

        public (Texture2D Texture, Vector2Int Position)[] Images { get; }

        public VertexParameters (Vector3 position, Quaternion rotation, int width, int height, float scaleX, float scaleY, float cutoff,(Texture2D Texture, Vector2Int Position)[] images)
        {
            Position = position;
            Rotation = rotation;
            Width = width;
            Height = height;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Cutoff = cutoff;
            Images = images ?? throw new ArgumentNullException(nameof(images));
        }
    }

    public class Vertex : MonoBehaviour
    {
        private Transform _transform;
        public BillboardControler BillboardControler { get; private set; }

        private void Awake ()
        {
            BillboardControler = GetComponent<BillboardControler>();
        }
    }
}
