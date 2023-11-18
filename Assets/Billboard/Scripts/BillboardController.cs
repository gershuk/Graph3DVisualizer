// This file is part of Graph3DVisualizer.
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
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.Billboards
{
    /// <summary>
    /// A collection containing <see cref="Billboard"/>s.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BillboardController : MonoBehaviour, IVisibile
    {
        private readonly Dictionary<BillboardId, Billboard> _billboards = new();

        private MeshFilter _meshFilter;

        private MeshRenderer _render;

        //ToDo : remove with immutable billboard
        [SerializeField]
        protected static Mesh _billboardMesh;

        public event Action<bool, UnityEngine.Object>? VisibleChanged;

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
            Array.Resize(ref materials, materials.Length + 1);
            materials[^1] = billboard.Material;
            _render.sharedMaterials = materials;
            billboard.Material = _render.sharedMaterials[^1];
            billboard.ScaleChanged += UpdateBounds;
            UpdateBounds();
        }

        private void Awake ()
        {
            if (_billboardMesh == null)
                _billboardMesh = MeshCreater.CreateQuadMesh();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.sharedMesh = MeshCreater.CreateQuadMesh();
            _render = GetComponent<MeshRenderer>();
            _render.sharedMaterials = new Material[0];
        }

        private void OnDestroy ()
        {
            foreach (var billboard in _billboards)
            {
                if (!CacheForCustomizableObjects.ContainsValue<BillboardParameters>(billboard))
                    billboard.Value.Dispose();
            }
        }

        //ToDo : Redo it so that the billboard adds or removes itself from the cache.
        public BillboardId CreateBillboard (BillboardParameters parameters)
        {
            BillboardId billboardId = new(parameters.Name, parameters.Description);

            Billboard? billboard;
            if (parameters.UseCash
                && CacheForCustomizableObjects.TryGetValue(parameters, out var customizableObject))
            {
                billboard = (customizableObject as Billboard) ?? throw new NullReferenceException();
            }
            else
            {
                billboard = new();
                billboard.SetupParams(parameters);
                if (parameters.UseCash)
                    CacheForCustomizableObjects.Add(parameters, billboard);
            }

            if (billboard == null)
                throw new NullReferenceException();

            _billboards.Add(billboardId, billboard);
            AddBillboardMaterialToRender(billboardId);
            return billboardId;
        }

        public void DeleteBillboard (BillboardId id)
        {
            DisableBillboard(id);
            _billboards.Remove(id);
            UpdateBounds();
        }

        public void DisableBillboard (BillboardId id)
        {
            var disabledBillboard = GetBillboard(id);
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

        public void EnableBillboard (BillboardId id) => AddBillboardMaterialToRender(id);

        public Billboard GetBillboard (BillboardId id) => _billboards[id];

        public void RemoveFromCache (BillboardParameters billboardParameters) =>
            CacheForCustomizableObjects.Remove(billboardParameters, true);

        //ToDo : Rewrite to multiple meshes
        public void UpdateBounds ()
        {
            var bounds = _meshFilter.sharedMesh.bounds;
            var newValue = 0f;
            foreach (var billboard in _billboards)
            {
                newValue = Mathf.Max(billboard.Value.ScaleX + (billboard.Value.Offset.x * 2),
                                     billboard.Value.ScaleY + (billboard.Value.Offset.y * 2),
                                     newValue);
            }
            bounds.size = new(newValue, newValue, newValue);
            _meshFilter.sharedMesh.bounds = bounds;
        }
    }

    /// <summary>
    /// Id to search for a billboard in <see cref="BillboardController"/>.
    /// </summary>
    public class BillboardId
    {
        public string Description { get; }

        public string Name { get; }

        public BillboardId (string name, string description)
        {
            Name = name ?? Guid.NewGuid().ToString();
            Description = description;
        }
    }
}