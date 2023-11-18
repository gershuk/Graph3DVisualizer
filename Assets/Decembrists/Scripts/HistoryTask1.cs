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
using System.Linq;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.SupportComponents;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class HistoryTask1 : VisualTaskController
    {
        private const string _decembristsPath = "Textures/Decembrists";
        private const string _fontPath = "Font/CustomFontArial";
        private const string _notDecembristsPath = "Textures/NotDecembrists";
        private const string _selectFramePath = "Textures/SelectFrame";

        [Obsolete]
        private DecembristVertex AddPeople (TextTextureFactory textTextureFactory,
                                            UnityEngine.Object man,
                                            GraphForBillboardVertexes graphController,
                                            Texture2D selectFrame,
                                            bool isDec)
        {
            var picked = (Texture2D) man;
            var name = textTextureFactory.MakeTextTexture(picked.name.Replace(',', '\n').Replace(' ', '\n').Replace("\n\n", "\n"), true);
            const float scale = 15f;

            var resizedTexture = Texture2DExtension.ResizeTexture(picked, name.width, picked.height / picked.width * name.width);
            var image1 = new PositionedImage[2] { (resizedTexture, new(0, name.height)), (name, new(0, 0)) };
            var image2 = new PositionedImage[1] { (selectFrame, Vector2Int.zero) };

            var width = Math.Max(resizedTexture.width, name.width);
            var height = resizedTexture.height + name.height;

            CombinedImages comIm1 = new(image1, width, height, initTransparentBackground: true);
            var billPar1 = new BillboardParameters[]
            {
                new BillboardParameters(Texture2DExtension.CombineTextures(comIm1),
                                        Vector4.zero,
                                        new Vector2(scale, height * scale / width),
                                        0.1f,
                                        false,
                                        Color.white)
            };

            CombinedImages comIm2 = new(image2, selectFrame.width, selectFrame.height, initTransparentBackground: true);
            var value = Mathf.Max(scale + 3.5f, (height * scale / width) + 3.5f);
            BillboardParameters billPar2 = new(Texture2DExtension.CombineTextures(comIm2),
                                               Vector4.zero,
                                               new Vector2(value, value),
                                               0.1f,
                                               true,
                                               Color.red);

            SelectableVertexParameters verPar = new(billPar1, billPar2);
            var vertex = graphController.SpawnVertex<DecembristVertex, SelectableVertexParameters>(verPar);
            vertex.IsDec = isDec;
            vertex.Name = picked.name;

            return vertex;
        }

        [Obsolete]
        public GraphForBillboardVertexes CreateGraph ()
        {
            GameObject graph = new("Graph");
            var graphControler = graph.AddComponent<GraphForBillboardVertexes>();

            var rand = new System.Random();
            var decembrists = Resources.LoadAll(_decembristsPath, typeof(Texture2D));
            var notDecembrists = Resources.LoadAll(_notDecembristsPath, typeof(Texture2D));

            const int decCount = 6;
            const int notdecCount = 4;
            var customFont = Resources.Load<Font>(_fontPath);
            TextTextureFactory textTextureFactory = new(customFont, 32);
            var selectFrame = Resources.Load<Texture2D>(_selectFramePath);

            var people = new List<DecembristVertex>(10);

            for (var i = 0; i < decCount; ++i)
                people.Add(AddPeople(textTextureFactory, decembrists[i], graphControler, selectFrame, true));

            for (var i = 0; i < notdecCount; ++i)
                people.Add(AddPeople(textTextureFactory, notDecembrists[i], graphControler, selectFrame, false));

            people = people.OrderBy(_ => rand.Next()).ToList();

            var p = 0;
            foreach (var man in people)
            {
                man.MovementComponent.GlobalCoordinates = new(p % 5 * 18, p / 5 * 25, 0);
                p++;
            }

            return graphControler;
        }

        public override List<Verdict> GetResult ()
        {
            List<Verdict> verdicts = new(base.Graphs[0].GetComponent<GraphForBillboardVertexes>().VertexesCount);
            foreach (DecembristVertex vertex in base.Graphs[0].GetComponent<GraphForBillboardVertexes>().GetVertexes())
            {
                var type = vertex.IsDec ? "Декабрист" : "Не декабрист";
                var act = vertex.IsSelected ? "выбрали" : "не выбрали";
                var stat = vertex.IsSelected == vertex.IsDec ? VerdictStatus.Correct : VerdictStatus.Incorrect;
                verdicts.Add(new Verdict($"{vertex.Name} - {type}. Вы {act}", stat));
            }
            return verdicts;
        }

        [Obsolete]
        public override void InitEnvironment ()
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

            base.Graphs.Add(CreateGraph());
            var player = Instantiate(Resources.Load<GameObject>(_playerPrefabPath));
            var flyPlayer = player.GetComponent<FlyPlayer>();
            flyPlayer.SetupParams(new PlayerParameters(toolConfigs: new[]
                                                       {
                                                            new ToolConfig(typeof(SelectItemTool),
                                                            new SelectItemToolParams(colors)),
                                                            new ToolConfig(typeof(GrabItemTool),
                                                            new GrabItemToolParams())
                                                       }));
            Players.Add(flyPlayer);
        }
    }
}