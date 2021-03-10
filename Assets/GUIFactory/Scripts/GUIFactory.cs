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

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Graph3DVisualizer.GUI
{
    /// <summary>
    /// Class that encapsulates <see cref="UnityEngine.UI"/> object creation functions.
    /// </summary>
    public static class GUIFactory
    {
        public readonly struct ButtonParameters
        {
            public Image Image { get; }
            public string Name { get; }
            public UnityAction OnClickFunction { get; }
            public RectTransformParameters RectTransformParameters { get; }

            public ButtonParameters (string name = default, in RectTransformParameters rectTransformParameters = default, Image image = default, UnityAction onClickFunction = default)
            {
                Name = name ?? string.Empty;
                RectTransformParameters = rectTransformParameters;
                Image = image;
                OnClickFunction = onClickFunction;
            }
        }

        public readonly struct CanvasParameters
        {
            public Camera Camera { get; }

            public RectTransformParameters RectTransformParameters { get; }

            public RenderMode RenderMode { get; }

            public CanvasParameters (RectTransformParameters rectTransformParameters, RenderMode renderMode, Camera camera)
            {
                RectTransformParameters = rectTransformParameters;
                RenderMode = renderMode;
                Camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            }
        }

        public readonly struct RectTransformParameters
        {
            public Vector2 AnchoredPosition { get; }
            public Vector2 AnchorMax { get; }
            public Vector2 AnchorMin { get; }
            public Transform Parent { get; }
            public Vector2 SizeDelta { get; }

            public RectTransformParameters (Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 sizeDelta, Vector2 anchoredPosition)
            {
                Parent = parent;
                AnchorMin = anchorMin;
                AnchorMax = anchorMax;
                SizeDelta = sizeDelta;
                AnchoredPosition = anchoredPosition;
            }
        }

        public readonly struct TextParameters
        {
            public TextAnchor Anchor { get; }
            public Color Color { get; }
            public Font Font { get; }
            public RectTransformParameters RectTransformParameters { get; }
            public int Size { get; }
            public string Text { get; }

            public TextParameters (string text, Color color, Font font, TextAnchor anchor, int size, in RectTransformParameters rectTransformParameters)
            {
                Text = text ?? string.Empty;
                Color = color;
                Font = font != null ? font : throw new ArgumentNullException(nameof(font));
                Anchor = anchor;
                Size = size;
                RectTransformParameters = rectTransformParameters;
            }
        }

        public static GameObject CreateButton (in ButtonParameters parameters)
        {
            var newButton = new GameObject($"{parameters.Name}Button", typeof(Image), typeof(Button), typeof(LayoutElement));

            var buttonComponent = newButton.GetComponent<Button>();
            if (parameters.OnClickFunction != null)
                buttonComponent.onClick.AddListener(parameters.OnClickFunction);
            if (parameters.Image != null)
                buttonComponent.image = parameters.Image;

            newButton.GetComponent<RectTransform>().SetUpRectTransform(parameters.RectTransformParameters);
            return newButton;
        }

        public static GameObject CreateCanvas (in CanvasParameters parameters)
        {
            var newCanvas = new GameObject("Canvas", typeof(Canvas), typeof(GraphicRaycaster));
            newCanvas.GetComponent<RectTransform>().SetUpRectTransform(parameters.RectTransformParameters);
            var canvas = newCanvas.GetComponent<Canvas>();
            canvas.renderMode = parameters.RenderMode;
            canvas.worldCamera = parameters.Camera;
            return newCanvas;
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

        public static void SetUpRectTransform (this RectTransform rectTransform, in RectTransformParameters parameters)
        {
            rectTransform.SetParent(parameters.Parent);
            rectTransform.anchorMin = parameters.AnchorMin;
            rectTransform.anchorMax = parameters.AnchorMax;
            rectTransform.sizeDelta = parameters.SizeDelta;
            rectTransform.anchoredPosition = parameters.AnchoredPosition;
        }
    }
}