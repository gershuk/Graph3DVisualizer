// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Grpah3DVisualizer.GraphTasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grpah3DVisualizer.Scene
{
    public class SceneController : MonoBehaviour
    {
        private Dictionary<string, Assembly> _asseblies;
        private Type _currentTaskType;
        private List<Type> _taskList;
        private AbstractVisualTask _visualTask;

        public Type CurrentTaskType
        {
            get => _currentTaskType;
            set => _currentTaskType = value == null || value.IsSubclassOf(typeof(AbstractVisualTask)) ? value : throw new Exception($"You can't cast {value.Name} to a VisualTask");
        }

        public List<Type> TaskList { get => _taskList; private set => _taskList = value; }

        public AbstractVisualTask VisualTask { get => _visualTask; private set => _visualTask = value; }

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

        public void CreateTask ()
        {
            if (_currentTaskType != null)
            {
                var gameObject = new GameObject("VisualTask");
                _visualTask = (AbstractVisualTask) gameObject.AddComponent(_currentTaskType);
                _visualTask.InitTask();
            }
        }

        public void FindAllTasks ()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            _taskList = new List<Type>();
            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(AbstractVisualTask)))
                        _taskList.Add(type);
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
    }
}