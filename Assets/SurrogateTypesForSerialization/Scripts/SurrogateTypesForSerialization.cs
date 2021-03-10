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

using System;
using System.IO;
using System.Runtime.Serialization;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.SurrogateTypesForSerialization
{
    [YuzuAll]
    public class JsonSystemType
    {
        public string Name { get; set; }

        public JsonSystemType (string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

        public static Type FromSurrogate (object obj) => Type.GetType((obj as JsonSystemType).Name);

        public static implicit operator JsonSystemType (Type type) => ToSurrogate(type);

        public static implicit operator Type (JsonSystemType jsonSystemType) => FromSurrogate(jsonSystemType);

        public static JsonSystemType ToSurrogate (object obj) => new JsonSystemType((obj as Type).AssemblyQualifiedName);
    }

    [YuzuAll]
    public class JsonTexture2D
    {
        public string Path { get; set; }

        public JsonTexture2D (string filePath) => Path = filePath ?? Guid.NewGuid().ToString();

        public static Texture2D FromSurrogate (object obj)
        {
            var jsonTexture2D = (JsonTexture2D) obj;
            var texture2D = new Texture2D(1, 1);
            using (var fs = new FileStream(jsonTexture2D.Path, FileMode.Open))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                texture2D.LoadImage(bytes);
            }
            return texture2D;
        }

        public static JsonTexture2D ToSurrogate (object obj)
        {
            var texture2D = (Texture2D) obj;
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

            return new JsonTexture2D(path);
        }
    }

    public class SurrogateColor : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var color = (Color) obj;
            info.AddValue("R", color.r);
            info.AddValue("G", color.g);
            info.AddValue("B", color.b);
            info.AddValue("A", color.a);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var color = (Color) obj;
            color.r = info.GetSingle("R");
            color.g = info.GetSingle("G");
            color.b = info.GetSingle("B");
            color.a = info.GetSingle("A");
            return color;
        }
    }

    public class SurrogateQuaternion : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var quaternion = (Quaternion) obj;
            info.AddValue("W", quaternion.w);
            info.AddValue("X", quaternion.x);
            info.AddValue("Y", quaternion.y);
            info.AddValue("Z", quaternion.z);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var quaternion = (Quaternion) obj;
            quaternion.w = info.GetSingle("W");
            quaternion.x = info.GetSingle("X");
            quaternion.y = info.GetSingle("Y");
            quaternion.z = info.GetSingle("Z");
            return quaternion;
        }
    }

    public class SurrogateTexture2D : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var texture2D = (Texture2D) obj;
            info.AddValue("Width", texture2D.width);
            info.AddValue("Height", texture2D.height);
            info.AddValue("MipmapCount", texture2D.mipmapCount);
            info.AddValue("TextureFormat", texture2D.format);
            info.AddValue("Pixels", texture2D.GetRawTextureData());
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var width = info.GetInt32("Width");
            var height = info.GetInt32("Height");
            var mipMapCount = info.GetInt32("MipmapCount");
            var textureFormat = (TextureFormat) info.GetValue("TextureFormat", typeof(TextureFormat));
            var pixels = (byte[]) info.GetValue("Pixels", typeof(byte[]));
            var texture2D = new Texture2D(width, height, textureFormat, mipMapCount, false);
            texture2D.LoadRawTextureData(pixels);
            texture2D.Apply();
            return texture2D;
        }
    }

    public class SurrogateVector2 : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var vector2 = (Vector2) obj;
            info.AddValue("X", vector2.x);
            info.AddValue("Y", vector2.y);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector2 = (Vector2) obj;
            vector2.x = info.GetSingle("X");
            vector2.y = info.GetSingle("Y");
            return vector2;
        }
    }

    public class SurrogateVector2Int : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var vector2 = (Vector2Int) obj;
            info.AddValue("X", vector2.x);
            info.AddValue("Y", vector2.y);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector2 = (Vector2Int) obj;
            vector2.x = info.GetInt32("X");
            vector2.y = info.GetInt32("Y");
            return vector2;
        }
    }

    public class SurrogateVector3 : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector3) obj;
            info.AddValue("X", vector3.x);
            info.AddValue("Y", vector3.y);
            info.AddValue("Z", vector3.z);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector3 = (Vector3) obj;
            vector3.x = info.GetSingle("X");
            vector3.y = info.GetSingle("Y");
            vector3.z = info.GetSingle("Z");
            return vector3;
        }
    }

    public class SurrogateVector4 : ISerializationSurrogate
    {
        public void GetObjectData (object obj, SerializationInfo info, StreamingContext context)
        {
            var vector4 = (Vector4) obj;
            info.AddValue("W", vector4.w);
            info.AddValue("X", vector4.x);
            info.AddValue("Y", vector4.y);
            info.AddValue("Z", vector4.z);
        }

        public object SetObjectData (object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector4 = (Vector4) obj;
            vector4.w = info.GetSingle("W");
            vector4.x = info.GetSingle("X");
            vector4.y = info.GetSingle("Y");
            vector4.z = info.GetSingle("Z");
            return vector4;
        }
    }
}