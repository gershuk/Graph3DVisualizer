using System;
using System.Collections.Generic;

using TextureFactory;

using UnityEngine;

namespace Grpah3DVisualser
{
    public class SimpleTask : VisualTask
    {
        public override Verdict CheckEdge (Edge edge) => new Verdict("", VerdictStatus.Undefined);
        public override Verdict CheckVertex (Vertex vertex) => new Verdict("", VerdictStatus.Undefined);

        public override Graph CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<Graph>();
            Vertex lastVertex = null;

            var customFont = (Font) Resources.Load("Font/CustomFontDroidSans-Bold");
            var texture = (Texture2D) Resources.Load("Textures/Default");

            var TextTextureFactory = new TextTextureFactory(customFont, 32);
            var resizedTetxure = Texture2DExtension.ResizeTexture(texture, 200, 200);

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

                Destroy(text);
            }

            return graphControler;
        }

        public override List<Verdict> CheckGraph (Graph graph) => throw new System.NotImplementedException();

    }
}
