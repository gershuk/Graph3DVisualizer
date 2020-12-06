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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

namespace SupportComponents
{
    public enum TransmissionMode
    {
        Auto = 0,
        Manual = 1,
        FirstGear = 2,
        TopGear = 3,
    }

    public class MoveComponent : MonoBehaviour, IMoveable
    {
        private const float _timeCheckThreshold = 0.001f;

        [SerializeField]
        [Range(-360, 360)]
        private float _minRotX = -360;
        [SerializeField]
        [Range(-360, 360)]
        private float _maxRotX = 360F;
        [SerializeField]
        [Range(-360, 360)]
        private float _minRotY = -360;
        [SerializeField]
        [Range(-360, 360)]
        private float _maxRotY = 360F;
        [SerializeField]
        private float _rotationSpeed = 1f;

        [SerializeField]
        private float _movingSpeed = 10;
        private List<(float deltaTimeFromStart, float multiplier)> _gears;
        [SerializeField]
        private int _currentGearIndex;
        private TransmissionMode _transmissionMode;
        private float _startMovingTime;
        private float _lastTimeCheck;

        private const float _thresholdDistance = 0.1f;
        private Vector3 _rotation;
        private Transform _transform;
        private bool _isMoving;

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

        public int CurrentGearIndex
        {
            get => _currentGearIndex;
            set
            {
                if (TransmissionMode == TransmissionMode.Manual)
                {
                    _currentGearIndex = (value > -1 && value < Gears.Count) ? value : throw new ArgumentOutOfRangeException();
                }
                else
                {
                    Debug.LogError($"Transmission in {TransmissionMode} mode, you can't change gear");
                }
            }
        }

        public TransmissionMode TransmissionMode
        {
            get => _transmissionMode;
            set
            {
                _transmissionMode = value;
                switch (_transmissionMode)
                {
                    //use a field instead of a property because the property only switches in manual mode
                    case TransmissionMode.FirstGear:
                        _currentGearIndex = 0;
                        break;
                    case TransmissionMode.TopGear:
                        _currentGearIndex = Gears.Count - 1;
                        break;
                }
            }
        }

        public float MovingSpeed { get => _movingSpeed * Gears[CurrentGearIndex].multiplier; set => _movingSpeed = value; }

        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

        public event Action<Vector3, UnityEngine.Object> ObjectMoved;

        private void Awake ()
        {
            Gears = new List<(float deltaTimeFromStart, float multiplier)>(4) { (0f, 1f), (10, 1.5f), (20, 2f), (40, 4f) };
            TransmissionMode = TransmissionMode.Auto;
            _startMovingTime = -1;
            _lastTimeCheck = -1;

            _rotation = Vector3.zero;
            _isMoving = false;
            _transform = transform;
        }

        //ToDo : Check only when automatic or always?
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

        public IEnumerator MoveAlongTrajectory (ReadOnlyCollection<Vector3> trajectory)
        {
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
                    ObjectMoved?.Invoke(newCoord, this);
                    yield return null;
                }
            }

            _isMoving = false;

            yield return null;
        }

        public Vector3 GlobalCoordinates
        {
            set
            {
                if (!_isMoving && _transform.position != value)
                {
                    _transform.position = value;
                    ObjectMoved?.Invoke(value, this);
                }
            }
            get => _transform.position;
        }

        public Vector3 LocalCoordinates
        {
            set
            {
                if (!_isMoving && _transform.localPosition != value)
                {
                    _transform.localPosition = value;
                    ObjectMoved?.Invoke(_transform.position, this);
                }
            }
            get => _transform.localPosition;
        }

        public void Translate (Vector3 directionVector, float deltaTime)
        {
            if (!_isMoving && directionVector != Vector3.zero)
            {
                UpdateParametersWhileMoving(deltaTime);
                _transform.Translate(directionVector * MovingSpeed * deltaTime);
            }
        }

        public void Rotate (Vector2 rotationChange, float deltaTime)
        {
            var rotDiv = rotationChange * RotationSpeed * deltaTime;
            _rotation.x = (_rotation.x - rotDiv.y) % 360f;
            _rotation.y = (_rotation.y + rotDiv.x) % 360f;

            _rotation.x = Mathf.Min(_rotation.x, _maxRotX);
            _rotation.y = Mathf.Min(_rotation.y, _maxRotY);

            _rotation.x = Mathf.Max(_rotation.x, _minRotX);
            _rotation.y = Mathf.Max(_rotation.y, _minRotY);

            _rotation.z = 0;

            _transform.eulerAngles = _rotation;
        }
    }
}
