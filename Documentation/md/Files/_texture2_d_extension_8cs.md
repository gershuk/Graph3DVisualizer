---
title: Assets/TextureFactory/Scripts/Texture2DExtension.cs

---

# Assets/TextureFactory/Scripts/Texture2DExtension.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::TextureFactory](Namespaces/namespace_graph3_d_visualizer_1_1_texture_factory.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::TextureFactory::Texture2DExtension](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_texture2_d_extension.md)** <br>Ð¡lass containing functions for changing textures that are not included in Unity3D engine.  |
| class | **[Graph3DVisualizer::TextureFactory::CombinedImages](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md)** <br>Class describing an image consisting of several pictures.  |
| class | **[Graph3DVisualizer::TextureFactory::PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md)** <br>Class that describes an image with a 2 dimensional coordinate reference.  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright Â© Gershuk Vladislav 2021.
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

using UnityEngine;

namespace Graph3DVisualizer.TextureFactory
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

    [Serializable]
    public class CombinedImages : ICloneable
    {
        public PositionedImage[] Images { get; set; }
        public bool IsTransparentBackground { get; set; }
        public int TextureHeight { get; set; }
        public int TextureWidth { get; set; }
        public TextureWrapMode WrapMode { get; set; }

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

    [Serializable]
    public class PositionedImage
    {
        public Vector2Int Position;
        public Texture2D Texture;

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
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (çèìà)
