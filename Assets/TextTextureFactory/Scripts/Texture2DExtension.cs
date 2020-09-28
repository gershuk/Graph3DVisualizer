using UnityEngine;

namespace TextureFactory
{
    public static class Texture2DExtension
    {
        public static Texture2D CombineTextures ((Texture2D Texture, Rect Rect)[] images, int textureWidth, int textureHeight)
        {
            var renderTexture = RenderTexture.GetTemporary(textureWidth, textureHeight);
            renderTexture.filterMode = FilterMode.Point;
            RenderTexture.active = renderTexture;

            foreach (var image in images)
            {
                var filterMode = image.Texture.filterMode;
                image.Texture.filterMode = FilterMode.Point;
                Graphics.DrawTexture(image.Rect, image.Texture);
                image.Texture.filterMode = filterMode;
            }

            var newTexture = new Texture2D(textureWidth, textureHeight);
            newTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
            newTexture.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);

            return newTexture;
        }
    }
}