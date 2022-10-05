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

#nullable enable

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.SupportComponents;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    /// <summary>
    /// Example of implementing a graph visual task.
    /// </summary>
    public class SimpleTask : VisualTaskController
    {
        private const string _mainTexture = "Textures/Default";
        private const string _selectFrameTexture = "Textures/SelectFrame";

        [Obsolete]
        public GraphForBillboardVertexes CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
            Graphs.Add(graphControler);
            graphControler.SetupParams(new GraphParameters(new Vector3(0, -20), "Test"));

            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 32);
            var mainTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 200, 200);
            mainTexture.name = "Target";
            var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 200, 200);
            selectFrame.name = "SelectFrame";

            var textTextureFactory = new TextTextureFactory(customFont, 0);

            var baseScale = Vector2.one;

            var imageParameters = new BillboardParameters(mainTexture, scale: baseScale * 3f, useCache: true);
            var selectFrameParameters = new BillboardParameters(selectFrame, scale: baseScale * 6f, isMonoColor: true, useCache: false);

            var edgeParameters = new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(Color.white, true), new SpringParameters(1, 10));

            BillboardVertex? vertex = null;
            for (var i = 0; i < 1000; ++i)
            {
                var text = textTextureFactory.MakeTextTexture($"Vertex{i}", true);
                const float scale = 10;
                var textParameters = new BillboardParameters(text, new Vector4(0, -5, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale));

                var currentVertex = graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                    new SelectableVertexParameters(new[] { imageParameters, textParameters }, selectFrameParameters, new Vector3(i % 32 * 10, i / 32 * 10)));

                if (vertex != null)
                    currentVertex.Link<StretchableEdge, StretchableEdgeParameters>(vertex, edgeParameters);
                vertex = currentVertex;
            }

            return graphControler;
        }

        public override List<Verdict> GetResult () => new(1) { new Verdict("Test", VerdictStatus.Correct) };

        [Obsolete]
        public override void InitTask ()
        {
            CreateGraph();

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

            var edgeTypes = new List<(Type type, EdgeParameters parameters)>(1) { (typeof(StretchableEdge), new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(), new SpringParameters(1, 10))) };

            var player = Instantiate(Resources.Load<GameObject>(_playerPrefabPath)).GetComponent<FlyPlayer>();
            Players.Add(player);
            player.SetupParams(new PlayerParameters(new Vector3(100, 20, -50), Vector3.zero, 40, 20, true,
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