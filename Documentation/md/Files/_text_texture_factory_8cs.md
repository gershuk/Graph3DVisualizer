---
title: Assets/TextureFactory/Scripts/TextTextureFactory.cs


---

# Assets/TextureFactory/Scripts/TextTextureFactory.cs







## Namespaces

| Name           |
| -------------- |
| **[TextureFactory](Namespaces/namespace_texture_factory.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[TextureFactory::TextTextureFactory](Classes/class_texture_factory_1_1_text_texture_factory.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
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

using System.Collections.Generic;

using UnityEngine;

namespace TextureFactory
{
    public class TextTextureFactory
    {
        private readonly Font _customFont;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Стили именования", Justification = "<Ожидание>")]
        private readonly int _ASCIIOffset;
        private readonly Texture2D[] _alphabet;

        public TextTextureFactory (Font customFont, int ASCIIOffset)
        {
            _customFont = customFont;
            _ASCIIOffset = ASCIIOffset;

            var fontTexture = (Texture2D) _customFont.material.mainTexture;

            _alphabet = new Texture2D[_customFont.characterInfo.Length];

            foreach (var characterInfo in _customFont.characterInfo)
            {
                //ToDo : Change to GetPixels32
                var pixels = fontTexture.GetPixels((int) (characterInfo.uvBottomLeft.x * fontTexture.width),
                                                    (int) (characterInfo.uvBottomLeft.y * fontTexture.height),
                                                    characterInfo.glyphWidth, characterInfo.glyphHeight);

                _alphabet[characterInfo.index] = new Texture2D(characterInfo.glyphWidth, characterInfo.glyphHeight);
                _alphabet[characterInfo.index].SetPixels(pixels);
                _alphabet[characterInfo.index].Apply(true);
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

                lineWidth += (character != '\n') ? _alphabet[character - _ASCIIOffset].width : 0;

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
                    var glyphIndex = character - _ASCIIOffset;
                    textTexture.SetPixels32(posX, posY, _alphabet[glyphIndex].width, _alphabet[glyphIndex].height, _alphabet[glyphIndex].GetPixels32());
                    posX += _alphabet[glyphIndex].width;
                }
            }

            textTexture.Apply(true);

            return textTexture;
        }
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (����)
