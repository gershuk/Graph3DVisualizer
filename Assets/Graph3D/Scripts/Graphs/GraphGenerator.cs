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
        public abstract AbstractPlaceholder Placeholder { get; protected set; }

        public abstract void Generate (AbstractGraph abstractGraph);
    }

    public abstract class AbstractPlaceholder
    {
        public abstract IEnumerable<Vector3> GetPosition (int count);
    }

    public class GridPlaceholder : AbstractPlaceholder
    {
        public uint Dist { get; protected set; }
        public Vector2Int GridDim { get; protected set; }

        public GridPlaceholder (Vector2Int gridDim, uint dist) => (GridDim, Dist) = (gridDim, dist);

        public override IEnumerable<Vector3> GetPosition (int count)
        {
            for (var i = 0; i < count; ++i)
                yield return new Vector3(i % GridDim.x * Dist, i / GridDim.x % GridDim.y * Dist, i / (GridDim.x * GridDim.y) * Dist);
        }
    }

    public class SimpleGenerator : AbstractGraphGenerator
    {
        private const string _fontPath = "Font/CustomFontArial";
        private const string _selectFrameTexture = "Textures/SelectFrame";
        private static readonly Font _customFont = Resources.Load<Font>(_fontPath);
        private static readonly TextTextureFactory _textTextureFactory = new TextTextureFactory(_customFont, 32);

        public List<(int firstVertex, int secondVertex, Color color)> Edges { get; protected set; }
        public override AbstractPlaceholder Placeholder { get; protected set; }
        public List<(string text, Texture2D texture2D, string? id)> Vertexes { get; protected set; }

        public SimpleGenerator (List<(string text, Texture2D texture2D, string? id)> vertexes, List<(int firstVertex, int secondVertex, Color color)> edges, AbstractPlaceholder placeholder)
        {
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            Vertexes = vertexes ?? throw new ArgumentNullException(nameof(vertexes));
            Placeholder = placeholder ?? throw new ArgumentNullException(nameof(placeholder));
        }

        public override void Generate (AbstractGraph graphControler)
        {
            using var coordsEnum = Placeholder.GetPosition(Vertexes.Count).GetEnumerator();
            var createdVertexes = new SelectableVertex[Vertexes.Count];

            for (var i = 0; i < Vertexes.Count; ++i)
            {
                var (text, texture2D, id) = Vertexes[i];
                texture2D.name = "Target";
                var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 200, 200);
                selectFrame.name = "SelectFrame";

                var baseScale = Vector2.one;

                var imageParameters = new BillboardParameters(texture2D, scale: baseScale * 3f, useCache: true);
                var selectFrameParameters = new BillboardParameters(selectFrame, scale: baseScale * 6f, isMonoColor: true, useCache: false);

                var textTexture = _textTextureFactory.MakeTextTexture(text);
                const float scale = 10;
                var textParameters = new BillboardParameters(textTexture, new Vector4(0, -5, 0, 0), new Vector2(scale, textTexture.height * 1.0f / textTexture.width * scale));

                createdVertexes[i] = graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                    new SelectableVertexParameters(new[] { imageParameters, textParameters }, selectFrameParameters, coordsEnum.Current, id: id));

                coordsEnum.MoveNext();
            }

            foreach (var (firstVertex, secondVertex, color) in Edges)
                createdVertexes[firstVertex].Link<StretchableEdge, StretchableEdgeParameters>(createdVertexes[secondVertex], new StretchableEdgeParameters(new StretchableEdgeMaterialParameters(color)));
        }
    }
}