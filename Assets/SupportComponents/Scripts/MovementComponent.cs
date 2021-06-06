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

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Graph3DVisualizer.SupportComponents
{
    /// <summary>
    /// Component that controls the movement of the object.
    /// </summary>
    public class MovementComponent : MonoBehaviour, IMoveable
    {
        private const float _thresholdDistance = 0.1f;
        private const float _timeCheckThreshold = 0.001f;

        [SerializeField]
        private int _currentGearIndex;

        private Vector3 _eulerAngles;
        private List<(float deltaTimeFromStart, float multiplier)> _gears = new List<(float deltaTimeFromStart, float multiplier)>(4) { (0f, 1f), (10, 3f), (20, 5f), (40, 8f) };

        private bool _isMoving;

        private float _lastTimeCheck;

        [SerializeField]
        [Range(-360, 360)]
        private float _maxRotX = 360F;

        [SerializeField]
        [Range(-360, 360)]
        private float _maxRotY = 360F;

        [SerializeField]
        [Range(-360, 360)]
        private float _minRotX = -360;

        [SerializeField]
        [Range(-360, 360)]
        private float _minRotY = -360;

        [SerializeField]
        private float _movingSpeed = 10;

        private float _startMovingTime;
        private Transform _transform;
        private TransmissionMode _transmissionMode;

        public event Action<Vector3, UnityEngine.Object>? ObjectEulerAnglesChanged;

        public event Action<Vector3, UnityEngine.Object>? ObjectPositionChanged;

        public int CurrentGearIndex
        {
            get => _currentGearIndex;
            set
            {
                if (TransmissionMode == TransmissionMode.Manual)
                {
                    _currentGearIndex = (value > -1 && value < Gears.Count) ? value : throw new ArgumentOutOfRangeException($"Max gear is {Gears.Count}");
                }
                else
                {
                    Debug.LogError($"Transmission in {TransmissionMode} mode, you can't change gear");
                }
            }
        }

        public List<(float deltaTimeFromStart, float multiplier)> Gears
        {
            get => _gears;
            private set
            {
                if (value == null)
                    throw new NullReferenceException();

                _gears = value.Count > 0 ? value : throw new Exception("Gears list size must be greater than 0");
            }
        }

        public Vector3 GlobalCoordinates
        {
            set
            {
                if (!_isMoving && _transform.position != value)
                {
                    _transform.position = value;
                    ObjectPositionChanged?.Invoke(value, this);
                }
            }
            get => _transform.position;
        }

        public Vector3 GlobalEulerAngles
        {
            get => _eulerAngles;
            set
            {
                if (_eulerAngles != value)
                {
                    _eulerAngles = value;
                    _transform.eulerAngles = _eulerAngles;
                    ObjectEulerAnglesChanged?.Invoke(value, this);
                }
            }
        }

        public Vector3 LocalCoordinates
        {
            set
            {
                if (!_isMoving && _transform.localPosition != value)
                {
                    _transform.localPosition = value;
                    ObjectPositionChanged?.Invoke(_transform.position, this);
                }
            }
            get => _transform.localPosition;
        }

        public float MovingSpeed { get => _movingSpeed * Gears[CurrentGearIndex].multiplier; set => _movingSpeed = value; }
        public float RotationSpeed { get; set; }

        public TransmissionMode TransmissionMode
        {
            get => _transmissionMode;
            set
            {
                _transmissionMode = value;
                switch (_transmissionMode)
                {
                    //use a field instead of a property because the property only switches in manual mode
                    case TransmissionMode.Manual:
                    case TransmissionMode.Auto:
                    case TransmissionMode.FirstGear:
                        _currentGearIndex = 0;
                        break;

                    case TransmissionMode.TopGear:
                        _currentGearIndex = Gears.Count - 1;
                        break;
                }
            }
        }

        private void Awake ()
        {
            Gears = new List<(float deltaTimeFromStart, float multiplier)>(4) { (0f, 1f), (10, 3f), (20, 5f), (40, 8f) };
            TransmissionMode = TransmissionMode.Auto;
            _startMovingTime = -1;
            _lastTimeCheck = -1;

            _isMoving = false;
            _transform = transform;
            GlobalEulerAngles = _transform.eulerAngles;
        }

        private void UpdateParametersWhileMoving (float deltaTime)
        {
            if (Mathf.Abs(_lastTimeCheck + deltaTime - Time.time) < _timeCheckThreshold)
            {
                _lastTimeCheck = Time.time;
                if (TransmissionMode == TransmissionMode.Auto)
                {
                    while (_currentGearIndex + 1 < Gears.Count && Gears[_currentGearIndex + 1].deltaTimeFromStart < Time.time - _startMovingTime)
                    {
                        _currentGearIndex++;
                    }
                }
            }
            else
            {
                _startMovingTime = Time.time;
                _lastTimeCheck = Time.time;
                if (TransmissionMode == TransmissionMode.Auto)
                    _currentGearIndex = 0;
            }
        }

        public IEnumerator MoveAlongTrajectory (IReadOnlyList<Vector3> trajectory)
        {
            if (_isMoving)
                yield break;
            _isMoving = true;
            foreach (var point in trajectory)
            {
                while (Vector3.Distance(_transform.position, point) > _thresholdDistance)
                {
                    var moveDir = (point - _transform.position) / Vector3.Distance(point, _transform.position);

                    UpdateParametersWhileMoving(Time.deltaTime);

                    var newCoord = Vector3.Distance(point, _transform.position) > Time.deltaTime * MovingSpeed
                        ? _transform.position + moveDir * Time.deltaTime * MovingSpeed
                        : point;

                    _transform.position = newCoord;
                    ObjectPositionChanged?.Invoke(newCoord, this);
                    yield return null;
                }
            }

            _isMoving = false;

            yield break;
        }

        public void Rotate (Vector2 rotationChange, float deltaTime)
        {
            var rotDiv = rotationChange * RotationSpeed * deltaTime;

            var eulerAngles = new Vector3((GlobalEulerAngles.x - rotDiv.y) % 360f, (GlobalEulerAngles.y + rotDiv.x) % 360f, 0);
            eulerAngles = new Vector3(Mathf.Min(eulerAngles.x, _maxRotX), Mathf.Min(eulerAngles.y, _maxRotY), 0);
            eulerAngles = new Vector3(Mathf.Max(eulerAngles.x, _minRotX), Mathf.Max(eulerAngles.y, _minRotY), 0);
            GlobalEulerAngles = eulerAngles;
        }

        public void Translate (Vector3 directionVector, float deltaTime, Transform? relativeTo = default)
        {
            relativeTo ??= _transform;
            if (!_isMoving && directionVector != Vector3.zero)
            {
                UpdateParametersWhileMoving(deltaTime);
                _transform.Translate(directionVector * MovingSpeed * deltaTime, relativeTo);
                ObjectPositionChanged?.Invoke(_transform.position, this);
            }
        }
    }

    public enum TransmissionMode
    {
        Auto = 0,
        Manual = 1,
        FirstGear = 2,
        TopGear = 3,
    }
}