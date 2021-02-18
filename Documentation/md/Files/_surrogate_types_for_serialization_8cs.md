---
title: Assets/SurrogateTypesForSerialization/Scripts/SurrogateTypesForSerialization.cs

---

# Assets/SurrogateTypesForSerialization/Scripts/SurrogateTypesForSerialization.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3D](Namespaces/namespace_graph3_d.md)**  |
| **[Graph3D::SurrogateTypesForSerialization](Namespaces/namespace_graph3_d_1_1_surrogate_types_for_serialization.md)**  |
| **[Newtonsoft::Json::Linq](Namespaces/namespace_newtonsoft_1_1_json_1_1_linq.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[Graph3D::SurrogateTypesForSerialization::JsonColor](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md)**  |
| struct | **[Graph3D::SurrogateTypesForSerialization::JsonQuaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md)**  |
| struct | **[Graph3D::SurrogateTypesForSerialization::JsonVector2](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector2.md)**  |
| struct | **[Graph3D::SurrogateTypesForSerialization::JsonVector2Int](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector2_int.md)**  |
| struct | **[Graph3D::SurrogateTypesForSerialization::JsonVector3](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector3.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::NewtonsoftSurrogateConverter](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_newtonsoft_surrogate_converter.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateColor](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_color.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateQuaternion](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_quaternion.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateTexture2D](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_texture2_d.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateVector2](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector2.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateVector2Int](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector2_int.md)**  |
| class | **[Graph3D::SurrogateTypesForSerialization::SurrogateVector3](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector3.md)**  |




## Source code

```cpp
using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Graph3D.SurrogateTypesForSerialization
{
    public struct JsonColor
    {
        public float A { get; set; }
        public float B { get; set; }
        public float G { get; set; }
        public float R { get; set; }

        public JsonColor (float r, float g, float b, float a) => (R, G, B, A) = (r, g, b, a);

        public static implicit operator Color (JsonColor jsonColor) => new Color(jsonColor.R, jsonColor.G, jsonColor.B, jsonColor.A);

        public static implicit operator JsonColor (Color color) => new JsonColor(color.r, color.g, color.b, color.a);
    }

    public struct JsonQuaternion
    {
        public float W { get; set; }
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public JsonQuaternion (float x, float y, float z, float w) => (X, Y, Z, W) = (x, y, z, w);

        public static implicit operator JsonQuaternion (Quaternion color) => new JsonQuaternion(color.x, color.y, color.z, color.w);

        public static implicit operator Quaternion (JsonQuaternion jsonQuaternion) => new Quaternion(jsonQuaternion.X, jsonQuaternion.Y, jsonQuaternion.Z, jsonQuaternion.W);
    }

    public struct JsonVector2
    {
        public float X { get; set; }

        public float Y { get; set; }

        public JsonVector2 (float x, float y) => (X, Y) = (x, y);

        public static implicit operator JsonVector2 (Vector2 vector2) => new JsonVector2(vector2.x, vector2.y);

        public static implicit operator Vector2 (JsonVector2 jsonVector2) => new Vector2(jsonVector2.X, jsonVector2.Y);
    }

    public struct JsonVector2Int
    {
        public int X { get; set; }

        public int Y { get; set; }

        public JsonVector2Int (int x, int y) => (X, Y) = (x, y);

        public static implicit operator JsonVector2Int (Vector2Int vector2) => new JsonVector2Int(vector2.x, vector2.y);

        public static implicit operator Vector2Int (JsonVector2Int jsonVector2Int) => new Vector2Int(jsonVector2Int.X, jsonVector2Int.Y);
    }

    public struct JsonVector3
    {
        public float X { get; set; }

        public float Y { get; set; }
        public float Z { get; set; }

        public JsonVector3 (float x, float y, float z) => (X, Y, Z) = (x, y, z);

        public static implicit operator JsonVector3 (Vector3 vector3) => new JsonVector3(vector3.x, vector3.y, vector3.z);

        public static implicit operator Vector3 (JsonVector3 jsonVector3) => new Vector3(jsonVector3.X, jsonVector3.Y, jsonVector3.Z);
    }

    public class NewtonsoftSurrogateConverter<TReal, TSurrogate> : JsonConverter
    {
        public override bool CanConvert (Type objectType) => objectType == typeof(TReal);

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            reader.ValueType == typeof(TSurrogate) ? (TReal) (dynamic) reader.Value : reader.Value;

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) => JToken.FromObject((TSurrogate) (dynamic) value).WriteTo(writer);
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

    //ToDo : add support for json
    public class SurrogateTexture2D : ISerializationSurrogate
    {
        //public int Width { get; set; }
        //public int Height { get; set; }
        //public int MipmapCount { get; set; }
        //public TextureFormat TextureFormat { get; set; }
        //public SurrogateColor[] Pixels { get; set; }

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
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
