using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Grpah3DVisualser;

using PlayerInputControls;

using TextureFactory;

using UnityEngine;

namespace GraphTasks
{
    public class HistoryTask1 : VisualTask
    {
        private GameObject _graph;
        private GameObject _player;

        public override IReadOnlyCollection<AbstractPLayer> Players { get => new List<AbstractPLayer>(1) { _player.GetComponent<AbstractPLayer>() }; protected set => throw new NotImplementedException(); }
        public override IReadOnlyCollection<Graph> Graphs { get => new List<Graph>(1) { _graph.GetComponent<Graph>() }; protected set => throw new NotImplementedException(); }

        private void AddPeople (TextTextureFactory textTextureFactory, UnityEngine.Object man, Graph graphController, Texture2D selectFrame, bool isDec)
        {
            var rand = new System.Random();
            var picked = (Texture2D) man;
            var name = textTextureFactory.MakeTextTexture(picked.name.Replace(',', '\n'));
            var scale = 15f;

            var resizedTexture = Texture2DExtension.ResizeTexture(picked, name.width, picked.height / picked.width * name.width);
            var image1 = new PositionedImage[2] { (resizedTexture, new Vector2Int(0, name.height)), (name, new Vector2Int(0, 0)) };
            var image2 = new PositionedImage[1] { (selectFrame, Vector2Int.zero) };
            var width = resizedTexture.width;
            var height = resizedTexture.height + name.height;

            var comIm1 = new CombinedImages(image1, width, height, TextureWrapMode.Clamp, false);
            var billPar1 = new BillboardParameters(comIm1, new Vector2(scale, height * scale / width), 0.1f, true, false, Color.white);

            var comIm2 = new CombinedImages(image2, selectFrame.width, selectFrame.height, TextureWrapMode.Clamp, false);
            var value = Mathf.Max(scale + 3.5f, height * scale / width + 3.5f);
            var billPar2 = new BillboardParameters(comIm2, new Vector2(value, value), 0.1f, true, true, Color.red);

            var verPar = new VertexParameters(new Vector3(rand.Next(-60, 60), rand.Next(-60, 60), rand.Next(-60, 60)), Quaternion.identity, billPar1, billPar2);
            var vertex = graphController.SpawnVertex<DecembristVertex>(verPar);
            vertex.IsDec = isDec;
            vertex.Name = picked.name;
        }

        public override Graph CreateGraph ()
        {
            _graph = new GameObject("Graph");
            var graphControler = _graph.AddComponent<Graph>();

            var rand = new System.Random();
            var decembrists = Resources.LoadAll("Textures/Decembrists", typeof(Texture2D)).OrderBy(x => rand.Next()).ToList();
            var notDecembrists = Resources.LoadAll("Textures/NotDecembrists", typeof(Texture2D)).OrderBy(x => rand.Next()).ToList();

            var decCount = 5;
            var notdecCount = 4;
            var customFont = (Font) Resources.Load("Font/CustomFontDroidSans-Bold");
            var textTextureFactory = new TextTextureFactory(customFont, 32);
            var selectFrame = (Texture2D) Resources.Load("Textures/SelectFrame");

            for (var i = 0; i < decCount; ++i)
                AddPeople(textTextureFactory, decembrists[i], graphControler, selectFrame, true);


            for (var i = 0; i < notdecCount; ++i)
                AddPeople(textTextureFactory, notDecembrists[i], graphControler, selectFrame, false);

            return graphControler;
        }

        public override void InitTask ()
        {
            var colors = new List<Color>(1)
                         {
                             new Color(1f,0f,0f),
                             //new Color(1f,127f/255f,0f),
                             //new Color(1f,1f,0f),
                             //new Color(0f,1f,0f),
                             //new Color(0f,0f,1f),
                             //new Color(75f/255f,0f,130f/255f),
                             //new Color(143f,0f,1f),
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

        public override List<Verdict> GetResult ()
        {
            var verdicts = new List<Verdict>(_graph.GetComponent<Graph>().Vertexes.Count);
            foreach (DecembristVertex vertex in _graph.GetComponent<Graph>().Vertexes)
            {
                var type = vertex.IsDec ? "Декабрист" : "Не декабрист";
                var act = vertex.IsSelected ? "выбрали" : "не выбрали";
                var stat = vertex.IsSelected == vertex.IsDec ? VerdictStatus.Correct : VerdictStatus.Incorrect;
                verdicts.Add(new Verdict($"{vertex.Name} - {type}. Вы {act}", stat));
            }
            return verdicts;
        }
    }
}
