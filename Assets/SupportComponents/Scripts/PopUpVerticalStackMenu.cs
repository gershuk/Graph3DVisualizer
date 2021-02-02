using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace SupportComponents
{
    public class PopUpVerticalStackMenu : ClickableObject
    {
        protected List<(float offset, Transform transform)> _subObjects;
        protected GameObject _plane;

        public float PlainOffset { get; set; } = 12;

        private void Awake ()
        {
            _transform = transform;
            _gameObject = gameObject;
            _subObjects = new List<(float offset, Transform transform)>();
        }

        protected override void ClickAction (GameObject gameObject) => _plane.SetActive(true);

        public void SetSubObjectList (IReadOnlyList<(float offset, Transform transform)> subObjects, Transform source)
        {
            if (_plane)
                Destroy(_plane);
            _subObjects.Clear();

            _plane = new GameObject("Plane");
            _plane.transform.parent = _transform;
            _plane.transform.localPosition = new Vector3(0, PlainOffset, 0);
            var lookAtScript = _plane.AddComponent<LookAtObject>();
            lookAtScript.TargetObject = source;
            lookAtScript.VectorWorldUp = Vector3.up;
            var incOffset = 0f;
            foreach (var (offset, transform) in subObjects)
            {
                transform.parent = _plane.transform;
                incOffset += offset;
                transform.localPosition = new Vector3(0, incOffset, 0);
            }

            _subObjects.AddRange(subObjects);
        }

        public override void SetDisabled () => _plane.SetActive(false);
    }
}
