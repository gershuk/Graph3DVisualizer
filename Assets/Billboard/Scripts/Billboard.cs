﻿// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

#nullable enable

using System;
using System.Collections.Generic;

using Graph3DVisualizer.Customizable;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.Billboards
{    /// <summary>
     /// An object containing information for displaying the Billboard image. Contained in the <see cref="BillboardController"/>.
     /// </summary>
    [CustomizableGrandType(typeof(BillboardParameters))]
    public sealed class Billboard : ICustomizable<BillboardParameters>, IDisposable
    {
        private const string _billboardShaderPath = "Custom/BillboardShader";
        private const string _cutoff = "_Cutoff";
        private const string _isMonoColor = "_IsMonoColor";
        private const string _mainTextureName = "_MainTex";
        private const string _monoColor = "_MonoColor";
        private const string _offset = "_Offset";
        private const string _scaleX = "_ScaleX";
        private const string _scaleY = "_ScaleY";
        private readonly Shader _shader = Shader.Find(_billboardShaderPath);
        private bool _disposed = false;

        public event Action? ScaleChanged;

        public Guid? CacheGuid { get; private set; }

        public float Cutoff
        {
            get => Material.GetFloat(_cutoff);
            set
            {
                if (value is > 1 or < 0)
                    throw new ArgumentOutOfRangeException("Cutoff parameter out of range");
                Material.SetFloat(_cutoff, value);
            }
        }

        public string? Description { get; set; }

        public bool IsMonoColor
        {
            get => Convert.ToBoolean(Material.GetFloat(_isMonoColor));
            set => Material.SetFloat(_isMonoColor, Convert.ToSingle(value));
        }

        public Texture2D MainTexture
        {
            get => (Texture2D) Material.mainTexture;
            set => Material.mainTexture = value;
        }

        public Material? Material { get; set; }

        public Color MonoColor
        {
            get => Material.GetColor(_monoColor);
            set => Material.SetColor(_monoColor, value);
        }

        public string? Name { get; set; }

        public Vector4 Offset
        {
            get => Material.GetVector(_offset);
            set => Material.SetVector(_offset, value);
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

        ~Billboard () => Dispose();

        //Release material
        public void Dispose ()
        {
            if (_disposed)
            {
                return;
            }

            GameObject.Destroy(Material);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        public BillboardParameters DownloadParams (Dictionary<Guid, object> writeCache)
        {
            if (CacheGuid.HasValue)
            {
                if (!writeCache.TryGetValue(CacheGuid.Value, out var billboardParameters))
                {
                    billboardParameters = new BillboardParameters(MainTexture,
                                                                  Offset,
                                                                  new Vector2(ScaleX, ScaleY),
                                                                  Cutoff,
                                                                  IsMonoColor,
                                                                  MonoColor,
                                                                  true,
                                                                  Name,
                                                                  Description,
                                                                  CacheGuid);
                    writeCache.Add(CacheGuid.Value, billboardParameters);
                }

                return (BillboardParameters) billboardParameters;
            }

            return new BillboardParameters(MainTexture,
                                           Offset,
                                           new Vector2(ScaleX, ScaleY),
                                           Cutoff,
                                           IsMonoColor,
                                           MonoColor,
                                           false,
                                           Name,
                                           Description);
        }

        AbstractCustomizableParameter ICustomizable.DownloadParams (Dictionary<Guid, object> writeCache) => throw new NotImplementedException();

        public void SetupParams (BillboardParameters billboardParameters)
        {
            CacheGuid = billboardParameters.UseCash ? billboardParameters.Id : default(Guid?);
            Material = new Material(_shader) { enableInstancing = true };
            MainTexture = billboardParameters.Texture;
            Offset = billboardParameters.Offset;
            ScaleX = billboardParameters.Scale.x;
            ScaleY = billboardParameters.Scale.y;
            Cutoff = billboardParameters.Cutoff;
            IsMonoColor = billboardParameters.IsMonoColor;
            MonoColor = billboardParameters.MonoColor;
            Name = billboardParameters.Name;
            Description = billboardParameters.Description;
        }

        public void SetupParams (AbstractCustomizableParameter parameters) => throw new NotImplementedException();
    }

    /// <summary>
    /// Parameters for creating a new <see cref="Billboard"/> object.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public sealed class BillboardParameters : AbstractCustomizableParameter
    {
        /// <summary>
        /// Used to determine the lower bound of the texel clipping, based on the alpha channel summary.
        /// </summary>
        public float Cutoff { get; private set; }

        public string Description { get; private set; }

        /// <summary>
        /// Used to determine image output mode. If true, image is displayed in one color, false-according to the texture.
        /// </summary>
        public bool IsMonoColor { get; private set; }

        /// <summary>
        /// Used to determine image color in MonoColor mode.
        /// </summary>
        public Color MonoColor { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Used to set the offset of the texture relative to the center of the billboard
        /// </summary>
        public Vector4 Offset { get; private set; }

        /// <summary>
        /// Used to set Billboard size in units.
        /// </summary>
        public Vector2 Scale { get; private set; }

        /// <summary>
        /// The texture that is shown by the billboard.
        /// </summary>
        public Texture2D Texture { get; private set; }

        public bool UseCash { get; private set; }

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="texture">
        /// Used to set  texture that is shown by the billboard.
        /// </param>
        /// <param name="scale">
        /// Used to set Billboard size in units.
        /// </param>
        /// <param name="cutoff">
        /// Used to determine the lower bound of the texel clipping, based on the alpha channel value.
        /// <param name="isMonoColor">
        /// Used to determine image output mode. If true, image is displayed in one color, false-according to the texture.
        /// </param>
        /// <param name="monoColor">
        /// Used to determine image color in MonoColor mode.</param>
        public BillboardParameters (Texture2D texture,
                                    Vector4 offset = default,
                                    Vector2 scale = default,
                                    float cutoff = 0.1f,
                                    bool isMonoColor = false,
                                    Color monoColor = default,
                                    bool useCache = false,
                                    string? name = default,
                                    string? description = default,
                                    Guid? parameterId = default) : base(parameterId)
        {
            Texture = texture;
            Scale = scale;
            Cutoff = cutoff;
            IsMonoColor = isMonoColor;
            MonoColor = monoColor;
            Offset = offset;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            UseCash = useCache;
        }
    }
}