using UnityEngine;
using UnityEngine.UI;

namespace Graph3DVisualizer.Player.HUD
{
    public class TaskPanelController : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public string Text { get => _text.text; set => _text.text = value; }
    }
}