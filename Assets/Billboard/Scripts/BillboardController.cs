using System;
using System.Collections.Generic;

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

        public Vector2 Scale { get; }

        public float Cutoff { get; }

        public bool Compressed { get; }

        public TextureWrapMode TextureWrapMode { get; }

        public bool IsMonoColor { get; }

        public Color MonoColor { get; }

        public BillboardParameters ((Texture2D Texture, Vector2Int Position)[] images, int textureWidth, int textureHeight,
            Vector2 scale, float cutoff, bool compressed, TextureWrapMode wrapMode, bool isMonoColor, Color monoColor)
        {
            Images = images ?? throw new ArgumentNullException(nameof(images));
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            Scale = scale;
            Cutoff = cutoff;
            Compressed = compressed;
            TextureWrapMode = wrapMode;
            IsMonoColor = isMonoColor;
            MonoColor = monoColor;
        }
    }

    public sealed class Billboard
    {
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private const string _cutoff = "_Cutoff";
        private const string _mainTextureName = "_MainTex";
        private const string _isMonoColor = "_IsMonoColor";
        private const string _monoColor = "_MonoColor";

        public Material Material { get; set; }

        public Action ScaleChanged;

        public Texture MainTexture
        {
            get => Material.mainTexture;
            set => Material.mainTexture = value;
        }

        public float ScaleX
        {
            get => Material.GetFloat(_scaleX);
            set
            {
                Material.SetFloat(_scaleX, value);
                ScaleChanged?.Invoke();
            }
        }

        public float ScaleY
        {
            get => Material.GetFloat(_scaleY);
            set
            {
                Material.SetFloat(_scaleY, value);
                ScaleChanged?.Invoke();
            }
        }

        public float Cutoff
        {
            get => Material.GetFloat(_cutoff);
            set
            {
                if (value > 1 || value < 0)
                    throw new Exception("Cutoff parameter out of range");
                Material.SetFloat(_cutoff, value);
            }
        }

        public Vector2 TextureOffset
        {
            get => Material.GetTextureOffset(_mainTextureName);
            set => Material.SetTextureOffset(_mainTextureName, value);
        }

        public Vector2 TextureScale
        {
            get => Material.GetTextureScale(_mainTextureName);
            set => Material.SetTextureScale(_mainTextureName, value);
        }

        public bool IsMonoColor
        {
            get => Convert.ToBoolean(Material.GetFloat(_isMonoColor));
            set => Material.SetFloat(_isMonoColor, Convert.ToSingle(value));
        }

        public Color MonoColor
        {
            get => Material.GetColor(_monoColor);
            set => Material.SetColor(_monoColor, value);
        }

        public void SetUpBillboard (in BillboardParameters billboardParameters)
        {
            var texture = Texture2DExtension.CombineTextures(billboardParameters.Images,
                                                             billboardParameters.TextureWidth,
                                                             billboardParameters.TextureHeight,
                                                             billboardParameters.TextureWrapMode);

            if (billboardParameters.Compressed)
                texture.Compress(false);

            Material.mainTexture = texture;
            ScaleX = billboardParameters.Scale.x;
            ScaleY = billboardParameters.Scale.y;
            Cutoff = billboardParameters.Cutoff;

            IsMonoColor = billboardParameters.IsMonoColor;
            MonoColor = billboardParameters.MonoColor;
        }


        public Billboard (in BillboardParameters parameters, Shader shader, Texture2D defaultTexture)
        {
            Material = new Material(shader) { mainTexture = defaultTexture };
            SetUpBillboard(parameters);
        }
    }

    public class BillboardId
    {
        public string Name { get; }
        public string Description { get; }

        public BillboardId (string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }

    [ExecuteInEditMode]
    public class BillboardController : MonoBehaviour, IVisibile
    {
        private Dictionary<BillboardId, Billboard> _billboards;
        private MeshRenderer _render;
        private MeshFilter _meshFilter;

        [SerializeField]
        private static Shader _shader;
        [SerializeField]
        private static Texture2D _defaultTexture;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        public bool Visibility 
        {
            get => _render.enabled;
            set
            {
                if (_render.enabled != value)
                {
                    _render.enabled = value;
                    VisibleChanged?.Invoke(value, this);
                }
            }
        }

        private void Awake ()
        {
            _billboards = new Dictionary<BillboardId, Billboard>();

            _meshFilter = GetComponent<MeshFilter>();
            _render = GetComponent<MeshRenderer>();

            var bounds = _meshFilter.mesh.bounds;
            bounds.size = new Vector3(0.5f, 0.5f, 0.5f);
            _meshFilter.mesh.bounds = bounds;

            _shader = _shader == null ? Shader.Find("Custom/BillboardShader") : _shader;
            _defaultTexture = _defaultTexture == null ? Resources.Load<Texture2D>("Textures/BillboardDefaultTexture") : _defaultTexture;
        }

        private void AddBillboardMaterialToRender (BillboardId billboardId)
        {
            var billboard = _billboards[billboardId];
            var materials = _render.sharedMaterials;
            var size = materials.Length + 1;
            var newMaterials = new Material[size];
            materials.CopyTo(newMaterials, 0);
            newMaterials[size - 1] = billboard.Material;
            _render.sharedMaterials = newMaterials;

            billboard.Material = _render.sharedMaterials[size - 1];

            billboard.ScaleChanged += UpdateBounds;
            UpdateBounds();
        }

        public BillboardId CreateBillboard (in BillboardParameters parameters, string name, string description)
        {
            var billboardId = new BillboardId(name, description);
            var billboard = new Billboard(parameters, _shader, _defaultTexture);
            _billboards.Add(billboardId, billboard);

            AddBillboardMaterialToRender(billboardId);

            return billboardId;
        }

        public Billboard GetBillboard (BillboardId billboardId) => _billboards[billboardId];

        public void DeleteBillboard (BillboardId billboardId)
        {
            DisableBillboard(billboardId);
            _billboards.Remove(billboardId);
            UpdateBounds();
        }

        public void UpdateBounds ()
        {
            var bounds = _meshFilter.mesh.bounds;
            var newValue = 0f;
            foreach (var billboard in _billboards)
            {
                newValue = Mathf.Max(billboard.Value.ScaleX, billboard.Value.ScaleY);               
            }
            bounds.size = new Vector3(newValue, newValue, newValue);
            _meshFilter.mesh.bounds = bounds;
        }

        public void DisableBillboard (BillboardId billboardId)
        {
            var disabledBillboard = GetBillboard(billboardId);
            disabledBillboard.ScaleChanged -= UpdateBounds;
            var newMaterials = new Material[_render.sharedMaterials.Length - 1];
            var i = 0;

            foreach (var material in _render.sharedMaterials)
            {
                if (material != disabledBillboard.Material)
                {
                    newMaterials[i] = material;
                    ++i;
                }
            }

            _render.sharedMaterials = newMaterials;

            UpdateBounds();
        }

        public void EnableBillboard (BillboardId billboardId) => AddBillboardMaterialToRender(billboardId);
    }
}
