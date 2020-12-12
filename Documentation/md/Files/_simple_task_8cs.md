---
title: Assets/VisualTasks/Scripts/SimpleTask.cs


---

# Assets/VisualTasks/Scripts/SimpleTask.cs







## Namespaces

| Name           |
| -------------- |
| **[GraphTasks](Namespaces/namespace_graph_tasks.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[GraphTasks::SimpleTask](Classes/class_graph_tasks_1_1_simple_task.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Grpah3DVisualizer;

using PlayerInputControls;

using TextureFactory;

using UnityEngine;

namespace GraphTasks
{
    public class SimpleTask : VisualTask
    {
        private GameObject _graph;
        private GameObject _player;

        public override IReadOnlyCollection<AbstractPLayer> Players { get => new List<AbstractPLayer>(1) { _player.GetComponent<AbstractPLayer>() }; protected set => throw new NotImplementedException(); }
        public override IReadOnlyCollection<Graph> Graphs { get => new List<Graph>(1) { _graph.GetComponent<Graph>() }; protected set => throw new NotImplementedException(); }

        public override Graph CreateGraph ()
        {
            _graph = new GameObject("Graph");
            var graphControler = _graph.AddComponent<Graph>();
            Vertex lastVertex = null;

            var customFont = (Font) Resources.Load("Font/CustomFontDroidSans-Bold");
            var mainTexture = (Texture2D) Resources.Load("Textures/Default");
            var selectFrame = (Texture2D) Resources.Load("Textures/SelectFrame");

            var TextTextureFactory = new TextTextureFactory(customFont, 32);
            var resizedTetxure = Texture2DExtension.ResizeTexture(mainTexture, 200, 200);

            for (var i = 0; i < 100; ++i)
            {
                var text = TextTextureFactory.MakeTextTexture($"Vertex{i}");
                text = Texture2DExtension.ResizeTexture(text, 200, (int) (Math.Truncate(200.0 / text.width + 1)) * text.height);

                var image1 = new PositionedImage[2] { (resizedTetxure, new Vector2Int(0, text.height)), (text, new Vector2Int(0, 0)) };
                var image2 = new PositionedImage[1] { (selectFrame, Vector2Int.zero) };

                var width = resizedTetxure.width;
                var height = resizedTetxure.height + text.height;
                var scale = 10f;

                var combIm1 = new CombinedImages(image1, width, height, TextureWrapMode.Clamp, false);
                var billPar1 = new BillboardParameters(combIm1, new Vector2(scale, height * scale / width), 0.1f, true, false, Color.white);

                var value = Mathf.Max(scale + 3.5f, height * scale / width + 3.5f);
                var combIm2 = new CombinedImages(image2, selectFrame.width, selectFrame.height, TextureWrapMode.Clamp, false);
                var billPar2 = new BillboardParameters(combIm2, new Vector2(value, value), 0.1f, true, true, Color.red);

                var verPar = new VertexParameters(new Vector3(i % 30 * 20, i / 30 * 20, 0), Quaternion.identity, billPar1, billPar2);
                var currentVertex = graphControler.SpawnVertex<Vertex>(verPar);
                var linkParameters = new LinkParameters(6, 6);
                lastVertex?.Link(currentVertex, typeof(Edge), linkParameters);
                if (lastVertex != null && i % 2 == 0)
                    currentVertex.Link(lastVertex, typeof(Edge), linkParameters);
                lastVertex = currentVertex;

                Destroy(text);
            }

            return graphControler;
        }

        public override void InitTask ()
        {
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

            var edgeTypes = new List<Type>(1) { typeof(Edge) };

            CreateGraph();
            _player = (GameObject) Instantiate(Resources.Load("Prefabs/Player"));
            _player.GetComponent<FlyPlayer>().SetupParams(new PlayerParams(Vector3.zero, Vector3.zero, 40, 20,
                new ToolConfig[3]
                {
                    new ToolConfig(typeof(SelectItemTool), new SelectItemToolParams(colors)),
                    new ToolConfig(typeof(GrabItemTool), null),
                    new ToolConfig(typeof(EdgeCreaterTool), new EdgeCreaterToolParams(edgeTypes))
                }));
        }

        public override void StartTask () => throw new NotImplementedException();

        public override void StopTask () => throw new NotImplementedException();

        public override void DestroyTask ()
        {
            Destroy(_player);
            Destroy(_graph);
            Destroy(gameObject);
        }

        public override List<Verdict> GetResult () => throw new NotImplementedException();
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
