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

using UnityEngine;

using static UnityEngine.Physics;

namespace Graph3DVisualizer.PlayerInputControls
{
    /// <summary>
    /// A component that simulates a laser pointer. Simplifies the use of tools in VR.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LaserPointer : MonoBehaviour
    {
        private const string _cutoff = "_Cutoff";

        private const string _edgeShaderPath = "Custom/EdgeShader";

        private const string _lineTexturePath = "Textures/Line";

        private const string _monoColorStateName = "_IsMonoColor";

        private GameObject _gameObject;

        private LaserState _laserState;

        private LineRenderer _lineRender;

        private Material _material;

        [SerializeField]
        private float _range;

        private Shader _shader;

        private Texture2D _texture2D;

        [SerializeField]
        private float _width;

        public LaserState LaserState
        {
            get => _laserState;
            set
            {
                if (_laserState != value)
                {
                    _laserState = value;

                    switch (value)
                    {
                        case LaserState.On:
                            _lineRender.enabled = true;
                            break;

                        case LaserState.Off:
                            _lineRender.enabled = false;
                            break;
                    }
                }
            }
        }

        public float Range { get => _range; set => _range = value; }
        public Color RayColor { get => _material.GetColor("_MonoColor"); set => _material.SetColor("_MonoColor", value); }

        private void Awake ()
        {
            _gameObject = gameObject;

            _texture2D = _texture2D == null ? Resources.Load<Texture2D>(_lineTexturePath) : _texture2D;
            _shader = _shader == null ? Shader.Find(_edgeShaderPath) : _shader;

            _material = new(_shader) { mainTexture = _texture2D };
            _material.SetFloat(_cutoff, 0.8f);
            _material.SetFloat(_monoColorStateName, Convert.ToSingle(true));
            RayColor = Color.red;

            _lineRender = GetComponent<LineRenderer>();
            if (_lineRender == null)
                _lineRender = _gameObject.AddComponent<LineRenderer>();

            _lineRender.sharedMaterial = _material;
            _material = _lineRender.sharedMaterial;

            _lineRender.positionCount = 2;
            _lineRender.useWorldSpace = false;
            _lineRender.endWidth = _width;
            _lineRender.startWidth = _width;
        }

        private void LateUpdate () => Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _, Range);
    }

    public enum LaserState
    {
        On = 0,
        Off = 1,
    }
}