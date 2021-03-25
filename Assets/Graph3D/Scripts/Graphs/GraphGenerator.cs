using System;
using System.Collections.Generic;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.Graph3D
{
    public abstract class GraphGenerator
    {
        public abstract void Generate (AbstractGraph abstractGraph);
    }

    public class GridGenerator : GraphGenerator
    {
        private const string _fontPath = "Font/CustomFontDroidSans-Bold";
        private const string _selectFrameTexture = "Textures/SelectFrame";
        private static readonly Font _customFont = Resources.Load<Font>(_fontPath);
        private static readonly TextTextureFactory _textTextureFactory = new TextTextureFactory(_customFont, 32);

        public List<LinkInfo> Edges { get; protected set; }

        public Vector2 GridDim { get; protected set; }

        public List<(string text, Texture2D texture2D)> Vertexes { get; protected set; }

        public GridGenerator ()
        {
            Vertexes = new List<(string text, Texture2D texture2D)>();
            Edges = new List<LinkInfo>();
            GridDim = default;
        }

        public GridGenerator (List<(string text, Texture2D texture2D)> vertexes, List<LinkInfo> edges, Vector3 gridDim)
        {
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            GridDim = gridDim;
            Vertexes = vertexes ?? throw new ArgumentNullException(nameof(vertexes));
        }

        public override void Generate (AbstractGraph graphControler)
        {
            for (var i = 0; i < Vertexes.Count; ++i)
            {
                (var text, var texture2D) = Vertexes[i];
                texture2D.name = "Target";
                var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 200, 200);
                selectFrame.name = "SelectFrame";

                var baseScale = Vector2.one;

                var imageParameters = new BillboardParameters(texture2D, scale: baseScale * 3f, useCache: true);
                var selectFrameParameters = new BillboardParameters(selectFrame, scale: baseScale * 6f, isMonoColor: true, useCache: false);

                var textTexture = _textTextureFactory.MakeTextTexture(text);
                const float scale = 10;
                var textParameters = new BillboardParameters(textTexture, new Vector4(0, -5, 0, 0), new Vector2(scale, textTexture.height * 1.0f / textTexture.width * scale));

                graphControler.SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                    new SelectableVertexParameters(new[] { imageParameters, textParameters }, selectFrameParameters, new Vector3(i % GridDim.x * 20, i / GridDim.x % GridDim.y * 20, i / (GridDim.x * GridDim.y) * 20)));
            }

            foreach (var linkInfo in Edges)
            {
                graphControler.GetVertexById(linkInfo.FirstVertexId).Link(graphControler.GetVertexById(linkInfo.FirstVertexId), linkInfo.EdgeType, linkInfo.EdgeParameters);
            }
        }
    }
}