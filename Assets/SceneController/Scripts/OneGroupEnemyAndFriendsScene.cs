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

using System.Collections.Generic;

using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class OneGroupEnemyAndFriendsScene : AbstractSceneController
    {
        public override void InitTask ()
        {
            var player = CreatePlayer();
            var toolConfig = new ToolConfig(typeof(LayoutTool), new ToolParams());
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -1050), Vector3.zero, SceneParametersContainer.PlayerSpeed, 20, sceneInfo: "Social interactions", isVR: SceneParametersContainer.IsVR, toolConfigs: new[] { toolConfig }));
            {
                var graph = new GameObject("Social interactions");
                var graphControler = graph.AddComponent<GraphForBillboardVertexes>();
                Graphs.Add(graphControler);
                var edges = new List<AdjacencyListBaseGenerator.AdjacencyInfo>(5000);

                const int vertCount = 500;
                const int sibCount = 10;
                const int enemyCount = 2;
                for (var i = 0; i < vertCount; ++i)
                {
                    for (var j = 0; j < Random.Range(0, sibCount + 1); ++j)
                    {
                        var sibId = Random.Range(0, vertCount);
                        while (sibId == i)
                            sibId = Random.Range(0, vertCount);
                        edges.Add(new AdjacencyListBaseGenerator.AdjacencyInfo(i.ToString(), sibId.ToString(), true, 100, 10, Color.blue));
                    }

                    for (var j = 0; j < Random.Range(0, enemyCount + 1); ++j)
                    {
                        var sibId = Random.Range(0, vertCount);
                        while (sibId == i)
                            sibId = Random.Range(0, vertCount);
                        edges.Add(new AdjacencyListBaseGenerator.AdjacencyInfo(i.ToString(), sibId.ToString(), true, 1000, 15, Color.red));
                    }
                }
                graphControler.SetupParams(new GraphParameters(Vector3.zero, "Social interactions"));
                var generator = new AdjacencyListBaseGenerator(edges, new RandomPlaceholder(Vector3.one * -2300, Vector3.one * 2300), null, 15);
                generator.Generate(graphControler);
            }
        }
    }
}