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
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class HubScene : AbstractSceneController
    {
        private const string _mainTexture = "Textures/Ñircle";

        [System.Obsolete]
        public override void InitEnvironment ()
        {
            var player = CreatePlayer();
            var toolConfig = new ToolConfig(typeof(ClickTool), new ClickToolParams());
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -50),
                                                    Vector3.zero,
                                                    SceneParametersContainer.PlayerSpeed,
                                                    sceneInfo: "Hub scene",
                                                    isVR: SceneParametersContainer.IsVR,
                                                    toolConfigs: new[] { toolConfig }));

            //ToDo : Replace with auto generation
            var customFont = FontsGenerator.GetOrCreateFont("Arial", 128);
            var textTextureFactory = new TextTextureFactory(customFont, 0);
            {
                var graph = new GameObject("Tutorial tasks");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                graphControler.ColliderEnable = false;
                Graphs.Add(graphControler);
                var baseScale = Vector2.one;
                graphControler.SetupParams(new GraphParameters(new Vector3(-20, 0, 0), "Tutorial tasks"));

                var text = textTextureFactory.MakeTextTexture("Tools test", true);
                var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 400, 400);
                var imageParameters = new BillboardParameters(mainTexture, Vector4.zero, new Vector2(9, 2), useCache: true);
                var textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false, isMonoColor: true, monoColor: Color.white);
                var vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(-30, 18, 0));
                var toolsTest = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                toolsTest.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<ToolTestTasks>();

                text = textTextureFactory.MakeTextTexture("Movement test", true);
                textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), isMonoColor: true, monoColor: Color.white, useCache: false);
                vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(-20, 9, 0));
                var movementTest = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                movementTest.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<MovementTestTask>();

                movementTest.Link<StretchableEdge, StretchableEdgeParameters>(toolsTest, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
            }

            {
                var graph = new GameObject("Beautiful scenes");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                graphControler.ColliderEnable = false;
                Graphs.Add(graphControler);
                var baseScale = Vector2.one;
                graphControler.SetupParams(new GraphParameters(new Vector3(20, 0, 0), "Beautiful scenes"));
                var text = textTextureFactory.MakeTextTexture("Plants Kingdom", true);
                var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 400, 400);
                var imageParameters = new BillboardParameters(mainTexture, Vector4.zero, new Vector2(9, 2), useCache: true);
                var textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), useCache: false, isMonoColor: true, monoColor: Color.white);
                var vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(10, 10, 0));
                var v1 = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                v1.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<PlantsKingdom>();

                text = textTextureFactory.MakeTextTexture("Social graph 1", true);
                textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), isMonoColor: true, monoColor: Color.white, useCache: false);
                vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(30, 19, 0));
                var v2 = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                v2.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<OneGroupEnemyAndFriendsScene>();

                text = textTextureFactory.MakeTextTexture("Social graph 2", true);
                textParameters = new BillboardParameters(text, Vector4.zero, new Vector2(4, 0.4f), isMonoColor: true, monoColor: Color.white, useCache: false);
                vertexParameters = new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(20, 28, 0));
                var v3 = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(vertexParameters);
                v3.gameObject.AddComponent<Button3D>().Action = (gameObject) => SceneLoader.Instance.LoadScene<MultyGroupFriendsScene>();

                v1.Link<StretchableEdge, StretchableEdgeParameters>(v2, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
                v2.Link<StretchableEdge, StretchableEdgeParameters>(v1, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
                v1.Link<StretchableEdge, StretchableEdgeParameters>(v3, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
                v3.Link<StretchableEdge, StretchableEdgeParameters>(v1, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
                v2.Link<StretchableEdge, StretchableEdgeParameters>(v3, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
                v3.Link<StretchableEdge, StretchableEdgeParameters>(v2, new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.green), new SpringParameters(1, 5)));
            }
        }
    }
}