using System;
using System.Collections;
using System.Collections.ObjectModel;

using UnityEngine;

namespace SupportComponents
{
    public class MoveComponent : MonoBehaviour, IMoveable
    {
        private const float _thresholdDistance = 0.1f;
        private Vector3 _rotation;
        private Transform _transform;
        private bool _isMoving;
        [SerializeField]
        private float _movingSpeed = 10;

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

        public float MovingSpeed { get => _movingSpeed; set => _movingSpeed = value; }
        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

        public event Action<Vector3, UnityEngine.Object> OnObjectMove;

        private void Awake ()
        {
            _rotation = Vector3.zero;
            _isMoving = false;
            _transform = transform;
        }

        public IEnumerator MoveAlongTrajectory (ReadOnlyCollection<Vector3> trajectory)
        {
            _isMoving = true;
            foreach (var point in trajectory)
            {
                while (Vector3.Distance(_transform.position, point) > _thresholdDistance)
                {
                    var moveDir = (point - _transform.position) / Vector3.Distance(point, _transform.position);

                    var newCoord = Vector3.Distance(point, _transform.position) > Time.deltaTime * MovingSpeed
                        ? _transform.position + moveDir * Time.deltaTime * MovingSpeed
                        : point;

                    _transform.position = newCoord;
                    OnObjectMove?.Invoke(newCoord, this);
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
                    OnObjectMove?.Invoke(value, this);
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
                    OnObjectMove?.Invoke(_transform.position, this);
                }
            }
            get => _transform.localPosition;
        }

        public void Translate (Vector3 directionVector, float deltaTime)
        {
            if (!_isMoving && directionVector != Vector3.zero)
            {
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
