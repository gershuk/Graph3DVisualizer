using UnityEngine;

namespace Grpah3DVisualser
{
    public class TaskManager : MonoBehaviour
    {
        void Start ()
        {
            var task = new GameObject("Task");
            var simpleTask = task.AddComponent<SimpleTask>();
            simpleTask.CreateGraph();
        }
    }
}
