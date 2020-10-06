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
        private float _speed = 10;

        [SerializeField]
        [Range(-360, 360)]
        private float _minimumX = -360;
        [SerializeField]
        [Range(-360, 360)]
        private float _maximumX = 360F;

        [SerializeField]
        [Range(-360, 360)]
        private float _minimumY = -360;
        [SerializeField]
        [Range(-360, 360)]
        private float _maximumY = 360F;

        [SerializeField]
        private float _rotationSpeed = 1f;

        public float Speed { get => _speed; set => _speed = value; }
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

                    var newCoord = Vector3.Distance(point, _transform.position) > Time.deltaTime * Speed
                        ? _transform.position + moveDir * Time.deltaTime * Speed
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
            if (!_isMoving)
            {
                _transform.Translate(directionVector * Speed * deltaTime);
            }
        }

        public void Rotate (Vector2 rotationChange, float deltaTime)
        {
            var rotDiv = rotationChange * RotationSpeed * deltaTime;
            _rotation.x = (_rotation.x - rotDiv.y) % 360f;
            _rotation.y = (_rotation.y + rotDiv.x) % 360f;

            _rotation.x = Mathf.Min(_rotation.x, _maximumX);
            _rotation.y = Mathf.Min(_rotation.y, _maximumY);

            _rotation.x = Mathf.Max(_rotation.x, _minimumX);
            _rotation.y = Mathf.Max(_rotation.y, _minimumY);

            _transform.eulerAngles = _rotation;
        }
    }
}
