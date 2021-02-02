using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SupportComponents
{
    public class LookAtObject : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _vectorUp = Vector3.up;

        public Transform TargetObject { get; set; }
        public Vector3 VectorWorldUp { get => _vectorUp; set => _vectorUp = value; }

        private void LateUpdate ()
        {
            if (TargetObject)
                transform.LookAt(TargetObject, VectorWorldUp);
        }
    }
}
