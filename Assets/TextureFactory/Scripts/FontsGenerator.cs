using System.Collections;
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
                _fonts[(name, 128)].RequestCharactersInTexture(_alphabet);
            }
            
            return _fonts[(name, size)];
        }
    }
}
