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

using UnityEngine;

namespace Graph3DVisualizer.TextureFactory
{
    /// <summary>
    /// Fabricator that allows you to create text on a texture.
    /// </summary>
    public class TextTextureFactory
    {
        private enum TextureCorner
        {
            BottomLeft = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3
        }

        private readonly Texture2D[] _alphabet;
        private readonly Font _customFont;
        private readonly int _lineHeight;

        public TextTextureFactory (Font customFont, int ASCIIOffset)
        {
            _customFont = customFont;
            var fontTexture = Texture2DExtension.ResizeTexture((Texture2D) _customFont.material.mainTexture, _customFont.material.mainTexture.width, _customFont.material.mainTexture.height);

            _alphabet = new Texture2D[3000];

            var maxWidth = 0;
            var maxHeight = 0;

            foreach (var characterInfo in _customFont.characterInfo)
            {
                var deltaX = characterInfo.uvTopRight.x - characterInfo.uvBottomLeft.x;
                var deltaY = characterInfo.uvTopRight.y - characterInfo.uvBottomLeft.y;
                // For accuracy, you can use : var width = characterInfo.glyphWidth; var height = characterInfo.glyphHeight; But they are not defined for all fonts.
                var width = Mathf.Abs(Mathf.RoundToInt(deltaX * fontTexture.width));
                var height = Mathf.Abs(Mathf.RoundToInt(deltaY * fontTexture.height));

                maxWidth = Math.Max(maxWidth, width);
                maxHeight = Math.Max(maxHeight, height);

                var pixels = fontTexture.GetPixels((int) (Mathf.Min(characterInfo.uvBottomLeft.x, characterInfo.uvTopRight.x) * fontTexture.width),
                                                   (int) (Mathf.Min(characterInfo.uvBottomLeft.y, characterInfo.uvTopRight.y) * fontTexture.height),
                                                   width, height);

                (Vector2 pos, TextureCorner type)[] locationOfCorners = { (characterInfo.uvBottomLeft, TextureCorner.BottomLeft),
                                                                 (characterInfo.uvTopLeft, TextureCorner.TopLeft),
                                                                 (characterInfo.uvBottomRight, TextureCorner.BottomRight),
                                                                 (characterInfo.uvTopRight, TextureCorner.TopRight) };

                Array.Sort(locationOfCorners, (c1, c2) =>
                    c1.pos == c2.pos ?
                    0 :
                    c1.pos.x > c2.pos.x || (c1.pos.x == c2.pos.x && c1.pos.y > c2.pos.y) ?
                    1 :
                    -1);

                static void VerticalInvers (Color[] pixels, int width, int height)
                {
                    for (var i = 0; i < width; ++i)
                    {
                        for (var j = 0; j < height / 2; ++j)
                        {
                            var buff = pixels[i + j * width];
                            pixels[i + j * width] = pixels[i + (height - 1 - j) * width];
                            pixels[i + (height - 1 - j) * width] = buff;
                        }
                    }
                }

                static void Rotate90Left (ref Color[] pixels, ref int width, ref int height)
                {
                    var rotatedPixels = new Color[width * height];
                    for (var y = 0; y < height; y++)
                    {
                        for (var x = 0; x < width; x++)
                        {
                            rotatedPixels[(height - 1) - y + x * height] = pixels[x + y * width];
                        }
                    }
                    var buff = height;
                    height = width;
                    width = buff;
                    pixels = rotatedPixels;
                }

                static void Rotate90Right (ref Color[] pixels, ref int width, ref int height)
                {
                    var rotatedPixels = new Color[width * height];
                    for (var y = 0; y < height; y++)
                    {
                        for (var x = 0; x < width; x++)
                        {
                            rotatedPixels[y + (width - 1 - x) * height] = pixels[x + y * width];
                        }
                    }
                    var buff = height;
                    height = width;
                    width = buff;
                    pixels = rotatedPixels;
                }

                switch ((locationOfCorners[0].type, locationOfCorners[1].type, locationOfCorners[2].type, locationOfCorners[3].type))
                {
                    case (TextureCorner.BottomLeft, TextureCorner.TopLeft, TextureCorner.BottomRight, TextureCorner.TopRight):
                        //orig
                        break;

                    case (TextureCorner.TopLeft, TextureCorner.TopRight, TextureCorner.BottomLeft, TextureCorner.BottomRight):
                        //+90
                        Rotate90Right(ref pixels, ref width, ref height);
                        break;

                    case (TextureCorner.TopRight, TextureCorner.BottomRight, TextureCorner.TopLeft, TextureCorner.BottomLeft):
                        //180
                        Array.Reverse(pixels);
                        break;

                    case (TextureCorner.BottomRight, TextureCorner.BottomLeft, TextureCorner.TopRight, TextureCorner.TopLeft):
                        //-90
                        Rotate90Left(ref pixels, ref width, ref height);
                        break;
                    //vertical symmetry +
                    case (TextureCorner.TopLeft, TextureCorner.BottomLeft, TextureCorner.TopRight, TextureCorner.BottomRight):
                        //orig
                        VerticalInvers(pixels, width, height);
                        break;

                    case (TextureCorner.BottomLeft, TextureCorner.BottomRight, TextureCorner.TopLeft, TextureCorner.TopRight):
                        //-90
                        Rotate90Left(ref pixels, ref width, ref height);
                        break;

                    case (TextureCorner.BottomRight, TextureCorner.TopRight, TextureCorner.BottomLeft, TextureCorner.TopLeft):
                        //180
                        VerticalInvers(pixels, width, height);
                        Array.Reverse(pixels);
                        break;

                    case (TextureCorner.TopRight, TextureCorner.TopLeft, TextureCorner.BottomRight, TextureCorner.BottomLeft):
                        //+90
                        Rotate90Right(ref pixels, ref width, ref height);
                        break;
                }

                _alphabet[ASCIIOffset + characterInfo.index] = new Texture2D(width, height);
                _alphabet[ASCIIOffset + characterInfo.index].SetPixels(pixels);
                _alphabet[ASCIIOffset + characterInfo.index].Apply(true);
            }

            //set whitespace
            _alphabet[32] = new Texture2D(maxWidth, maxHeight);
            _alphabet[32].SetPixels32(new Color32[maxWidth * maxHeight]);

            _lineHeight = Math.Max(maxHeight, _customFont.lineHeight);
        }

