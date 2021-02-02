using System;

using UnityEngine;

namespace SupportComponents
{
    public class Button3DComponnet : ClickableObject
    {
        //ToDo : change to expression
        public Action<GameObject> Action { get; set; }

        public override void SetDisabled () { }
        protected override void ClickAction (GameObject gameObject) => Action?.Invoke(gameObject);
    }
}
