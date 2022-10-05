// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

namespace Graph3DVisualizer.TypesForSerialization.YuzuTypes
{
    [YuzuAll]
    public class JsonSystemType
    {
        public string Name { get; set; }

        public JsonSystemType (string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public static Type FromSurrogate (object obj) => Type.GetType((obj as JsonSystemType).Name);

        public static implicit operator JsonSystemType (Type type) => ToSurrogate(type);

        public static implicit operator Type (JsonSystemType jsonSystemType) => FromSurrogate(jsonSystemType);

        public static JsonSystemType ToSurrogate (object obj) => new((obj as Type).AssemblyQualifiedName);
    }

    [YuzuAll]
    public class JsonTexture2D
    {
        private static Dictionary<string, Texture2D> _readCache;
        private static Dictionary<Texture2D, JsonTexture2D> _writeCache;

        public string Path { get; set; }

        static JsonTexture2D () => (_readCache, _writeCache) = (new(), new());

        public JsonTexture2D (string filePath) => Path = filePath ?? Guid.NewGuid().ToString();

        public static Texture2D FromSurrogate (object obj)
        {
            var jsonTexture2D = (JsonTexture2D) obj;

            if (_readCache.TryGetValue(jsonTexture2D.Path, out var texture2D))
                return texture2D;

            texture2D = new Texture2D(1, 1) { name = jsonTexture2D.Path };
            using (var fs = new FileStream(jsonTexture2D.Path, FileMode.Open))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                texture2D.LoadImage(bytes);
            }

            _readCache.Add(jsonTexture2D.Path, texture2D);

            return texture2D;
        }

        public static void ReinitializeCache ()
        {
            _readCache = new();
            _writeCache = new();
        }

        public static JsonTexture2D ToSurrogate (object obj)
        {
            var texture2D = (Texture2D) obj;

            if (_writeCache.TryGetValue(texture2D, out var jsonTexture2D))
                return jsonTexture2D;

            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Graph3DVisualizer\");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var fileName = (texture2D.name != string.Empty ? texture2D.name : Guid.NewGuid().ToString()) + ".jpg";
            path = System.IO.Path.Combine(path, fileName);
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var bytes = texture2D.EncodeToJPG();
                fs.Write(bytes, 0, bytes.Length);
            }

            jsonTexture2D = new JsonTexture2D(path);
            _writeCache.Add(texture2D, jsonTexture2D);
            return jsonTexture2D;
        }
    }
}