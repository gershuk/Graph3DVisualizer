---
title: Assets/Billboard/Scripts/BillboardController.cs

---

# Assets/Billboard/Scripts/BillboardController.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::Billboards](Namespaces/namespace_graph3_d_visualizer_1_1_billboards.md)**  |
| **[System](Namespaces/namespace_system.md)**  |
| **[System::Collections::Generic](Namespaces/namespace_system_1_1_collections_1_1_generic.md)**  |
| **[UnityEngine](Namespaces/namespace_unity_engine.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::Billboards::Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md)** <br>An object containing information for displaying the [Billboard]() image. Contained in the [BillboardController]().  |
| class | **[Graph3DVisualizer::Billboards::BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md)** <br>A collection containing [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md)s.  |
| class | **[Graph3DVisualizer::Billboards::BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md)** <br>Id to search for a billboard in [BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md).  |
| class | **[Graph3DVisualizer::Billboards::BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md)** <br>Parameters for creating a new [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md) object.  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright В© Gershuk Vladislav 2021.
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
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.SupportComponents;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

namespace Graph3DVisualizer.Billboards
{
    [CustomizableGrandType(Type = typeof(BillboardParameters))]
    public sealed class Billboard : ICustomizable<BillboardParameters>
    {
        private const string _cutoff = "_Cutoff";
        private const string _isMonoColor = "_IsMonoColor";
        private const string _mainTextureName = "_MainTex";
        private const string _monoColor = "_MonoColor";
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private CombinedImages _combinedImage;

        public event Action ScaleChanged;

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

        public bool IsMonoColor
        {
            get => Convert.ToBoolean(Material.GetFloat(_isMonoColor));
            set => Material.SetFloat(_isMonoColor, Convert.ToSingle(value));
        }

        public Texture MainTexture
        {
            get => Material.mainTexture;
            set => Material.mainTexture = value;
        }

        public Material Material { get; set; }

        public Color MonoColor
        {
            get => Material.GetColor(_monoColor);
            set => Material.SetColor(_monoColor, value);
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

        public Billboard (in BillboardParameters parameters, Shader shader, Texture2D defaultTexture)
        {
            Material = new Material(shader) { mainTexture = defaultTexture };
            SetupParams(parameters);
        }

        public BillboardParameters DownloadParams () =>
            new BillboardParameters((CombinedImages) _combinedImage.Clone(), new Vector2(ScaleX, ScaleY), Cutoff, IsMonoColor, IsMonoColor, MonoColor);

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
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BillboardController : MonoBehaviour, IVisibile
    {
        private const string _billboardDefaultTexturePath = "Textures/BillboardDefaultTexture";
        private const string _billboardShaderPath = "Custom/BillboardShader";

        [SerializeField]
        private static Texture2D _defaultTexture;

        [SerializeField]
        private static Shader _shader;

        private Dictionary<BillboardId, Billboard> _billboards;
        private MeshFilter _meshFilter;
        private MeshRenderer _render;

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

        public BillboardId CreateBillboard (in BillboardParameters parameters, string name, string description)
        {
            var billboardId = new BillboardId(name, description);
            var billboard = new Billboard(parameters, _shader, _defaultTexture);
            _billboards.Add(billboardId, billboard);

            AddBillboardMaterialToRender(billboardId);

            return billboardId;
        }

        public void DeleteBillboard (BillboardId billboardId)
        {
            DisableBillboard(billboardId);
            _billboards.Remove(billboardId);
            UpdateBounds();
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

        public Billboard GetBillboard (BillboardId billboardId) => _billboards[billboardId];

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
    }

    public class BillboardId
    {
        public string Description { get; }

        public string Name { get; }

        public BillboardId (string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }

    [Serializable]
    public sealed class BillboardParameters : AbstractCustomizableParameter
    {
        public CombinedImages CombinedImages { get; }

        public bool Compressed { get; }

        public float Cutoff { get; }

        public bool IsMonoColor { get; }

        public Color MonoColor { get; }

        public Vector2 Scale { get; }

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
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
