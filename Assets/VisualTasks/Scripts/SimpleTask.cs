using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Grpah3DVisualser;

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
