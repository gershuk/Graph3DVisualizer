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

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.Graph3D
{
    public abstract class AbstractGraphGenerator
    {
        public abstract void Generate (AbstractGraph abstractGraph);
    }

    public abstract class AbstractGraphGeneratorWithPlaceholder : AbstractGraphGenerator
    {
        public virtual AbstractPlaceholder? Placeholder { get; protected set; }
    }

    public abstract class AbstractPlaceholder
    {
        public abstract IEnumerable<Vector3> GetPosition ();
    }

    public class AdjacencyListBaseGenerator : AbstractGraphGeneratorWithPlaceholder
    {
        public struct AdjacencyInfo
        {
            public Color Color;
            public string FirstId;
            public bool IsBidirectional;
            public float Length;
            public string SecondId;
            public float StiffnessCoefficient;

            public AdjacencyInfo (string firstId,
                                  string secondId,
                                  bool isBidirectional,
                                  float length = 20,
                                  float stiffnessCoefficient = 1,
                                  Color color = default)
            {
                FirstId = firstId;
                SecondId = secondId;
                IsBidirectional = isBidirectional;
                Length = length;
                StiffnessCoefficient = stiffnessCoefficient;
                Color = color;
            }

            public void Deconstruct (out string firstId,
                                     out string secondId,
                                     out bool isBidirectional,
                                     out float length,
                                     out float stiffnessCoefficient,
                                     out Color color)
            {
                firstId = FirstId;
                secondId = SecondId;
                isBidirectional = IsBidirectional;
                length = Length;
                stiffnessCoefficient = StiffnessCoefficient;
                color = Color;
            }

            public override bool Equals (object? obj) => obj is AdjacencyInfo other &&
                       FirstId == other.FirstId &&
                       SecondId == other.SecondId &&
                       IsBidirectional == other.IsBidirectional &&
                       Length == other.Length &&
                       StiffnessCoefficient == other.StiffnessCoefficient &&
                       Color == other.Color;

            public override int GetHashCode () => HashCode.Combine(FirstId, SecondId, IsBidirectional, Length, StiffnessCoefficient, Color);
        }

        private const string _defaultTexture = "Textures/Dot";
        public List<AdjacencyInfo> Edges { get; protected set; }
        public string? MainImagePath { get; protected set; }
        public float Size { get; protected set; }

        public AdjacencyListBaseGenerator (List<AdjacencyInfo> edges,
                                           AbstractPlaceholder abstractPlaceholder,
                                           string? mainImagePath = default,
                                           float size = 5)
        {
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            MainImagePath = mainImagePath;
            Placeholder = abstractPlaceholder;
            Size = size;
        }

        [Obsolete]
        public override void Generate (AbstractGraph abstractGraph)
        {
            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 64);
            var textTextureFactory = new TextTextureFactory(customFont, 0);
            var texture = string.IsNullOrEmpty(MainImagePath)
                          ? Resources.Load<Texture2D>(_defaultTexture)
                          : Texture2DExtension.ReadTexture(MainImagePath);
            var imageParameters = new BillboardParameters(texture, scale: Vector2.one * Size, useCache: true, description: "MainImage");
            var posEn = Placeholder.GetPosition().GetEnumerator();

            AbstractVertex GetOrCreateVertex (string id)
            {
                //slow, but more reliable than using an exception
                AbstractVertex abstractVertex;
                if (!abstractGraph.ContainsVertex(id))
                {
                    posEn.MoveNext();
                    var text = textTextureFactory.MakeTextTexture(id, true);
                    BillboardParameters textParameters = new(text,
                                                              new Vector4(0, Size * 0.2f, 0, 1),
                                                              new Vector2(Size / 2, text.height * 1.0f / text.width * Size / 2),
                                                              isMonoColor: true,
                                                              monoColor: Color.white);
                    abstractVertex = abstractGraph.
                        SpawnVertex<BillboardVertex, BillboardVertexParameters>(new(new[] { imageParameters, textParameters },
                                                                                     posEn.Current,
                                                                                     id: id));
                }
                else
                {
                    abstractVertex = abstractGraph.GetVertexById(id);
                }

                return abstractVertex;
            }

            var stretchableEdgeMaterialsCache = new Dictionary<Color, StretchableEdgeMaterialParameters>();
            foreach (var adjacencyInfo in Edges)
            {
                var firstVertex = GetOrCreateVertex(adjacencyInfo.FirstId);
                var secondVertex = GetOrCreateVertex(adjacencyInfo.SecondId);

                if (!stretchableEdgeMaterialsCache.TryGetValue(adjacencyInfo.Color, out var stretchableEdgeMaterialParameters))
                {
                    stretchableEdgeMaterialParameters = new StretchableEdgeMaterialParameters(adjacencyInfo.Color, true);
                    stretchableEdgeMaterialsCache.Add(stretchableEdgeMaterialParameters.Color, stretchableEdgeMaterialParameters);
                }

                StretchableEdgeParameters edgeParameters = new(stretchableEdgeMaterialParameters,
                                                                new(adjacencyInfo.StiffnessCoefficient, adjacencyInfo.Length),
                                                                1,
                                                                (Size / 2) + 1,
                                                                (Size / 2) + 1,
                                                                Size / 10);

                try
                {
                    firstVertex.Link<StretchableEdge, StretchableEdgeParameters>(secondVertex, edgeParameters);
                    if (adjacencyInfo.IsBidirectional)
                        secondVertex.Link<StretchableEdge, StretchableEdgeParameters>(firstVertex, edgeParameters);
                }
                catch (LinkAlreadyExistException e)
                {
                    Debug.Log(e);
                }
            }
        }
    }

    public class GridPlaceholder : AbstractPlaceholder
    {
        public int Count;
        public uint Dist { get; protected set; }
        public Vector2Int GridDim { get; protected set; }

        public GridPlaceholder (Vector2Int gridDim, uint dist) => (GridDim, Dist) = (gridDim, dist);

        public override IEnumerable<Vector3> GetPosition ()
        {
            for (var i = 0; i < Count; ++i)
                yield return new Vector3(i % GridDim.x * Dist, i / GridDim.x % GridDim.y * Dist, i / (GridDim.x * GridDim.y) * Dist);
        }
    }

    public sealed class RandomPlaceholder : AbstractPlaceholder
    {
        private Vector3 _maxValue;
        private Vector3 _minValue;

        public RandomPlaceholder (Vector3 minValue, Vector3 maxValue) => (_minValue, _maxValue) = (minValue, maxValue);

        public override IEnumerable<Vector3> GetPosition ()
        {
            while (true)
            {
                yield return new(UnityEngine.Random.Range(_minValue.x, _maxValue.x),
                                 UnityEngine.Random.Range(_minValue.y, _maxValue.y),
                                 UnityEngine.Random.Range(_minValue.z, _maxValue.z));
            }
        }
    }

    public class SimpleGenerator : AbstractGraphGeneratorWithPlaceholder
    {
        private const string _fontPath = "Font/CustomFontArial";
        private const string _selectFrameTexture = "Textures/SelectFrame";
        private static readonly Font _customFont = Resources.Load<Font>(_fontPath);

        [Obsolete]
        private static readonly TextTextureFactory _textTextureFactory = new(_customFont, 32);

        public List<(int firstVertex, int secondVertex, Color color)> Edges { get; protected set; }
        public List<(string text, Texture2D texture2D, string? id)> Vertexes { get; protected set; }

        public SimpleGenerator (List<(string text, Texture2D texture2D, string? id)> vertexes,
                                List<(int firstVertex, int secondVertex, Color color)> edges,
                                AbstractPlaceholder placeholder)
        {
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            Vertexes = vertexes ?? throw new ArgumentNullException(nameof(vertexes));
            Placeholder = placeholder ?? throw new ArgumentNullException(nameof(placeholder));
        }

        [Obsolete]
        public override void Generate (AbstractGraph graphControler)
        {
            using var coordsEnum = Placeholder.GetPosition().GetEnumerator();
            var createdVertexes = new SelectableVertex[Vertexes.Count];

            for (var i = 0; i < Vertexes.Count; ++i)
            {
                var (text, texture2D, id) = Vertexes[i];
                texture2D.name = "Target";
                var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 200, 200);
                selectFrame.name = "SelectFrame";

                var baseScale = Vector2.one;

                BillboardParameters imageParameters = new(texture2D, scale: baseScale * 3f, useCache: true);
                BillboardParameters selectFrameParameters = new(selectFrame, scale: baseScale * 6f, isMonoColor: true, useCache: false);

                var textTexture = _textTextureFactory.MakeTextTexture(text);
                const float scale = 10;
                BillboardParameters textParameters = new(textTexture,
                                                          new Vector4(0, -5, 0, 0),
                                                          new Vector2(scale, textTexture.height * 1.0f / textTexture.width * scale));

                createdVertexes[i] = graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                    new(new[] { imageParameters, textParameters }, selectFrameParameters, coordsEnum.Current, id: id));

                coordsEnum.MoveNext();
            }

            foreach (var (firstVertex, secondVertex, color) in Edges)
            {
                createdVertexes[firstVertex].Link<StretchableEdge, StretchableEdgeParameters>(createdVertexes[secondVertex],
                                                                                          new(new(color), new SpringParameters(1, 10)));
            }
        }
    }
}