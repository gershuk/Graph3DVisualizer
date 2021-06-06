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

using System.Collections.Generic;
using System.IO;

using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class MailGraph : AbstractSceneController
    {
        // Start is called before the first frame update
        private void Start ()
        {
        }

        // Update is called once per frame
        private void Update ()
        {
        }

        public override void InitTask ()
        {
            var player = CreatePlayer();
            var toolConfig = new ToolConfig(typeof(ClickTool), new ClickToolParams());
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -50), sceneInfo: "Mail Graph", isVR: true, toolConfigs: new[] { toolConfig }));

            {
                var graph = new GameObject("Mail Graph");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                Graphs.Add(graphControler);
                using var sr = new StreamReader("MailEdges.txt");
                var edges = new List<AdjacencyListBaseGenerator.AdjacencyInfo>(5000);
                //foreach (var pair in sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    var ids = pair.Split(' ');
                //    if (ids[0] == ids[1])
                //        continue;
                //    edges.Add(new AdjacencyListBaseGenerator.AdjacencyInfo(ids[0], ids[1], false, 1000, 10));
                //}
                const int vertCount = 120;
                const int sibCount = 5;
                for (var i = 0; i < vertCount; ++i)
                {
                    for (var j = 0; j < sibCount; ++j)
                    {
                        var sibId = UnityEngine.Random.Range(0, vertCount);
                        while (sibId == i)
                            sibId = UnityEngine.Random.Range(0, vertCount);
                        edges.Add(new AdjacencyListBaseGenerator.AdjacencyInfo(i.ToString(), sibId.ToString(), true, UnityEngine.Random.Range(0, 10) < 9 ? 5000 : 100, 10));
                    }
                }
                graphControler.SetupParams(new GraphParameters(Vector3.zero, "Mail Graph"));
                var generator = new AdjacencyListBaseGenerator(edges, new RandomPlaceholder(Vector3.one * -1000, Vector3.one * 1000));
                generator.Generate(graphControler);
            }
        }
    }
}