        public Texture2D GetCharTexture (char ch) => _alphabet[ch];

        public Texture2D MakeTextTexture (string text, bool isTransparentBackground = false)
        {
            var textureWidth = 0;
            var textureHeight = 0;
            var lineWidth = 0;

            var linesSizes = new List<Vector2Int>();

            for (var i = 0; i < text.Length; i++)
            {
                var character = text[i];

                lineWidth += (character != '\n') ? _alphabet[character].width : 0;

                if (character == '\n' || i == text.Length - 1)
                {
                    textureHeight += _lineHeight;
                    textureWidth = Mathf.Max(textureWidth, lineWidth);
                    linesSizes.Add(new Vector2Int(lineWidth, _lineHeight));
                    lineWidth = 0;
                }
            }

            var textTexture = new Texture2D(textureWidth, textureHeight)
            {
                wrapMode = TextureWrapMode.Clamp,
            };

            if (isTransparentBackground)
            {
                textTexture.SetPixels32(new Color32[textureWidth * textureHeight]);
            }

            var posX = 0;
            var posY = textureHeight - linesSizes[linesSizes.Count - 1].y;
            var index = 0;

            foreach (var character in text)
            {
                if (character == '\n')
                {
                    posX = 0;
                    posY -= linesSizes[index].y;
                    ++index;
                }
                else
                {
                    textTexture.SetPixels32(posX, posY, _alphabet[character].width, _alphabet[character].height, _alphabet[character].GetPixels32());
                    posX += _alphabet[character].width;
                }
            }

            textTexture.Apply(true);

            return textTexture;
        }
    }
}