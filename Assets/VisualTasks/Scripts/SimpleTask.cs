﻿using System;
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

        public GameObject Player { get => _player; private set => _player = value; }
        public override ReadOnlyCollection<FlyPlayer> Players { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override ReadOnlyCollection<Graph> Graphs { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

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

                var image1 = new (Texture2D, Vector2Int Position)[2] { (resizedTetxure, new Vector2Int(0, text.height)), (text, new Vector2Int(0, 0)) };
                var image2 = new (Texture2D, Vector2Int Position)[1] { (selectFrame, Vector2Int.zero) };

                var width = resizedTetxure.width;
                var height = resizedTetxure.height + text.height;
                var scale = 10f;

                var verPar = new VertexParameters(new Vector3(i % 30 * 20, i / 30 * 20, 0), Quaternion.identity);
                var billPar1 = new BillboardParameters(image1, width, height, new Vector2(scale, height * scale / width), 0.1f, true, TextureWrapMode.Clamp, false, Color.white);
                var value = Mathf.Max(scale + 3.5f, height * scale / width + 3.5f);
                var billPar2 = new BillboardParameters(image2, selectFrame.width, selectFrame.height, new Vector2(value, value), 0.1f, true, TextureWrapMode.Clamp, true, Color.red);

                var currentVertex = graphControler.SpawnVertex<Vertex>(in verPar, in billPar1, in billPar2);
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
            _player.GetComponent<FlyPlayer>().SetUpPlayer(new PlayerParams(Vector3.zero, Vector3.zero, 40, 20,
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