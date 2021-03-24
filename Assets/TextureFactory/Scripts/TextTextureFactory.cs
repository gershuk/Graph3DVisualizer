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

using System.Collections.Generic;

using UnityEngine;

namespace Graph3DVisualizer.TextureFactory
{
    /// <summary>
    /// Fabricator that allows you to create text on a texture.
    /// </summary>
    public class TextTextureFactory
    {
        private readonly Texture2D[] _alphabet;
        private readonly Font _customFont;

        public TextTextureFactory (Font customFont, int ASCIIOffset)
        {
            _customFont = customFont;
            var fontTexture = (Texture2D) _customFont.material.mainTexture;

            _alphabet = new Texture2D[256];

            foreach (var characterInfo in _customFont.characterInfo)
            {
                var width = Mathf.RoundToInt((characterInfo.uvTopRight.x - characterInfo.uvBottomLeft.x) * fontTexture.width);
                var height = Mathf.RoundToInt((characterInfo.uvTopRight.y - characterInfo.uvBottomLeft.y) * fontTexture.height);

                var pixels = fontTexture.GetPixels((int) (characterInfo.uvBottomLeft.x * fontTexture.width),
                                                   (int) (characterInfo.uvBottomLeft.y * fontTexture.height),
                                                   width, height);

                _alphabet[ASCIIOffset + characterInfo.index] = new Texture2D(width, height);
                _alphabet[ASCIIOffset + characterInfo.index].SetPixels(pixels);
                _alphabet[ASCIIOffset + characterInfo.index].Apply(true);
            }
        }

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
                    textureHeight += _customFont.lineHeight;
                    textureWidth = Mathf.Max(textureWidth, lineWidth);
                    linesSizes.Add(new Vector2Int(lineWidth, _customFont.lineHeight));
                    lineWidth = 0;
                }
            }

            var textTexture = new Texture2D(textureWidth, textureHeight);

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