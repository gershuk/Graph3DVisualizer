using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.GUI;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class BeautifulScenes : AbstractSceneController
    {
        private const string _fontPath = "Font/CustomFontDroidSans-Bold";
        private const string _mainTexture = "Textures/Dot";

        public Graph CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<Graph>();
            graph.AddComponent<Button3D>().Action = (g) => graphControler.StartForceBasedLayout();
            Graphs.Add(graphControler);
            graphControler.SetupParams(new GraphParameters(new Vector3(-69, -15, -30), "Click me"));

            var customFont = Resources.Load<Font>(_fontPath);
            var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 200, 200);
            mainTexture.name = "Target";

            var textTextureFactory = new TextTextureFactory(customFont, 32);
            var resizedTetxure = Texture2DExtension.ResizeTexture(mainTexture, 200, 200);

            var baseScale = Vector2.one;

            var imageParameters = new BillboardParameters(resizedTetxure, scale: baseScale * 3f, useCache: true);

            var stretchableEdgeMaterialParameters = new StretchableEdgeMaterialParameters(Color.white, true);
            var edgeParameters = new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.yellow, true), new SpringParameters(1, 5));

            BillboardVertex? vertex = null;
            var coordinates = new Vector3[26] { new Vector3(-91.62799f, -2.3942585f, -43.161083f),
                                                new Vector3(-91.138885f, -3.3022366f, -17.54464f),
                                                new Vector3(-68.31055f, -9.711845f, -29.303394f),
                                                new Vector3(-71.31007f, -2.7437487f, -56.661026f),
                                                new Vector3(-45.411583f, -2.9353018f, -42.584347f),
                                                new Vector3(-47.30028f, -3.1969986f, -22.738922f),
                                                new Vector3(-64.77525f, -3.20299f, -2.5527115f),
                                                new Vector3(-49.3282f, 5.356124f, -2.7157516f),
                                                new Vector3(-37.920048f, 5.7275085f, -30.361862f),
                                                new Vector3(-52.707493f, 8.093277f, -57.873806f),
                                                new Vector3(-88.294876f, 7.605483f, -57.47967f),
                                                new Vector3(-81.45683f, 6.2511196f, -1.4397621f),
                                                new Vector3(-105.84165f, 6.152034f, -28.007107f),
                                                new Vector3(-99.75531f, 16.45427f, -11.093414f),
                                                new Vector3(-38.78393f, 16.683071f, -14.812604f),
                                                new Vector3(-40.808224f, 15.8126135f, -44.379215f),
                                                new Vector3(-73.01358f, 14.908555f, -55.7931f),
                                                new Vector3(-68.13217f, 15.000669f, 1.9544525f),
                                                new Vector3(-83.44349f, 25.460217f, -8.76181f),
                                                new Vector3(-59.4664f, 24.522339f, -10.350107f),
                                                new Vector3(-47.37295f, 23.873238f, -31.69215f),
                                                new Vector3(-92.189575f, 24.484436f, -28.520731f),
                                                new Vector3(-81.99469f, 23.73766f, -46.882977f),
                                                new Vector3(-56.52922f, 23.624348f, -43.66665f),
                                                new Vector3(-72.91645f, 30.9808f, -30.28012f),
                                                new Vector3(-93f, 20f, -42f),
                                                };

            var currentVertex = new BillboardVertex[coordinates.Length];
            for (var i = 0; i < coordinates.Length; ++i)
                currentVertex[i] = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(new BillboardVertexParameters(new[] { imageParameters }, coordinates[i]));

            currentVertex[0].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[1].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[3].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[4].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[5].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[6].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[2], edgeParameters);
            currentVertex[0].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[3], edgeParameters);
            currentVertex[1].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[0], edgeParameters);
            currentVertex[3].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[0], edgeParameters);
            currentVertex[4].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[5], edgeParameters);
            currentVertex[4].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[3], edgeParameters);
            currentVertex[5].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[6], edgeParameters);
            currentVertex[6].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[1], edgeParameters);

            currentVertex[7].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[8], edgeParameters);
            currentVertex[8].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[9], edgeParameters);
            currentVertex[9].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[10], edgeParameters);
            currentVertex[10].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[12], edgeParameters);
            currentVertex[11].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[7], edgeParameters);
            currentVertex[12].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[11], edgeParameters);

            currentVertex[9].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[4], edgeParameters);
            currentVertex[9].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[3], edgeParameters);
            currentVertex[10].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[3], edgeParameters);
            currentVertex[10].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[0], edgeParameters);
            currentVertex[12].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[0], edgeParameters);
            currentVertex[12].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[1], edgeParameters);

            currentVertex[11].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[1], edgeParameters);
            currentVertex[11].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[6], edgeParameters);
            currentVertex[7].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[6], edgeParameters);
            currentVertex[7].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[5], edgeParameters);
            currentVertex[8].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[5], edgeParameters);
            currentVertex[8].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[4], edgeParameters);

            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[20], edgeParameters);
            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[19], edgeParameters);
            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[18], edgeParameters);
            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[21], edgeParameters);
            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[22], edgeParameters);
            currentVertex[24].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[23], edgeParameters);

            currentVertex[20].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[19], edgeParameters);
            currentVertex[19].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[18], edgeParameters);
            currentVertex[18].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[21], edgeParameters);
            currentVertex[21].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[22], edgeParameters);
            currentVertex[22].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[23], edgeParameters);
            currentVertex[23].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[20], edgeParameters);

            currentVertex[18].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[17], edgeParameters);
            currentVertex[18].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[13], edgeParameters);
            currentVertex[21].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[13], edgeParameters);
            currentVertex[22].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[16], edgeParameters);
            currentVertex[23].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[16], edgeParameters);
            currentVertex[23].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[15], edgeParameters);
            currentVertex[20].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[15], edgeParameters);
            currentVertex[20].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[14], edgeParameters);
            currentVertex[19].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[14], edgeParameters);
            currentVertex[19].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[17], edgeParameters);

            currentVertex[17].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[13], edgeParameters);
            currentVertex[16].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[15], edgeParameters);
            currentVertex[15].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[14], edgeParameters);
            currentVertex[14].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[17], edgeParameters);

            currentVertex[15].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[9], edgeParameters);
            currentVertex[15].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[8], edgeParameters);
            currentVertex[14].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[8], edgeParameters);
            currentVertex[14].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[7], edgeParameters);
            currentVertex[17].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[7], edgeParameters);
            currentVertex[17].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[11], edgeParameters);
            currentVertex[13].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[11], edgeParameters);
            currentVertex[13].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[12], edgeParameters);
            currentVertex[16].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[10], edgeParameters);
            currentVertex[16].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[9], edgeParameters);

            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[10], edgeParameters);
            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[12], edgeParameters);
            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[13], edgeParameters);
            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[21], edgeParameters);
            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[22], edgeParameters);
            currentVertex[25].Link<StretchableEdge, StretchableEdgeParameters>(currentVertex[16], edgeParameters);

            var text = textTextureFactory.MakeTextTexture($"Click me", true);
            const float scale = 20;
            var textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale));
            return graphControler;
        }

        public override void InitTask ()
        {
            CreateGraph();
            var player = CreatePlayer();
            player.SetupParams(new PlayerParameters(Vector3.zero, Vector3.zero, 20, 10, new[] { new ToolConfig(typeof(ClickTool), new ClickToolParams()) }));
        }
    }
}