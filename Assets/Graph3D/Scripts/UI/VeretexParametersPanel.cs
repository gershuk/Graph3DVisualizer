// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

using System.IO;

using Graph3DVisualizer.Billboards;
using Graph3DVisualizer.Gui;
using Graph3DVisualizer.TextureFactory;

using UnityEngine;
using UnityEngine.UI;

namespace Graph3DVisualizer.Graph3D
{
    public class VeretexParametersPanel : MonoBehaviour
    {
        private const string _mainTexture = "Textures/Default";
        private const string _selectFrameTexture = "Textures/SelectFrame";
        private static Texture2D _defaultTexture;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private InputField _imagePath;

        [SerializeField]
        private InputField _name;

        [SerializeField]
        private Vector3View _positon;

        [SerializeField]
        private Vector2View _scale;

        [SerializeField]
        public GameObject AbstractGraph;

        [System.Obsolete]
        private void Awake ()
        {
            _defaultTexture ??= Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 200, 200);
            _button.onClick.AddListener(CreateVertex);
        }

        [System.Obsolete]
        public void CreateVertex ()
        {
            var texture = _defaultTexture;
            if (!string.IsNullOrEmpty(_imagePath.text))
            {
                texture = Texture2D.whiteTexture;
                using var fs = new FileStream(_imagePath.text, FileMode.Open);
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                texture.LoadImage(bytes);
            }

            var customFont = FontsGenerator.GetOrCreateFont("Broadway", 32);
            var selectFrame = Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_selectFrameTexture), 200, 200);
            selectFrame.name = "SelectFrame";

            var TextTextureFactory = new TextTextureFactory(customFont, 0);

            BillboardParameters imageParameters = new(texture, scale: _scale.Vector * 3f);
            var selectFrameParameters = new BillboardParameters(selectFrame, scale: _scale.Vector * 6f, isMonoColor: true, useCache: false);

            var text = TextTextureFactory.MakeTextTexture(_name.text, true);
            var scale = 10f;
            BillboardParameters textParameters = new(text,
                                                     new(0, -Mathf.Max(_scale.Vector.x, _scale.Vector.y) - 10, 0, 0),
                                                     new(scale, text.height * 1.0f / text.width * scale));

            AbstractGraph.GetComponent<AbstractGraph>()
                         .SpawnVertex<SelectableVertex, SelectableVertexParameters>(new(new[] { imageParameters, textParameters },
                                                                                        selectFrameParameters,
                                                                                        _positon.Vector));
        }
    }
}