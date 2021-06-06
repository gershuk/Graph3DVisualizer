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

using Graph3DVisualizer.Graph3D;

using UnityEngine;

using static UnityEngine.ParticleSystem;

#nullable enable

namespace Graph3DVisualizer.SupportComponents
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(BoxCollider))]
    public class TrackRing : AbstractVertex
    {
        private BoxCollider _collider;
        private MainModule _mainModule;
        private MovementComponent _movementComponent;
        private ParticleSystem _particleSystem;
        private bool _visibility = true;

        public override event Action<UnityEngine.Object>? Destroyed;

        public event Action<TrackRing, Collider>? Event;

        public override event Action<bool, UnityEngine.Object>? VisibleChanged;

        public MinMaxGradient Color
        {
            get => _mainModule.startColor;
            set => _mainModule.startColor = value;
        }

        public override MovementComponent MovementComponent { get => _movementComponent; protected set => _movementComponent = value; }

        public Vector3 Scale
        {
            get => _transform.localScale;
            set => _transform.localScale = value;
        }

        public override bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                _collider.enabled = _visibility;

                if (_visibility)
                    _particleSystem.Play();
                else
                    _particleSystem.Stop();

                _collider.enabled = _visibility;
                VisibleChanged?.Invoke(_visible, this);
            }
        }

        private void Awake ()
        {
            _transform = transform;
            _movementComponent = GetComponent<MovementComponent>();
            _particleSystem = GetComponent<ParticleSystem>();
            _mainModule = _particleSystem.main;
            _collider = GetComponent<BoxCollider>();
            _collider.isTrigger = true;
            Visibility = true;
        }

        private void OnTriggerEnter (Collider other) => Event?.Invoke(this, other);
    }
}