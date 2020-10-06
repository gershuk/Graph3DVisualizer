using Grpah3DVisualser;

using TextureFactory;

using UnityEngine;

public class Tester : MonoBehaviour
{
    public Texture2D Texture;
    public GameObject Graph;

    void Start ()
    {
        var graphControler = Graph.GetComponent<Graph>();
        Vertex lastVertex = null;

        var image = new (Texture2D, Vector2Int Position)[2] { ( Texture2DExtension.ResizeTexture(Texture,100,100), new Vector2Int(0,0)),
                (Texture2DExtension.ResizeTexture(Texture, 100, 100), new Vector2Int(100, 100)) };

        for (var i = 0; i < 1000; ++i)
        {
            var verPar = new VertexParameters(new Vector3(i * 20, 0, 0), Quaternion.identity);
            var billPar = new BillboardParameters(image, 200, 200, 1, 1, 1f, true);
            var currentVertex = graphControler.SpawnVertex<Vertex>(in verPar, in billPar);
            var linkParameters = new LinkParameters(1, 1);
            lastVertex?.Link<Edge>(currentVertex, linkParameters);
            if (lastVertex != null)
                currentVertex.Link<Edge>(lastVertex, linkParameters);
            lastVertex = currentVertex;
        }
    }
}
