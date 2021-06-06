// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
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
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class HubScene : AbstractSceneController
    {
        private const string _mainTexture = "Textures/Ñircle";

        public override void InitTask ()
        {
            var player = CreatePlayer();
            var toolConfig = new ToolConfig(typeof(ClickTool), new ClickToolParams());
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -50), sceneInfo: "Hub scene", isVR: true, toolConfigs: new[] { toolConfig }));

            //ToDo : Replace with auto generation
            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 128);
            var textTextureFactory = new TextTextureFactory(customFont, 0);

            {
                var graph = new GameObject("Tutorial tasks");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                Graphs.Add(graphControler);
                var baseScale = Vector2.one;
                graphControler.SetupParams(new GraphParameters(Vector3.zero, "Tutorial tasks"));

                var text = textTextureFactory.MakeTextTexture("Tools test", true);
                var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 400, 400);
                var imageParameters = new BillboardParameters(mainTexture, Vector4.zero, new Vector2(9, 2), useCache: true);
                var textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false);
                var vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(-10, 6, 0));
                var toolsTest = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                toolsTest.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<ToolTestTasks>();

                text = textTextureFactory.MakeTextTexture("Movement test", true);
                textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false);
                vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(0, 3, 0));
                var movementTest = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                movementTest.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<MovementTestTask>();

                movementTest.Link<StretchableEdge, StretchableEdgeParameters>(toolsTest, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
            }

            {
                var graph = new GameObject("Beautiful scenes");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                Graphs.Add(graphControler);
                var baseScale = Vector2.one;
                graphControler.SetupParams(new GraphParameters(new Vector3(20, 0, 0), "Beautiful scenes"));
                var text = textTextureFactory.MakeTextTexture("Map", true);
                var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 400, 400);
                var imageParameters = new BillboardParameters(mainTexture, Vector4.zero, new Vector2(9, 2), useCache: true);
                var textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false);
                var vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(20, 3, 0));
                graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters).gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<BeautifulScenes>();

                text = textTextureFactory.MakeTextTexture("Mail Graph", true);
                textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false);
                vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(20, 8, 0));
                graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters).gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<MailGraph>();
            }
        }
    }
}