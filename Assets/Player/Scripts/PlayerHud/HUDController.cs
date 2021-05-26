using UnityEngine;

namespace Graph3DVisualizer.Player.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        private ObjectInfoPanelController _objectInfoPanel;

        [SerializeField]
        private TaskPanelController _taskPanel;

        [SerializeField]
        private ToolsPanelController _toolsPanel;

        private void Awake ()
        {
        }
    }
}