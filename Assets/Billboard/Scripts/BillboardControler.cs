using System;

using TextureFactory;

using UnityEngine;

namespace Grpah3DVisualser
{
    public readonly struct BillboardParameters
    {
        public int TextureWidth { get; }
        public int TextureHeight { get; }

        public float ScaleX { get; }
        public float ScaleY { get; }

        public float Cutoff { get; }

        public (Texture2D Texture, Vector2Int Position)[] Images { get; }

        public BillboardParameters (int textureWidth, int textureHeight, float scaleX, float scaleY, float cutoff, (Texture2D Texture, Vector2Int Position)[] images)
        {
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Cutoff = cutoff;
            Images = images ?? throw new ArgumentNullException(nameof(images));
        }
    }

    public interface IBillboardControler
    {
        float Cutoff { get; set; }
        float ScaleX { get; set; }
        float ScaleY { get; set; }
        Vector2 TextureOffset { get; set; }
        Vector2 TextureScale { get; set; }

        void SetUpBillboard (BillboardParameters billboardParameters);
    }

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    [ExecuteInEditMode]
    public sealed class BillboardControler : MonoBehaviour, IBillboardControler
    {
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private const string _cutoff = "_Cutoff";
        private const string _mainTextureName = "_MainTex";

        private static Texture2D _defaultTexture;
        private static Shader _shader;

        private MeshRenderer _render;

        private void Awake ()
        {
            _shader = _shader == null ? Shader.Find("Custom/BillboardShader") : _shader;
            _defaultTexture = _defaultTexture == null ? Resources.Load<Texture2D>("Textures/BillboardDefaultTexture") : _defaultTexture;

            var material = new Material(_shader) { mainTexture = _defaultTexture };
            material.SetFloat(_scaleX, 27.59f);
            material.SetFloat(_scaleY, 10f);
            material.SetFloat(_cutoff, 0.687f);

            _render = GetComponent<MeshRenderer>();
            _render.material = material;

            //ToDo Set Mesh Filter to Quad
        }

        public Texture MainTexture
        {
            set => _render.material.mainTexture = value;
            get => _render.material.mainTexture;
        }

        public float ScaleX
        {
            set => _render.material.SetFloat(_scaleX, value);
            get => _render.material.GetFloat(_scaleX);
        }

        public float ScaleY
        {
            set => _render.material.SetFloat(_scaleY, value);
            get => _render.material.GetFloat(_scaleY);
        }

        public float Cutoff
        {
            set
            {
                if (value > 1 || value < 0)
                    throw new Exception("Cutoff parameter out of range");
                _render.material.SetFloat(_cutoff, value);
            }
            get => _render.material.GetFloat(_cutoff);
        }

        public Vector2 TextureOffset
        {
            set => _render.material.SetTextureOffset(_mainTextureName, value);
            get => _render.material.GetTextureOffset(_mainTextureName);
        }

        public Vector2 TextureScale
        {
            set => _render.material.SetTextureScale(_mainTextureName, value);
            get => _render.material.GetTextureScale(_mainTextureName);
        }

        public void SetUpBillboard (BillboardParameters billboardParameters)
        {
            _render.material.mainTexture = Texture2DExtension.CombineTextures(billboardParameters.Images,
                                                                              billboardParameters.TextureWidth,
                                                                              billboardParameters.TextureHeight);

            ScaleX = billboardParameters.ScaleX;
            ScaleY = billboardParameters.ScaleY;
            Cutoff = billboardParameters.Cutoff;
        }
    }
}
