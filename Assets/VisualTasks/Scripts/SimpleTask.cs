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

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.GraphTasks
{
    /// <summary>
    /// Example of implementing a graph visual task.
    /// </summary>
    public class SimpleTask : AbstractVisualTask
    {
        private const string _fontPath = "Font/CustomFontDroidSans-Bold";
        private const string _mainTexture = "Textures/Default";
        private const string _playerPrefabPath = "Prefabs/Player";
        private const string _selectFrameTexture = "Textures/SelectFrame";

        public override IReadOnlyCollection<AbstractGraph> Graphs { get => _graphs; protected set => _graphs = (List<AbstractGraph>) value; }
        public override IReadOnlyCollection<AbstractPlayer> Players { get => _players; protected set => _players = (List<AbstractPlayer>) value; }

        public Graph CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<Graph>();
            _graphs.Add(graphControler);
            Vertex lastVertex = null;
            graphControler.SetupParams(new GraphParameters());

            var customFont = Resources.Load<Font>(_fontPath);
            var mainTexture = Resources.Load<Texture2D>(_mainTexture);
            var selectFrame = Resources.Load<Texture2D>(_selectFrameTexture);

            var TextTextureFactory = new TextTextureFactory(customFont, 32);
            var resizedTetxure = Texture2DExtension.ResizeTexture(mainTexture, 200, 200);

            for (var i = 0; i < 500; ++i)
            {
                var text = TextTextureFactory.MakeTextTexture($"Vertex{i}");
                text = Texture2DExtension.ResizeTexture(text, 200, (int) (Math.Truncate(200.0 / text.width + 1)) * text.height);

                var image1 = new PositionedImage[2] { (resizedTetxure, new Vector2Int(0, text.height)), (text, new Vector2Int(0, 0)) };
                var image2 = new PositionedImage[1] { (selectFrame, Vector2Int.zero) };

                var width = resizedTetxure.width;
                var height = resizedTetxure.height + text.height;
                const float scale = 10f;

                var combIm1 = new CombinedImages(image1, width, height);
                var billPar1 = new BillboardParameters(combIm1, new Vector2(scale, height * scale / width), 0.1f, true, false, Color.white);

                var value = Mathf.Max(scale + 3.5f, height * scale / width + 3.5f);
                var combIm2 = new CombinedImages(image2, selectFrame.width, selectFrame.height);
                var billPar2 = new BillboardParameters(combIm2, new Vector2(value, value), 0.1f, true, true, Color.red);

                var verPar = new SelectableVertexParameters(billPar1, billPar2, new Vector3(i % 30 * 40, i / 30 * 40, 0));
                var currentVertex = graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(verPar);
                currentVertex.gameObject.AddComponent<VertexLinksMenu>();
                currentVertex.gameObject.name = $"Vertex{i}";
                var edgeParams = new StretchableEdgeParameters(6, 6, 6, 16, Color.green);
                lastVertex?.Link<StretchableEdge, StretchableEdgeParameters>(currentVertex, edgeParams);
                if (lastVertex != null && i % 2 == 0)
                    currentVertex.Link<StretchableEdge, StretchableEdgeParameters>(lastVertex, edgeParams);
                lastVertex = currentVertex;

                //Destroy(text);
            }

            return graphControler;
        }

        public override List<Verdict> GetResult () => new List<Verdict>(1) { new Verdict("Test", VerdictStatus.Correct) };

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

            var edgeTypes = new List<Type>(1) { typeof(SpriteEdge) };

            var player = Instantiate(Resources.Load<GameObject>(_playerPrefabPath)).GetComponent<FlyPlayer>();
            _players.Add(player);
            player.SetupParams(new PlayerParameters(new Vector3(100, 20, -50), Vector3.zero, 40, 20,
                new ToolConfig[4]
                {
                    new ToolConfig(typeof(SelectItemTool), new SelectItemToolParams(colors)),
                    new ToolConfig(typeof(GrabItemTool), null),
                    new ToolConfig(typeof(EdgeCreaterTool), new EdgeCreaterToolParams(edgeTypes)),
                    new ToolConfig(typeof(ClickTool), new ClickToolParams())
                }));
        }
    }
}