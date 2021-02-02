using System;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualizer
{
    public static class GUIFactory3D
    {
        public readonly struct TextMeshParameters
        {
            public Transform Parent { get; }
            public Vector3 LocalPos { get; }
            public string ObjectName { get; }
            public string Text { get; }
            public float TextSize { get; }
            public Font Font { get; }
            public int FontSize { get; }
            public float TabSize { get; }
            public Color Color { get; }

            public TextMeshParameters (Transform parent, Vector3 localPos, string objectName, string text, float textSize, Font font, int fontSize, float tabSize, Color color)
            {
                Parent = parent;
                LocalPos = localPos;
                ObjectName = objectName ?? throw new ArgumentNullException(nameof(objectName));
                Text = text ?? throw new ArgumentNullException(nameof(text));
                TextSize = textSize;
                Font = font ?? throw new ArgumentNullException(nameof(font));
                FontSize = fontSize;
                TabSize = tabSize;
                Color = color;
            }
        }

        public static GameObject CreateText (TextMeshParameters parameters)
        {
            var text3D = new GameObject(parameters.ObjectName);
            var text3DMeshComponent = text3D.AddComponent<TextMesh>();
            text3DMeshComponent.text = parameters.Text;
            text3DMeshComponent.characterSize = parameters.TextSize;
            text3DMeshComponent.fontSize = parameters.FontSize;
            text3DMeshComponent.anchor = TextAnchor.MiddleCenter;
            text3DMeshComponent.font = parameters.Font;
            text3DMeshComponent.transform.parent = parameters.Parent;
            text3DMeshComponent.tabSize = parameters.TabSize;
            text3DMeshComponent.color = parameters.Color;
            text3D.transform.localPosition = parameters.LocalPos;
            return text3D;
        }

        public static GameObject CreateButton<T> (TextMeshParameters textParameters) where T : ClickableObject
        {
            var button = CreateText(textParameters);
            var collider = button.AddComponent<BoxCollider>();
            var back = UnityEngine.Object.Instantiate( Resources.Load<GameObject>("Prefabs/Background"));
            back.transform.localScale = new Vector3(collider.size.x, collider.size.y, 0.1f);
            back.transform.parent = button.transform;
            back.transform.localPosition = new Vector3(0, 0, 0.1f);
            button.AddComponent<T>();
            return button;
        }
    }
}
