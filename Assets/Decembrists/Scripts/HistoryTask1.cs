// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
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
using System.Linq;

using Grpah3DVisualizer.Billboards;
using Grpah3DVisualizer.Graph3D;
using Grpah3DVisualizer.PlayerInputControls;
using Grpah3DVisualizer.TextureFactory;

using UnityEngine;

namespace Grpah3DVisualizer.GraphTasks
{
    public class HistoryTask1 : AbstractVisualTask
    {
        private const string _decembristsPath = "Textures/Decembrists";
        private const string _fontPath = "Font/CustomFontArial";
        private const string _notDecembristsPath = "Textures/NotDecembrists";
        private const string _playerPath = "Prefabs/Player";
        private const string _selectFramePath = "Textures/SelectFrame";
        public override IReadOnlyCollection<AbstractGraph> Graphs { get => _graphs; protected set => _graphs = (List<AbstractGraph>) value; }
        public override IReadOnlyCollection<AbstractPlayer> Players { get => _players; protected set => _players = (List<AbstractPlayer>) value; }

        private DecembristVertex AddPeople (TextTextureFactory textTextureFactory, UnityEngine.Object man, Graph graphController, Texture2D selectFrame, bool isDec)
        {
            var rand = new System.Random();
            var picked = (Texture2D) man;
            var name = textTextureFactory.MakeTextTexture(picked.name.Replace(',', '\n').Replace(' ', '\n').Replace("\n\n", "\n"), true);
            var scale = 15f;

            var resizedTexture = Texture2DExtension.ResizeTexture(picked, name.width, picked.height / picked.width * name.width);
            var image1 = new PositionedImage[2] { (resizedTexture, new Vector2Int(0, name.height)), (name, new Vector2Int(0, 0)) };
            var image2 = new PositionedImage[1] { (selectFrame, Vector2Int.zero) };

            var width = Math.Max(resizedTexture.width, name.width);
            var height = resizedTexture.height + name.height;

            var comIm1 = new CombinedImages(image1, width, height, TextureWrapMode.Clamp, true);
            var billPar1 = new BillboardParameters(comIm1, new Vector2(scale, height * scale / width), 0.1f, true, false, Color.white);

            var comIm2 = new CombinedImages(image2, selectFrame.width, selectFrame.height, TextureWrapMode.Clamp, true);
            var value = Mathf.Max(scale + 3.5f, height * scale / width + 3.5f);
            var billPar2 = new BillboardParameters(comIm2, new Vector2(value, value), 0.1f, true, true, Color.red);

            var verPar = new SelectableVertexParameters(new Vector3(rand.Next(-60, 60), rand.Next(-60, 60), rand.Next(-60, 60)), Quaternion.identity, billPar1, billPar2, false);
            var vertex = graphController.SpawnVertex<DecembristVertex, SelectableVertexParameters>(verPar);
            vertex.IsDec = isDec;
            vertex.Name = picked.name;

            return vertex;
        }

        public Graph CreateGraph ()
        {
            var graph = new GameObject("Graph");
            var graphControler = graph.AddComponent<Graph>();

            var rand = new System.Random();
            var decembrists = Resources.LoadAll(_decembristsPath, typeof(Texture2D));
            var notDecembrists = Resources.LoadAll(_notDecembristsPath, typeof(Texture2D));

            var decCount = 6;
            var notdecCount = 4;
            var customFont = Resources.Load<Font>(_fontPath);
            var textTextureFactory = new TextTextureFactory(customFont, 32);
            var selectFrame = Resources.Load<Texture2D>(_selectFramePath);

            var people = new List<DecembristVertex>(10);

            for (var i = 0; i < decCount; ++i)
                people.Add(AddPeople(textTextureFactory, decembrists[i], graphControler, selectFrame, true));

            for (var i = 0; i < notdecCount; ++i)
                people.Add(AddPeople(textTextureFactory, notDecembrists[i], graphControler, selectFrame, false));

            people = people.OrderBy(x => rand.Next()).ToList();

            var p = 0;
            foreach (var man in people)
            {
                man.MovementComponent.GlobalCoordinates = new Vector3((p % 5) * 18, p / 5 * 25, 0);
                p++;
            }

            return graphControler;
        }

        public override List<Verdict> GetResult ()
        {
            var verdicts = new List<Verdict>(_graphs[0].GetComponent<Graph>().VertexesCount);
            foreach (DecembristVertex vertex in _graphs[0].GetComponent<Graph>().GetVertexes())
            {
                var type = vertex.IsDec ? "Декабрист" : "Не декабрист";
                var act = vertex.IsSelected ? "выбрали" : "не выбрали";
                var stat = vertex.IsSelected == vertex.IsDec ? VerdictStatus.Correct : VerdictStatus.Incorrect;
                verdicts.Add(new Verdict($"{vertex.Name} - {type}. Вы {act}", stat));
            }
            return verdicts;
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

            _graphs.Add(CreateGraph());
            var player = Instantiate(Resources.Load<GameObject>(_playerPath));
            var flyPlayer = player.GetComponent<FlyPlayer>();
            flyPlayer.SetupParams(new PlayerParameters(Vector3.back * 20, Vector3.zero, 40, 20,
                new ToolConfig[3]
                {
                    new ToolConfig(typeof(SelectItemTool), new SelectItemToolParams(colors)),
                    new ToolConfig(typeof(GrabItemTool), null),
                    new ToolConfig(typeof(EdgeCreaterTool), new EdgeCreaterToolParams(edgeTypes))
                }));
            _players.Add(flyPlayer);
        }

        public override void StartTask () => throw new NotImplementedException();

        public override void StopTask () => throw new NotImplementedException();
    }
}