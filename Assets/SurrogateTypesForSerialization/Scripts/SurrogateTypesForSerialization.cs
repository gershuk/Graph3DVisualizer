using System.Runtime.Serialization;

using UnityEngine;

namespace Graph3DVisualizer.SurrogateTypesForSerialization
{
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
}