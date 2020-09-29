﻿using Grpah3DVisualser;

using TextureFactory;

using UnityEngine;

public class Tester : MonoBehaviour
{
    public Texture2D Texture;
    public GameObject Graph;

    void Start ()
    {
        var graphControler = Graph.GetComponent<Graph>();
        for (var i = 0; i < 1000; ++i)
        {
            var image = new (Texture2D, Vector2Int Position)[2] { ( Texture2DExtension.ResizeTexture(Texture,100,100), new Vector2Int(0,0)),
                (Texture2DExtension.ResizeTexture(Texture, 100, 100), new Vector2Int(100, 100)) };
            var verPar = new VertexParameters(new Vector3(i, i, i), Quaternion.identity);
            var billPar = new BillboardParameters(200, 200, 1, 1, 0.9f, image);
            graphControler.SpawnVertex<Vertex>(in verPar, in billPar);
        }
    }
}
