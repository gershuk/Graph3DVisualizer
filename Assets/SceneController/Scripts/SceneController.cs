using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using GraphTasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grpah3DVisualser
{
    public class SceneController : MonoBehaviour
    {
        private Dictionary<string, Assembly> _asseblies;
        private Type _currentTaskType;
        private VisualTask _visualTask;
        private List<Type> _taskList;

        public Type CurrentTask
        {
            get => _currentTaskType;
            set => _currentTaskType = value.IsSubclassOf(typeof(VisualTask)) ? value : throw new Exception($"You can't cast {value.Name} to a VisualTask");
        }

        public List<Type> TaskList { get => _taskList; private set => _taskList = value; }

        public VisualTask VisualTask { get => _visualTask; private set => _visualTask = value; }

        public Type CurrentTaskType { get => _currentTaskType; set => _currentTaskType = value; }

        private void Awake ()
        {
            _asseblies = new Dictionary<string, Assembly>();
            DontDestroyOnLoad(gameObject);
        }

        private void LoadAssemblies (params (string name, string path)[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                try
                {
                    _asseblies[assembly.name] = !_asseblies.ContainsKey(name) ? Assembly.LoadFrom(assembly.path) : throw new Exception("This assembly alias is already used");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{ex} - {assembly.path} can't be loaded");
                }
            }

        }

        public void LoadMods ()
        {
            var assembliesPath = Application.dataPath + "/ModAssemblies";
            if (Directory.Exists(assembliesPath))
            {
                LoadAssemblies(Directory.GetFiles(assembliesPath, "*.dll").Select((path) => (path, path)).ToArray());
            }
            else
            {
                Directory.CreateDirectory(assembliesPath);
            }
        }

        public void LoadScene (string name) => SceneManager.LoadScene(name);

        public void SetTaskByName (string name) => CurrentTask = Type.GetType(name);

        public void FindAllTasks ()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            _taskList = new List<Type>();
            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(VisualTask)))
                        _taskList.Add(type);
                }
            }
        }

        public void CreateTask ()
        {
            if (_currentTaskType != null)
            {
                var gameObject = new GameObject("VisualTask");
                _visualTask = (VisualTask) gameObject.AddComponent(_currentTaskType);
                _visualTask.InitTask();
            }
        }
    }
}
