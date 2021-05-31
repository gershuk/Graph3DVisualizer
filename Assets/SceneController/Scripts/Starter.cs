using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class Starter : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start ()
        {
            SceneLoader.Instance.LoadScene<HubScene>();
            Destroy(gameObject);
        }
    }
}