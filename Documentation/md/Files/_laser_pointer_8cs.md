---
title: Assets/Player/Scripts/PlayerTools/LaserPointer.cs


---

# Assets/Player/Scripts/PlayerTools/LaserPointer.cs







## Namespaces

| Name           |
| -------------- |
| **[PlayerInputControls](Namespaces/namespace_player_input_controls.md)**  |
| **[static](Namespaces/namespacestatic.md)**  |
| **[UnityEngine::Physics](Namespaces/namespace_unity_engine_1_1_physics.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[PlayerInputControls::LaserPointer](Classes/class_player_input_controls_1_1_laser_pointer.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
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

using UnityEngine;

using static UnityEngine.Physics;

namespace PlayerInputControls
{
    public enum LaserState
    {
        On = 0,
        Off = 1,
    }

    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LaserPointer : MonoBehaviour
    {
        [SerializeField]
        private float _range;
        private const string _cutoff = "_Cutoff";
        private const string _monoColorStateName = "_IsMonoColor";
        private Texture2D _texture2D;
        private Shader _shader;
        private Material _material;
        private LineRenderer _lineRender;
        private Transform _transform;
        private GameObject _gameObject;
        private LaserState _laserState;

        public Color RayColor { get => _material.GetColor("_MonoColor"); set => _material.SetColor("_MonoColor", value); }

        public float Range { get => _range; set => _range = value; }

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

        private void Awake ()
        {
            _transform = transform;
            _gameObject = gameObject;

            _texture2D = _texture2D == null ? (Texture2D) Resources.Load("Textures/Line") : _texture2D;
            _shader = _shader == null ? Shader.Find("Custom/EdgeShader") : _shader;

            _material = new Material(_shader) { mainTexture = _texture2D };
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
            _lineRender.endWidth = 0.5f;
            _lineRender.startWidth = 0.5f;

            Range = 1000f;
        }

        private void LateUpdate ()
        {
            Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Range);

            _lineRender.SetPosition(0, Vector3.zero);
            _lineRender.SetPosition(1, hit.transform == null ? Vector3.forward * Range : Vector3.forward * hit.distance);
        }
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
