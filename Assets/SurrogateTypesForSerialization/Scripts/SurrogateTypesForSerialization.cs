using System;
using System.Linq;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3D.SurrogateTypesForSerialization
{
    public struct SurrogateColor : ISerializationSurrogate
    {
        public float A { get; set; }
        public float B { get; set; }
        public float G { get; set; }
        public float R { get; set; }

        public SurrogateColor (float r, float g, float b, float a) => (R, G, B, A) = (r, g, b, a);

        public static implicit operator Color (SurrogateColor surrogateColor) => new Color(surrogateColor.R, surrogateColor.G, surrogateColor.B, surrogateColor.A);

        public static implicit operator SurrogateColor (Color color) => new SurrogateColor(color.r, color.g, color.b, color.a);

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

    public struct SurrogateQuaternion : ISerializationSurrogate
    {
        public float W { get; set; }
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public SurrogateQuaternion (float x, float y, float z, float w) => (X, Y, Z, W) = (x, y, z, w);

        public static implicit operator Quaternion (SurrogateQuaternion surrogateQuaternion) => new Quaternion(surrogateQuaternion.X, surrogateQuaternion.Y, surrogateQuaternion.Z, surrogateQuaternion.W);

        public static implicit operator SurrogateQuaternion (Quaternion color) => new SurrogateQuaternion(color.x, color.y, color.z, color.w);

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

    public struct SurrogateVector2 : ISerializationSurrogate
    {
        public float X { get; set; }

        public float Y { get; set; }

        public SurrogateVector2 (float x, float y) => (X, Y) = (x, y);

        public static implicit operator SurrogateVector2 (Vector2 vector2) => new SurrogateVector2(vector2.x, vector2.y);

        public static implicit operator Vector2 (SurrogateVector2 simplifedVector2) => new Vector2(simplifedVector2.X, simplifedVector2.Y);

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

    public struct SurrogateVector2Int : ISerializationSurrogate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public SurrogateVector2Int (int x, int y) => (X, Y) = (x, y);

        public static implicit operator SurrogateVector2Int (Vector2Int vector2) => new SurrogateVector2Int(vector2.x, vector2.y);

        public static implicit operator Vector2Int (SurrogateVector2Int simplifedVector2) => new Vector2Int(simplifedVector2.X, simplifedVector2.Y);

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

    public struct SurrogateVector3 : ISerializationSurrogate
    {
        public float X { get; set; }

        public float Y { get; set; }
        public float Z { get; set; }

        public SurrogateVector3 (float x, float y, float z) => (X, Y, Z) = (x, y, z);

        public static implicit operator SurrogateVector3 (Vector3 vector3) => new SurrogateVector3(vector3.x, vector3.y, vector3.z);

        public static implicit operator Vector3 (SurrogateVector3 simplifedVector3) => new Vector3(simplifedVector3.X, simplifedVector3.Y, simplifedVector3.Z);

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

    //ToDo : add support for json
    public struct SurrogateTexture2D : ISerializationSurrogate
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int MipmapCount { get; set; }
        public TextureFormat TextureFormat { get; set; }
        public SurrogateColor[] Pixels { get; set; }

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

    public class NewtonsoftSurrogateConverter<TReal, TSurrogate> : JsonConverter
    {
        public override bool CanConvert (Type objectType) => objectType == typeof(TReal);

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            reader.ValueType == typeof(TSurrogate) ? (TReal) (dynamic) reader.Value : reader.Value;

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) => JToken.FromObject((TSurrogate) (dynamic) value).WriteTo(writer);
    }
}