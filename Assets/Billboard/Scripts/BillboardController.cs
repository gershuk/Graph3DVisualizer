// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

using SupportComponents;

using TextureFactory;

using UnityEngine;

namespace Grpah3DVisualizer
{
    /// <summary>
    /// Parameters for creating a new <see cref="Billboard"/> object.
    /// </summary>
    public sealed class BillboardParameters : CustomizableParameter
    {
        /// <summary>
        /// Used to set position of images on the Billboard.
        /// </summary>
        public CombinedImages CombinedImages { get; }

        /// <summary>
        /// Used to set Billboard size in units.
        /// </summary>
        public Vector2 Scale { get; }

        /// <summary>
        /// Used to determine the lower bound of the texel clipping, based on the alpha channel summary.
        /// </summary>
        public float Cutoff { get; }

        /// <summary>
        /// Used to determine whether the display texture is compressed.
        /// </summary>
        public bool Compressed { get; }

        /// <summary>
        /// Used to determine image output mode. If true, image is displayed in one color, false-according to the texture.
        /// </summary>
        public bool IsMonoColor { get; }

        /// <summary>
        /// Used to determine image color in MonoColor mode.
        /// </summary>
        public Color MonoColor { get; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="combinedImage">
        /// Used to set position of images on the Billboard.
        /// </param>
        /// <param name="scale">
        /// Used to set Billboard size in units.
        /// </param>
        /// <param name="cutoff">
        /// Used to determine the lower bound of the texel clipping, based on the alpha channel value.
        /// </param>
        /// <param name="compressed">
        /// Used to determine whether the display texture is compressed.
        /// </param>
        /// <param name="isMonoColor">
        /// Used to determine image output mode. If true, image is displayed in one color, false-according to the texture.
        /// </param>
        /// <param name="monoColor">
        /// Used to determine image color in MonoColor mode.</param>
        public BillboardParameters (CombinedImages combinedImage, Vector2 scale, float cutoff, bool compressed, bool isMonoColor, Color monoColor)
        {
            CombinedImages = combinedImage ?? throw new ArgumentNullException(nameof(combinedImage));
            Scale = scale;
            Cutoff = cutoff;
            Compressed = compressed;
            IsMonoColor = isMonoColor;
            MonoColor = monoColor;
        }
    }

    /// <summary>
    /// An object containing information for displaying the Billboard image. Contained in the <see cref="BillboardController"/>.
    /// </summary>
    public sealed class Billboard : ICustomizable<BillboardParameters>
    {
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private const string _cutoff = "_Cutoff";
        private const string _mainTextureName = "_MainTex";
        private const string _isMonoColor = "_IsMonoColor";
        private const string _monoColor = "_MonoColor";
        private CombinedImages _combinedImage;

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

        public void SetupParams (BillboardParameters billboardParameters)
        {
            var texture = Texture2DExtension.CombineTextures(billboardParameters.CombinedImages);
            _combinedImage = billboardParameters.CombinedImages;

            if (billboardParameters.Compressed)
                texture.Compress(false);

            Material.mainTexture = texture;
            ScaleX = billboardParameters.Scale.x;
            ScaleY = billboardParameters.Scale.y;
            Cutoff = billboardParameters.Cutoff;

            IsMonoColor = billboardParameters.IsMonoColor;
            MonoColor = billboardParameters.MonoColor;
        }

        public BillboardParameters DownloadParams () =>
            new BillboardParameters((CombinedImages) _combinedImage.Clone(), new Vector2(ScaleX, ScaleY), Cutoff, IsMonoColor, IsMonoColor, MonoColor);

        public Billboard (in BillboardParameters parameters, Shader shader, Texture2D defaultTexture)
        {
            Material = new Material(shader) { mainTexture = defaultTexture };
            SetupParams(parameters);
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

    /// <summary>
    /// A collection containing <see cref="Billboard"/>s.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BillboardController : MonoBehaviour, IVisibile
    {
        private const string _billboardDefaultTexturePath = "Textures/BillboardDefaultTexture";
        private const string _billboardShaderPath = "Custom/BillboardShader";
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

            _shader = _shader == null ? Shader.Find(_billboardShaderPath) : _shader;
            _defaultTexture = _defaultTexture == null ? Resources.Load<Texture2D>(_billboardDefaultTexturePath) : _defaultTexture;
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

        //ToDo : Rewrite to multiple meshes
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
