using UnityEngine;

namespace TextureFactory
{
    public static class Texture2DExtension
    {
        public static Texture2D CombineTextures ((Texture2D Texture, Vector2Int Position)[] images, int textureWidth, int textureHeight)
        {
            var newTexture = new Texture2D(textureWidth, textureHeight);
            //ToDo: too slow, need to replace
            //for (var j=0;j<textureHeight; ++j)
            //{
            //    for (var i=0; i<textureWidth; ++i)
            //    {
            //        newTexture.SetPixel(i, j, Color.clear);
            //    }
            //}

            foreach (var image in images)
            {
                newTexture.SetPixels32(image.Position.x, image.Position.y,
                                     image.Texture.width, image.Texture.height,
                                     image.Texture.GetPixels32());
            }

            newTexture.Apply();

            return newTexture;
        }

        public static Texture2D ResizeTexture (Texture2D texture, int textureWidth, int textureHeight)
        {
            var renderTexture = RenderTexture.GetTemporary(textureWidth, textureHeight);
            renderTexture.filterMode = FilterMode.Point;
            RenderTexture.active = renderTexture;

            Graphics.Blit(texture, renderTexture);

            var newTexture = new Texture2D(textureWidth, textureHeight);
            newTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
            newTexture.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);

            return newTexture;
        }
    }
}