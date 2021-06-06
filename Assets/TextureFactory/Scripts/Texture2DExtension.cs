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

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.TextureFactory
{
    /// <summary>
    /// Сlass containing functions for changing textures that are not included in Unity3D engine.
    /// </summary>
    public static class Texture2DExtension
    {
        public static Texture2D CombineTextures (CombinedImages combinedImage)
        {
            var newTexture = new Texture2D(combinedImage.TextureWidth, combinedImage.TextureHeight, combinedImage.TextureFormat, combinedImage.IsMipChain)
            {
                wrapMode = combinedImage.WrapMode
            };

            //ToDo : Rewrite to CLI/C++
            if (combinedImage.InitTransparentBackground)
            {
                newTexture.SetPixels32(new Color32[combinedImage.TextureWidth * combinedImage.TextureHeight]);
            }

            foreach (var image in combinedImage.Images)
            {
                newTexture.SetPixels32(image.Position.x, image.Position.y,
                                     image.Texture.width, image.Texture.height,
                                     image.Texture.GetPixels32());
            }

            newTexture.Apply();

            return newTexture;
        }

        //ToDo : replace all methods of reading a texture from a file with this one
        public static Texture2D ReadTexture (string path)
        {
            var texture2D = new Texture2D(1, 1) { name = path };
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                texture2D.LoadImage(bytes);
            }
            return texture2D;
        }

        public static Texture2D ResizeTexture (Texture2D texture, int textureWidth, int textureHeight)
        {
            var renderTexture = RenderTexture.GetTemporary(textureWidth, textureHeight);
            renderTexture.filterMode = FilterMode.Point;
            RenderTexture.active = renderTexture;

            Graphics.Blit(texture, renderTexture);

            var newTexture = new Texture2D(textureWidth, textureHeight);
            newTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
            newTexture.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);

            newTexture.wrapMode = texture.wrapMode;

            return newTexture;
        }

        public static Texture2D RotateImage180Rigth (Texture2D originTexture)
        {
            var result = new Texture2D(originTexture.width, originTexture.height);
            var pixels = originTexture.GetPixels32();
            Array.Reverse(pixels);
            result.SetPixels32(pixels);
            result.Apply();
            return result;
        }

        public static Texture2D RotateImage90Left (Texture2D originTexture)
        {
            var result = new Texture2D(originTexture.height, originTexture.width);
            for (var i = 0; i < originTexture.width; ++i)
            {
                for (var j = 0; j < originTexture.height; ++j)
                    result.SetPixel(j, i, originTexture.GetPixel(i, originTexture.height - 1 - j));
            }

            result.Apply();
            return result;
        }

        public static Texture2D RotateImage90Right (Texture2D originTexture)
        {
            var result = new Texture2D(originTexture.height, originTexture.width);
            for (var i = 0; i < originTexture.width; ++i)
            {
                for (var j = 0; j < originTexture.height; ++j)
                    result.SetPixel(j, i, originTexture.GetPixel(originTexture.width - 1 - i, j));
            }

            result.Apply();
            return result;
        }
    }

    /// <summary>
    /// Class describing an image consisting of several pictures.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class CombinedImages : ICloneable
    {
        public PositionedImage[] Images { get; set; }
        public bool InitTransparentBackground { get; set; }
        public bool IsMipChain { get; set; }
        public TextureFormat TextureFormat { get; set; }
        public int TextureHeight { get; set; }
        public int TextureWidth { get; set; }
        public TextureWrapMode WrapMode { get; set; }

        public CombinedImages (PositionedImage[] images, int textureWidth, int textureHeight,
            TextureFormat textureFormat = TextureFormat.RGBA32, bool isMipChain = true, TextureWrapMode wrapMode = TextureWrapMode.Clamp, bool initTransparentBackground = false)
        {
            Images = images ?? throw new System.ArgumentNullException(nameof(images));
            InitTransparentBackground = initTransparentBackground;
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            TextureFormat = textureFormat;
            IsMipChain = isMipChain;
            WrapMode = wrapMode;
        }

        public object Clone () => new CombinedImages((PositionedImage[]) Images.Clone(), TextureWidth, TextureHeight, TextureFormat, IsMipChain, WrapMode, InitTransparentBackground);
    }

    /// <summary>
    /// Class that describes an image with a 2 dimensional coordinate reference.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class PositionedImage
    {
        public Vector2Int Position { get; set; }
        public Texture2D Texture { get; set; }

        public PositionedImage (Texture2D texture, Vector2Int position)
        {
            Texture = texture;
            Position = position;
        }

        public static implicit operator (Texture2D Texture, Vector2Int Position) (PositionedImage value) => (value.Texture, value.Position);

        public static implicit operator PositionedImage ((Texture2D Texture, Vector2Int Position) value) => new PositionedImage(value.Texture, value.Position);

        public void Deconstruct (out Texture2D texture, out Vector2Int position)
        {
            texture = Texture;
            position = Position;
        }

        public override bool Equals (object obj) => obj is PositionedImage other && EqualityComparer<Texture2D>.Default.Equals(Texture, other.Texture) && Position.Equals(other.Position);

        public override int GetHashCode ()
        {
            var hashCode = -1773728546;
            hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(Texture);
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            return hashCode;
        }
    }
}