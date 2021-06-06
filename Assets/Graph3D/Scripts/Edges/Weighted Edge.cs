// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
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

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.Graph3D
{
    [CustomizableGrandType(typeof(WeightedEdgeParameters))]
    public sealed class WeightedEdge : StretchableEdge, ICustomizable<WeightedEdgeParameters>
    {
        private static TextTextureFactory? _textTextureFactory;

        private BillboardController _billboardController;
        private GameObject _childBillboard;
        private float? _value;
        private BillboardId? _valueId;

        public float? Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                if (_valueId != null)
                    _billboardController.DeleteBillboard(_valueId);
                _value = value;

                if (_value.HasValue)
                {
                    const float scale = 5;
                    var texture = _textTextureFactory.MakeTextTexture(_value.ToString(), true);
                    _valueId = _billboardController.CreateBillboard(new BillboardParameters(texture,
                                                                                            new Vector4(0, Width, 0, 1),
                                                                                            new Vector2(scale, texture.height * 1.0f / texture.width * scale),
                                                                                            0.1f,
                                                                                            true,
                                                                                            Color.blue));
                }
            }
        }

        protected override void Awake ()
        {
            base.Awake();
            _childBillboard = new GameObject("ChildBilllboard");
            _childBillboard.transform.parent = transform;
            _childBillboard.transform.localPosition = Vector3.zero;
            _billboardController = _childBillboard.AddComponent<BillboardController>();
            _textTextureFactory ??= new TextTextureFactory(FontsGenerator.GetOrCreateFont("Arial", 48), 0);
            Value = 0;
        }

        WeightedEdgeParameters ICustomizable<WeightedEdgeParameters>.DownloadParams (Dictionary<Guid, object> writeCache) =>
            new WeightedEdgeParameters((this as ICustomizable<StretchableEdgeParameters>).DownloadParams(writeCache), Value);

        public void SetupParams (WeightedEdgeParameters parameters)
        {
            (this as ICustomizable<StretchableEdgeParameters>).SetupParams(parameters);
            Value = parameters.Value;
        }
    }

    public sealed class WeightedEdgeParameters : StretchableEdgeParameters
    {
        public float? Value { get; private set; }

        public WeightedEdgeParameters (StretchableEdgeParameters stretchableEdgeParameters, float? value) :
            base(stretchableEdgeParameters, stretchableEdgeParameters.HeadLength) => Value = value;
    }
}