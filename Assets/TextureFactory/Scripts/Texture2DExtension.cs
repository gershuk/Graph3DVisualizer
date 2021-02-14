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

using UnityEngine;

namespace Grpah3DVisualizer.TextureFactory
{
    public static class Texture2DExtension
    {
        public static Texture2D CombineTextures (CombinedImages combinedImage)
        {
            var newTexture = new Texture2D(combinedImage.TextureWidth, combinedImage.TextureHeight);

            //ToDo : Rewrite to CLI/C++
            if (combinedImage.IsTransparentBackground)
            {
                newTexture.SetPixels32(new Color32[combinedImage.TextureWidth * combinedImage.TextureHeight]);
            }

            foreach (var image in combinedImage.Images)
            {
                newTexture.SetPixels32(image.Position.x, image.Position.y,
                                     image.Texture.width, image.Texture.height,
                                     image.Texture.GetPixels32());
            }

            newTexture.wrapMode = combinedImage.WrapMode;
            newTexture.Apply();

            return newTexture;
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
    }

    public class PositionedImage
    {
        public Texture2D Texture;
        public Vector2Int Position;

        public PositionedImage (Texture2D texture, Vector2Int position)
        {
            Texture = texture;
            Position = position;
        }

        public override bool Equals (object obj) => obj is PositionedImage other && EqualityComparer<Texture2D>.Default.Equals(Texture, other.Texture) && Position.Equals(other.Position);

        public override int GetHashCode ()
        {
            var hashCode = -1773728546;
            hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(Texture);
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            return hashCode;
        }

        public void Deconstruct (out Texture2D texture, out Vector2Int position)
        {
            texture = Texture;
            position = Position;
        }

        public static implicit operator (Texture2D Texture, Vector2Int Position) (PositionedImage value) => (value.Texture, value.Position);
        public static implicit operator PositionedImage ((Texture2D Texture, Vector2Int Position) value) => new PositionedImage(value.Texture, value.Position);
    }

    public class CombinedImages : ICloneable
    {
        public PositionedImage[] Images { get; set; }
        public int TextureWidth { get; set; }
        public int TextureHeight { get; set; }
        public TextureWrapMode WrapMode { get; set; }
        public bool IsTransparentBackground { get; set; }

        public CombinedImages (PositionedImage[] images, int textureWidth, int textureHeight, TextureWrapMode wrapMode, bool isTransparentBackground)
        {
            Images = images ?? throw new System.ArgumentNullException(nameof(images));
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            WrapMode = wrapMode;
            IsTransparentBackground = isTransparentBackground;
        }

        public object Clone () => new CombinedImages((PositionedImage[]) Images.Clone(), TextureWidth, TextureHeight, WrapMode, IsTransparentBackground);
    }
}