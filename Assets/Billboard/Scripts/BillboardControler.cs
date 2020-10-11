using System;

using SupportComponents;

using TextureFactory;

using UnityEngine;

namespace Grpah3DVisualser
{
    public readonly struct BillboardParameters
    {
        public (Texture2D Texture, Vector2Int Position)[] Images { get; }

        public int TextureWidth { get; }
        public int TextureHeight { get; }

        public float ScaleX { get; }
        public float ScaleY { get; }

        public float Cutoff { get; }

        public bool Compressed { get; }

        public TextureWrapMode TextureWrapMode { get; }

        public BillboardParameters ((Texture2D Texture, Vector2Int Position)[] images, int textureWidth, int textureHeight,
            float scaleX, float scaleY, float cutoff, bool compressed, TextureWrapMode wrapMode)
        {
            Images = images ?? throw new ArgumentNullException(nameof(images));
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Cutoff = cutoff;
            Compressed = compressed;
            TextureWrapMode = wrapMode;
        }
    }

    public interface IBillboardControler
    {
        float Cutoff { get; set; }
        float ScaleX { get; set; }
        float ScaleY { get; set; }
        Vector2 TextureOffset { get; set; }
        Vector2 TextureScale { get; set; }

        void SetUpBillboard (in BillboardParameters billboardParameters);
    }

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]

    [ExecuteInEditMode]
    public sealed class BillboardControler : MonoBehaviour, IBillboardControler, IVisibile
    {
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private const string _cutoff = "_Cutoff";
        private const string _mainTextureName = "_MainTex";

        [SerializeField]
        private static Shader _shader;
        [SerializeField]
        private static Texture2D _defaultTexture;

        private MeshRenderer _render;
        private MeshFilter _meshFilter;

        public event Action<bool, UnityEngine.Object> OnVisibleChange;

        public Texture MainTexture
        {
            set => _render.material.mainTexture = value;
            get => _render.material.mainTexture;
        }

        public float ScaleX
        {
            set
            {
                var bounds = _meshFilter.mesh.bounds;
                var newValue = Mathf.Max(value, ScaleY);
                bounds.size = new Vector3(newValue, newValue, newValue);
                _meshFilter.mesh.bounds = bounds;
                _render.material.SetFloat(_scaleX, value);
            }
            get => _render.material.GetFloat(_scaleX);
        }

        public float ScaleY
        {
            set
            {
                var bounds = _meshFilter.mesh.bounds;
                var newValue = Mathf.Max(value, ScaleX);
                bounds.size = new Vector3(newValue, newValue, newValue);
                _meshFilter.mesh.bounds = bounds;
                _render.material.SetFloat(_scaleY, value);
            }
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

        public void SetUpBillboard (in BillboardParameters billboardParameters)
        {
            var texture = Texture2DExtension.CombineTextures(billboardParameters.Images,
                                                             billboardParameters.TextureWidth,
                                                             billboardParameters.TextureHeight,
                                                             billboardParameters.TextureWrapMode);

            if (billboardParameters.Compressed)
                texture.Compress(false);

            _render.material.mainTexture = texture;
            ScaleX = billboardParameters.ScaleX;
            ScaleY = billboardParameters.ScaleY;
            Cutoff = billboardParameters.Cutoff;
        }

        public bool Visibility
        {
            set
            {
                if (_render.enabled != value)
                {
                    _render.enabled = value;
                    OnVisibleChange?.Invoke(value, this);
                }
            }
            get => _render.enabled;
        }

        private void Awake ()
        {
            _shader = _shader == null ? Shader.Find("Custom/BillboardShader") : _shader;
            _defaultTexture = _defaultTexture == null ? Resources.Load<Texture2D>("Textures/BillboardDefaultTexture") : _defaultTexture;
            _meshFilter = GetComponent<MeshFilter>();

            var material = new Material(_shader) { mainTexture = _defaultTexture };
            material.SetFloat(_scaleX, 1f);
            material.SetFloat(_scaleY, 1f);
            material.SetFloat(_cutoff, 0.687f);

            _render = GetComponent<MeshRenderer>();
            _render.material = material;

            var bounds = _meshFilter.mesh.bounds;
            bounds.size = new Vector3(0.5f, 0.5f, 0.5f);
            _meshFilter.mesh.bounds = bounds;
        }
    }
}
