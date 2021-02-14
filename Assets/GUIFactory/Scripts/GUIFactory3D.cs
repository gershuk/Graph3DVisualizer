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

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.GUI
{
    public static class GUIFactory3D
    {
        public readonly struct TextMeshParameters
        {
            public Color Color { get; }
            public Font Font { get; }
            public int FontSize { get; }
            public Vector3 LocalPos { get; }
            public string ObjectName { get; }
            public Transform Parent { get; }
            public float TabSize { get; }
            public string Text { get; }
            public float TextSize { get; }

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

        public static GameObject CreateButton<T> (TextMeshParameters textParameters) where T : ClickableObject
        {
            var button = CreateText(textParameters);
            var collider = button.AddComponent<BoxCollider>();
            var back = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Background"));
            back.transform.localScale = new Vector3(collider.size.x, collider.size.y, 0.1f);
            back.transform.parent = button.transform;
            back.transform.localPosition = new Vector3(0, 0, 0.1f);
            button.AddComponent<T>();
            return button;
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
    }
}