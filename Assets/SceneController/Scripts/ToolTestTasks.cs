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

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.GUI;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.SupportComponents;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class ToolTestTasks : VisualTaskController
    {
        private const string _dotTexture = "Textures/Dot";
        private const string _mainTexture = "Textures/Default";
        private const string _selectFrameTexture = "Textures/SelectFrame";

        private int _clickCount = 0;
        private BillboardVertex _dot1;
        private BillboardVertex _dot2;
        private BillboardVertex _moveVertex;
        private SelectableVertex _selectableVertex;

        public override List<Verdict> GetResult ()
        {
            var verdicts = new List<Verdict>(4)
            {
                new Verdict($"Нажатий на вершину>0 == {_clickCount}", _clickCount > 0 ? VerdictStatus.Correct : VerdictStatus.Incorrect),
                new Verdict($"Вершина выбрана", _selectableVertex.IsSelected ? VerdictStatus.Correct : VerdictStatus.Incorrect)
            };

            var c = false;

            foreach (var link in _dot1.OutgoingLinks)
            {
                if (link.AdjacentVertex == _dot2)
                {
                    c = true;
                    break;
                }
            }

            verdicts.Add(new Verdict($"Dot1 ----> Dot2", c ? VerdictStatus.Correct : VerdictStatus.Incorrect));
            verdicts.Add(new Verdict("Вершина сдвинута", new Vector3(30, 20, -10) != _moveVertex.transform.position ? VerdictStatus.Correct : VerdictStatus.Incorrect));

            return verdicts;
        }

        [Obsolete]
        public override void InitTask ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
            Graphs.Add(graphControler);
            graphControler.SetupParams(new GraphParameters(new Vector3(0, -20), "Test"));

            var customFont = FontsGenerator.GetOrCreateFont("Arial", 32);
            var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 800, 800);
            mainTexture.name = "Target";
            var dotTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 800, 800);
            mainTexture.name = "Dot";
            var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 800, 800);
            selectFrame.name = "SelectFrame";

            var textTextureFactory = new TextTextureFactory(customFont, 0);

            var baseScale = Vector2.one;

            var imageParameters = new BillboardParameters(mainTexture, scale: baseScale * 3f, useCache: true);
            var selectFrameParameters = new BillboardParameters(selectFrame, scale: baseScale * 6f, isMonoColor: true, useCache: false);
            var text = textTextureFactory.MakeTextTexture($"Select Me", true);
            const float scale = 10;
            var textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale), isMonoColor: true, monoColor: Color.white);

            _selectableVertex = graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                new SelectableVertexParameters(new[] { imageParameters, textParameters }, selectFrameParameters, new Vector3(-20, 20)));

            var dotImageParameters = new BillboardParameters(mainTexture, scale: baseScale * 3f, useCache: true);

            text = textTextureFactory.MakeTextTexture($"Dot 1", true);
            textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale), isMonoColor: true, monoColor: Color.white);
            _dot1 = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(
                new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(0, 20)));

            text = textTextureFactory.MakeTextTexture($"Dot 2", true);
            textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale), isMonoColor: true, monoColor: Color.white);
            _dot2 = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(
                new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(20, 20)));

            text = textTextureFactory.MakeTextTexture($"Move me", true);
            textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale), isMonoColor: true, monoColor: Color.white);
            _moveVertex = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(
                new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(30, 20, -10)));

            text = textTextureFactory.MakeTextTexture($"Click me", true);
            textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale), isMonoColor: true, monoColor: Color.white);
            var clickVeretex = graphControler.SpawnVertex<BillboardVertex, BillboardVertexParameters>(
                new BillboardVertexParameters(new[] { imageParameters, textParameters }, new Vector3(-50, 20, -10)));

            var snowPref = Resources.Load<GameObject>(@"Prefabs\Snowflake");
            clickVeretex.gameObject.AddComponent<Button3D>().Action = (g) => { _clickCount++; Destroy(Instantiate(snowPref, clickVeretex.transform.position, Quaternion.identity), 2); };

            var colors = new List<Color>(7)
                         {
                             new Color(1f,0f,0f),
                             new Color(1f,127f/255f,0f),
                             new Color(1f,1f,0f),
                             new Color(0f,1f,0f),
                             new Color(0f,0f,1f),
                             new Color(75f/255f,0f,130f/255f),
                             new Color(143f,0f,1f),
                         };

            var edgeTypes = new List<(Type type, EdgeParameters parameters)>(1) { (typeof(StretchableEdge), new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.white), new SpringParameters(1, 10))) };

            var player = CreatePlayer();
            player.SetupParams(new PlayerParameters(Vector3.zero, Vector3.zero, SceneParametersContainer.PlayerSpeed, 20, isVR: SceneParametersContainer.IsVR,
                new ToolConfig[4]
                {
                    new ToolConfig(typeof(SelectItemTool), new SelectItemToolParams(colors)),
                    new ToolConfig(typeof(GrabItemTool), new GrabItemToolParams()),
                    new ToolConfig(typeof(EdgeCreaterTool), new EdgeCreaterToolParams(edgeTypes)),
                    new ToolConfig(typeof(ClickTool), new ClickToolParams())
                }));
        }
    }
}