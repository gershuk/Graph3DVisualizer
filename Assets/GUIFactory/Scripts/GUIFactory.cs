using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Grpah3DVisualser
{
    public static class GUIFactory
    {
        public readonly struct RectTransformParameters
        {
            public Transform Parent { get; }
            public Vector2 AnchorMin { get; }
            public Vector2 AnchorMax { get; }
            public Vector2 SizeDelta { get; }
            public Vector2 AnchoredPosition { get; }

            public RectTransformParameters (Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 sizeDelta, Vector2 anchoredPosition)
            {
                Parent = parent;
                AnchorMin = anchorMin;
                AnchorMax = anchorMax;
                SizeDelta = sizeDelta;
                AnchoredPosition = anchoredPosition;
            }
        }

        public static void SetUpRectTransform (this RectTransform rectTransform, in RectTransformParameters parameters)
        {
            rectTransform.SetParent(parameters.Parent);
            rectTransform.anchorMin = parameters.AnchorMin;
            rectTransform.anchorMax = parameters.AnchorMax;
            rectTransform.sizeDelta = parameters.SizeDelta;
            rectTransform.anchoredPosition = parameters.AnchoredPosition;
        }

        public readonly struct TextParameters
        {
            public string Text { get; }
            public Color Color { get; }
            public Font Font { get; }
            public TextAnchor Anchor { get; }
            public int Size { get; }
            public RectTransformParameters RectTransformParameters { get; }

            public TextParameters (string text, Color color, Font font, TextAnchor anchor, int size, in RectTransformParameters rectTransformParameters)
            {
                Text = text ?? throw new ArgumentNullException(nameof(text));
                Color = color;
                Font = font != null ? font : throw new ArgumentNullException(nameof(font));
                Anchor = anchor;
                Size = size;
                RectTransformParameters = rectTransformParameters;
            }
        }

        public static GameObject CreateText (in TextParameters parameters)
        {
            var newText = new GameObject($"{parameters.Text}Text", typeof(Text));
            newText.GetComponent<RectTransform>().SetUpRectTransform(parameters.RectTransformParameters);
            var textComponent = newText.GetComponent<Text>();
            textComponent.text = $"{parameters.Text}";
            textComponent.color = parameters.Color;
            textComponent.font = parameters.Font;
            textComponent.alignment = parameters.Anchor;
            textComponent.fontSize = parameters.Size;
            return newText;
        }

        public readonly struct ButtonParameters
        {
            public string Name { get; }
            public RectTransformParameters RectTransformParameters { get; }
            public UnityAction OnClickFunction { get; }

            public ButtonParameters (string name, in RectTransformParameters rectTransformParameters, UnityAction onClickFunction)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                RectTransformParameters = rectTransformParameters;
                OnClickFunction = onClickFunction;
            }
        }

        public static GameObject CreateButton (in ButtonParameters parameters)
        {
            var newButton = new GameObject($"{parameters.Name}Button", typeof(Image), typeof(Button), typeof(LayoutElement));
            newButton.GetComponent<Button>().onClick.AddListener(parameters.OnClickFunction);
            newButton.GetComponent<RectTransform>().SetUpRectTransform(parameters.RectTransformParameters);
            return newButton;
        }
    }
}
