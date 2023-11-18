// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
//
// Graph3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Graph3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Graph3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.GUI;
using Graph3DVisualizer.LightGraphSerializer;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.SceneController
{
    public class BeautifulScenes : AbstractSceneController
    {
        private const string _fontPath = "Font/CustomFontDroidSans-Bold";

        private const string _mainTexture = "Textures/Dot";

        [SerializeField]
        private Texture2D _texure2D;

        [System.Obsolete]
        public GraphForBillboardVertexes CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
            graph.AddComponent<Button3D>().Action = (g) => graphControler.StartForceBasedLayout(1000);
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

        [System.Obsolete]
        public override void InitEnvironment ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
            var graphInfo = new GraphSerializationInfo()
            {
                VertexInfos = new[]
                {
                    new VertexSerializationInfo(new Vector3(355.5f,186), 10,"Москва",Color.yellow,string.Empty),
                    new VertexSerializationInfo(new Vector3(281.5f,113), 10,"Тверь",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(163.5f,20), 10,"Великий Новгород",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(133.5f,-77), 10,"Санкт-Петербург",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(444.5f,88), 10,"Ярославль",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(450.5f,-15), 10,"Вологда",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(497.5f,211), 10,"Владимир",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(517.5f,136), 10,"Иваново",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(657.5f,166), 10,"Нижний Новгород",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(819.5f,212), 10,"Чебоксары",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(957.5f,199), 10,"Казань",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(867.5f,321), 10,"Ульяновск",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(967.5f,417), 10,"Самара",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(737.5f,363), 10,"Пенза",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(693.5f,258), 10,"Саранск",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(702.5f,471), 10,"Саратов",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(608.5f,626), 10,"Волгоград",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(457.5f,446), 10,"Воронеж",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(247.5f,467), 10,"Курск",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(274.5f,343), 10,"Орёл",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(320.5f,258), 10,"Тула",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(526.5f,291), 10,"Резань",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(222.5f,249), 10,"Калуга",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(121.5f,336), 10,"Брянск",Color.white,string.Empty),
                    new VertexSerializationInfo(new Vector3(89.5f,203), 10,"Смоленск",Color.white,string.Empty),
                },
                EdgeInfos = new[]
                {
                    new EdgeSerializationInfo("0","1",156,true,1, Color.white),
                    new EdgeSerializationInfo("1","2",323,true,1, Color.white),
                    new EdgeSerializationInfo("2","3",151,true,1, Color.white),
                    new EdgeSerializationInfo("0","4",246,true,1, Color.white),
                    new EdgeSerializationInfo("4","5",174,true,1, Color.white),
                    new EdgeSerializationInfo("5","3",545,true,1, Color.white),
                    new EdgeSerializationInfo("0","6",184,true,1, Color.white),
                    new EdgeSerializationInfo("6","7",83,true,1, Color.white),
                    new EdgeSerializationInfo("7","4",100,true,1, Color.white),
                    new EdgeSerializationInfo("6","8",224,true,1, Color.white),
                    new EdgeSerializationInfo("8","9",209,true,1, Color.white),
                    new EdgeSerializationInfo("9","10",116,true,1, Color.white),
                    new EdgeSerializationInfo("10","11",180,true,1, Color.white),
                    new EdgeSerializationInfo("11","12",157,true,1, Color.white),
                    new EdgeSerializationInfo("11","13",251,true,1, Color.white),
                    new EdgeSerializationInfo("13","12",342,true,1, Color.white),
                    new EdgeSerializationInfo("13","15",208,true,1, Color.white),
                    new EdgeSerializationInfo("15","16",335,true,1, Color.white),
                    new EdgeSerializationInfo("15","17",462,true,1, Color.white),
                    new EdgeSerializationInfo("13","21",382,true,1, Color.white),
                    new EdgeSerializationInfo("13","14",111,true,1, Color.white),
                    new EdgeSerializationInfo("14","8",217,true,1, Color.white),
                    new EdgeSerializationInfo("21","6",192,true,1, Color.white),
                    new EdgeSerializationInfo("17","0",462,true,1, Color.white),
                    new EdgeSerializationInfo("17","18",212,true,1, Color.white),
                    new EdgeSerializationInfo("18","19",135,true,1, Color.white),
                    new EdgeSerializationInfo("19","20",174,true,1, Color.white),
                    new EdgeSerializationInfo("20","0",171,true,1, Color.white),
                    new EdgeSerializationInfo("22","0",157,true,1, Color.white),
                    new EdgeSerializationInfo("23","22",171,true,1, Color.white),
                    new EdgeSerializationInfo("24","0",363,true,1, Color.white),
                },
            };
            var lightGenerator = new LightGraphGenerator(graphInfo);
            lightGenerator.Generate(graphControler);

            CreateGraph();
            var player = CreatePlayer();
            player.SetupParams(new PlayerParameters(Vector3.zero, Vector3.zero, SceneParametersContainer.PlayerSpeed, 20, SceneParametersContainer.IsVR, new[] { new ToolConfig(typeof(ClickTool), new ClickToolParams()) }));
        }
    }
}