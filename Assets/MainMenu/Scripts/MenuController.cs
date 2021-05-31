using UnityEngine;
using UnityEngine.SceneManagement;

namespace Graph3DVisualizer.MainMenu
{
    public class MenuController : MonoBehaviour
    {
        // Start is called before the first frame update
        public void StartGame ()
        {
            SceneManager.LoadScene("Hub", LoadSceneMode.Single);
        }
    }
}