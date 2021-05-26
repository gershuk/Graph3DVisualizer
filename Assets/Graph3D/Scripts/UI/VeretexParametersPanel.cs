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

        private void Awake ()
        {
            _defaultTexture ??= Texture2DExtension.ResizeTexture(Resources.Load<Texture2D>(_mainTexture), 200, 200);
            _button.onClick.AddListener(CreateVertex);
        }

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

            var imageParameters = new BillboardParameters(texture, scale: _scale.Vector * 3f);
            var selectFrameParameters = new BillboardParameters(selectFrame, scale: _scale.Vector * 6f, isMonoColor: true, useCache: false);

            var text = TextTextureFactory.MakeTextTexture(_name.text, true);
            float scale = 10;
            var textParameters = new BillboardParameters(text, new Vector4(0, -Mathf.Max(_scale.Vector.x, _scale.Vector.y) - 10, 0, 0), new Vector2(scale, text.height * 1.0f / text.width * scale));

            var currentVertex = AbstractGraph.GetComponent<AbstractGraph>().SpawnVertex<SelectableVertex, SelectableVertexParameters>(
                new SelectableVertexParameters(new[] { imageParameters, textParameters }, selectFrameParameters, _positon.Vector));
        }
    }
}