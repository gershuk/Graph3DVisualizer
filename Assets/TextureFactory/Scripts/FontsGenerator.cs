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
using System.Text;

using UnityEngine;

namespace Graph3DVisualizer.TextureFactory
{
    public static class FontsGenerator
    {
        private static readonly string _alphabet;
        private static readonly Dictionary<(string name, int size), Font> _fonts;

        static FontsGenerator ()
        {
            var stringBuilder = new StringBuilder(256);
            for (var i = 32; i < 125; ++i)
                stringBuilder.Append((char) i);
            _alphabet = stringBuilder.ToString();
            _fonts = new Dictionary<(string name, int size), Font>(6);
        }

        public static Font GetOrCreateFont (string name, int size)
        {
            if (!_fonts.ContainsKey((name, size)))
            {
                _fonts.Add((name, size), Font.CreateDynamicFontFromOSFont(name, size));
                _fonts[(name, size)].RequestCharactersInTexture(_alphabet);
            }

            return _fonts[(name, size)];
        }
    }
}