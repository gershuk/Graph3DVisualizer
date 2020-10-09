using System;

using Grpah3DVisualser;

using TextureFactory;

using UnityEngine;

public class Tester : MonoBehaviour
{
    public Texture2D Texture;
    public GameObject Graph;
    public Font CustomFont;
    public TextTextureFactory TextTextureFactory;

    void Start ()
    {
        var graphControler = Graph.GetComponent<Graph>();
        Vertex lastVertex = null;

        TextTextureFactory = new TextTextureFactory(CustomFont, 32);
        var resizedTetxure = Texture2DExtension.ResizeTexture(Texture, 200, 200);

        for (var i = 0; i < 1000; ++i)
        {
            var text = TextTextureFactory.MakeTextTexture($"Vertex{i}");
            text = Texture2DExtension.ResizeTexture(text, 200, (int) (Math.Truncate(200.0 / text.width + 1)) * text.height);

            var image = new (Texture2D, Vector2Int Position)[2] { (resizedTetxure, new Vector2Int(0, text.height)), (text, new Vector2Int(0, 0)) };

            var width = resizedTetxure.width;
            var height = resizedTetxure.height + text.height;
            var scale = 10f;

            var verPar = new VertexParameters(new Vector3(i % 30 * 20, i / 30 * 20, 0), Quaternion.identity);
            var billPar = new BillboardParameters(image, width, height, scale, (height * scale) / width, 0.1f, true, TextureWrapMode.Clamp);
            var currentVertex = graphControler.SpawnVertex<Vertex>(in verPar, in billPar);
            var linkParameters = new LinkParameters(6, 6);
            lastVertex?.Link<Edge>(currentVertex, linkParameters);
            if (lastVertex != null && i % 2 == 0)
                currentVertex.Link<Edge>(lastVertex, linkParameters);
            lastVertex = currentVertex;
        }
    }
}
