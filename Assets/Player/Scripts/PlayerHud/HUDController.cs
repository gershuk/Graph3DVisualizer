using System;

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.Player.HUD
{
    public class HUDController : MonoBehaviour, IVisibile
    {
        [SerializeField]
        private float _dist;

        [SerializeField]
        private GameObject _head;

        [SerializeField]
        private ObjectInfoPanelController _objectInfoPanel;

        [SerializeField]
        private TaskPanelController _taskPanel;

        [SerializeField]
        private ToolsPanelController _toolsPanel;

        private Transform _transform;
        private bool _visibility = true;

        public event Action<bool, UnityEngine.Object> VisibleChanged;

        public float Dist { get => _dist; set => _dist = value; }

        public string SceneInfo
        {
            get => _taskPanel.Text;
            set => _taskPanel.Text = value;
        }

        public bool Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                    return;

                _visibility = value;

                _objectInfoPanel.Visibility = value;
                _taskPanel.Visibility = value;
                _toolsPanel.Visibility = value;

                VisibleChanged?.Invoke(value, this);

                transform.position = _head.transform.position + _head.transform.forward * Dist;
            }
        }

        private void Awake ()
        {
            _transform = transform;
            _transform.position = _head.transform.position + _head.transform.forward * Dist;
        }

        private void LateUpdate ()
        {
            const float eps = 0.2f;
            var targetPoint = _head.transform.position + _head.transform.forward * Dist;

            if (Vector3.Distance(transform.position, targetPoint) > eps)
                _transform.position = Vector3.Lerp(_transform.position, targetPoint, Time.deltaTime);

            transform.LookAt(_head.transform, Vector3.up);
        }
    }
}