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
using System.IO;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

using Yuzu;

#nullable enable

namespace Graph3DVisualizer.LightGraphSerializer
{
    [YuzuAll]
    public struct EdgeSerializationInfo
    {
        public bool Bidirectional { get; set; }

        public Color Color { get; set; }
        public string Source { get; set; }

        public SpringParameters? SpringParameters { get; set; }
        public string Traget { get; set; }

        public float? Value { get; set; }

        public float Width { get; set; }

        public EdgeSerializationInfo (string source,
                                      string traget,
                                      float? value,
                                      bool bidirectional,
                                      float width = 1f,
                                      Color color = default,
                                      SpringParameters? springParaneters = default)
        {
            Source = source;
            Traget = traget;
            Value = value;
            Bidirectional = bidirectional;
            Width = width;
            Color = color;
            SpringParameters = springParaneters;
        }
    }

    [YuzuAll]
    public struct GraphSerializationInfo
    {
        public EdgeSerializationInfo[] EdgeInfos { get; set; }
        public VertexSerializationInfo[] VertexInfos { get; set; }
    }

    [YuzuAll]
    public struct VertexSerializationInfo
    {
        public Vector3 Coordinates { get; set; }
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Path { get; set; }
        public int Size { get; set; }
        public Color TextColor { get; set; }

        public VertexSerializationInfo (Vector3 coordinates,
                                        int size,
                                        string name,
                                        Color textColor = default,
                                        string? id = default,
                                        string? path = default) =>
            (Coordinates, Size, Name, TextColor, Id, Path) = (coordinates, size, name, textColor, id, path);
    }

    public sealed class LightGraphGenerator : AbstractGraphGenerator
    {
        private const string _defaultTexture = "Textures/Dot";
        private readonly Dictionary<string, Texture2D> _readCache;
        private GraphSerializationInfo _graphInfo;

        public GraphSerializationInfo GraphInfo { get => _graphInfo; private set => _graphInfo = value; }

        public LightGraphGenerator (GraphSerializationInfo graphInfo) =>
            (GraphInfo, _readCache) = (graphInfo, new Dictionary<string, Texture2D>());

        private Texture2D ReadTexture (string path)
        {
            if (_readCache.TryGetValue(path, out var texture2D))
                return texture2D;

            texture2D = new Texture2D(1, 1) { name = path };
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                texture2D.LoadImage(bytes);
            }

            _readCache.Add(path, texture2D);

            return texture2D;
        }

        [System.Obsolete]
        public override void Generate (AbstractGraph abstractGraph)
        {
            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 64);
            var defaultTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_defaultTexture), 200, 200);
            defaultTexture.name = "Target";
            var textTextureFactory = new TextTextureFactory(customFont, 0);

            for (var i = 0; i < GraphInfo.VertexInfos.Length; i++)
            {
                var vertexInfo = GraphInfo.VertexInfos[i];
                BillboardParameters imageParameters = string.IsNullOrEmpty(vertexInfo.Path)
                                                      ? new(defaultTexture, scale: Vector2.one * vertexInfo.Size)
                                                      : new(ReadTexture(vertexInfo.Path), scale: Vector2.one * vertexInfo.Size);

                var text = textTextureFactory.MakeTextTexture(vertexInfo.Name, true);
                BillboardParameters textParameters = new(text,
                                                         new(0, 0, 0, 1),
                                                         new(vertexInfo.Size, text.height * 1.0f / text.width * vertexInfo.Size),
                                                         isMonoColor: true,
                                                         monoColor: vertexInfo.TextColor);
                abstractGraph.SpawnVertex<BillboardVertex, BillboardVertexParameters>(new(new[] { imageParameters, textParameters },
                                                                                          vertexInfo.Coordinates,
                                                                                          Vector3.zero,
                                                                                          vertexInfo.Id ?? i.ToString()));
            }

            var stretchableEdgeMaterialsCache = new Dictionary<Color, StretchableEdgeMaterialParameters>();

            foreach (var edgeInfo in _graphInfo.EdgeInfos)
            {
                if (!stretchableEdgeMaterialsCache.TryGetValue(edgeInfo.Color, out var stretchableEdgeMaterialParameters))
                {
                    stretchableEdgeMaterialParameters = new StretchableEdgeMaterialParameters(edgeInfo.Color, true);
                    stretchableEdgeMaterialsCache.Add(stretchableEdgeMaterialParameters.Color, stretchableEdgeMaterialParameters);
                }

                var edgeParameters = new WeightedEdgeParameters(new(stretchableEdgeMaterialParameters,
                                                                     edgeInfo.SpringParameters ?? new(edgeInfo.Value ?? 1, 10),
                                                                     width: edgeInfo.Width), edgeInfo.Value);
                var source = abstractGraph.GetVertexById(edgeInfo.Source);
                var target = abstractGraph.GetVertexById(edgeInfo.Traget);
                source.Link<WeightedEdge, WeightedEdgeParameters>(target, edgeParameters);
                if (edgeInfo.Bidirectional)
                    target.Link<WeightedEdge, WeightedEdgeParameters>(source, edgeParameters);
            }
        }
    }
}