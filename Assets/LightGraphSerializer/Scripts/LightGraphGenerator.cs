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
        public int Source { get; set; }

        public int Traget { get; set; }

        public float Value { get; set; }

        public EdgeSerializationInfo (int source, int traget, float value, bool bidirectional, Color color)
        {
            Source = source;
            Traget = traget;
            Value = value;
            Bidirectional = bidirectional;
            Color = color;
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
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public Color TextColor { get; set; }

        public VertexSerializationInfo (Vector3 coordinates, int size, string name, Color textColor, string path) =>
            (Coordinates, Size, Name, TextColor, Path) = (coordinates, size, name, textColor, path);
    }

    public sealed class LightGraphGenerator : AbstractGraphGenerator
    {
        private const string _defaultTexture = "Textures/Dot";
        private readonly Dictionary<string, Texture2D> _readCache;
        private GraphSerializationInfo _graphInfo;

        public GraphSerializationInfo GraphInfo { get => _graphInfo; private set => _graphInfo = value; }

        public LightGraphGenerator (GraphSerializationInfo graphInfo) => (GraphInfo, _readCache) = (graphInfo, new Dictionary<string, Texture2D>());

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

        public override void Generate (AbstractGraph abstractGraph)
        {
            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 64);
            var defaultTexture = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_defaultTexture), 200, 200);
            defaultTexture.name = "Target";
            var textTextureFactory = new TextTextureFactory(customFont, 0);
            var defaultImageParameters = new BillboardParameters(defaultTexture, scale: Vector2.one * 3f, useCache: true);

            for (var i = 0; i < GraphInfo.VertexInfos.Length; i++)
            {
                var vertexInfo = GraphInfo.VertexInfos[i];
                var imageParameters = string.IsNullOrEmpty(vertexInfo.Path) ? defaultImageParameters : new BillboardParameters(ReadTexture(vertexInfo.Path), scale: Vector2.one * vertexInfo.Size);

                var text = textTextureFactory.MakeTextTexture(vertexInfo.Name, true);
                var textParameters = new BillboardParameters(text, new Vector4(0, 0, 0, 1), new Vector2(vertexInfo.Size, text.height * 1.0f / text.width * vertexInfo.Size),
                    isMonoColor: true, monoColor: vertexInfo.TextColor);
                abstractGraph.SpawnVertex<BillboardVertex, BillboardVertexParameters>(new BillboardVertexParameters(new[] { imageParameters, textParameters },
                                                                                                                    vertexInfo.Coordinates,
                                                                                                                    Vector3.zero,
                                                                                                                    i.ToString()));
            }

            foreach (var edgeInfo in _graphInfo.EdgeInfos)
            {
                var edgeParameters = new WeightedEdgeParameters(new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(edgeInfo.Color), new SpringParameters(edgeInfo.Value, 10)), edgeInfo.Value);
                var source = abstractGraph.GetVertexById(edgeInfo.Source.ToString());
                var target = abstractGraph.GetVertexById(edgeInfo.Traget.ToString());
                source.Link<WeightedEdge, WeightedEdgeParameters>(target, edgeParameters);
                if (edgeInfo.Bidirectional)
                    target.Link<WeightedEdge, WeightedEdgeParameters>(source, edgeParameters);
            }
        }
    }
}