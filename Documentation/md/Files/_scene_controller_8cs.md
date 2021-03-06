---
title: Assets/SceneController/Scripts/SceneController.cs

---

# Assets/SceneController/Scripts/SceneController.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::Scene](Namespaces/namespace_graph3_d_visualizer_1_1_scene.md)**  |
| **[System::IO](Namespaces/namespace_system_1_1_i_o.md)**  |
| **[System::Reflection](Namespaces/namespace_system_1_1_reflection.md)**  |
| **[System::Runtime::Serialization::Formatters::Binary](Namespaces/namespace_system_1_1_runtime_1_1_serialization_1_1_formatters_1_1_binary.md)**  |
| **[Newtonsoft::Json](Namespaces/namespace_newtonsoft_1_1_json.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::Scene::SceneController](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md)** <br>Component that manages the loading/saving of AbstractVisualTask.  |
| class | **[Graph3DVisualizer::Scene::SceneControllerParameters](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller_parameters.md)** <br>Class that describes [SceneController](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md) parameters for ICustomizable<TParams>.  |




## Source code

```cpp
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Graph3D.SurrogateTypesForSerialization;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.GraphTasks;

using Newtonsoft.Json;

using UnityEngine;

namespace Graph3DVisualizer.Scene
{
    [CustomizableGrandType(Type = typeof(SceneControllerParameters))]
    public class SceneController : MonoBehaviour, ICustomizable<SceneControllerParameters>
    {
        private static readonly List<JsonConverter> _jsonConverters = new List<JsonConverter>(4)
        {
            new NewtonsoftSurrogateConverter<Vector2, JsonVector2>(),
            new NewtonsoftSurrogateConverter<Vector2Int, JsonVector2Int>(),
            new NewtonsoftSurrogateConverter<Vector3,JsonVector3>(),
            new NewtonsoftSurrogateConverter<Color,JsonColor>(),
            new NewtonsoftSurrogateConverter<Quaternion,JsonQuaternion>(),
        };

        private static SurrogateSelector _surrogateSelector;

        private AbstractVisualTask _activeTask;
        private Dictionary<string, Assembly> _asseblies;
        private List<Type> _taskList;

        public AbstractVisualTask ActiveTask { get => _activeTask; private set => _activeTask = value; }
        public List<Type> TaskList { get => _taskList; private set => _taskList = value; }

        private void Awake ()
        {
            _asseblies = new Dictionary<string, Assembly>();
            _surrogateSelector = new SurrogateSelector();
            _surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new SurrogateVector2());
            _surrogateSelector.AddSurrogate(typeof(Vector2Int), new StreamingContext(StreamingContextStates.All), new SurrogateVector2Int());
            _surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), new SurrogateVector3());
            _surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new SurrogateColor());
            _surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), new SurrogateQuaternion());
            _surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new SurrogateTexture2D());
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

        public SceneControllerParameters DownloadParams () => new SceneControllerParameters(_activeTask.GetType(), (VisualTaskParameters) CustomizableExtension.CallDownloadParams(_activeTask));

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

        public void Load ()
        {
        }

        public void LoadBinary (string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter { SurrogateSelector = _surrogateSelector };
                var parameters = (SceneControllerParameters) formatter.Deserialize(fs);
                SetupParams(parameters);
            }
        }

        //public void StopAllTasks ()
        //{
        //    foreach (var acitveTask in ActiveTasks)
        //        acitveTask.StopTask();
        //    ActiveTasks.Clear();
        //}
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

        public void SaveBinary (string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter { SurrogateSelector = _surrogateSelector };
                formatter.Serialize(fs, DownloadParams());
            }
        }

        public void SaveJson (string path)
        {
            using (var sw = new StreamWriter(path))
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, ReferenceLoopHandling = ReferenceLoopHandling.Error, Converters = _jsonConverters };
                sw.WriteLine(JsonConvert.SerializeObject(DownloadParams(), Formatting.Indented, settings));
            }
        }

        public void SetupParams (SceneControllerParameters parameters)
        {
            var index = TaskList.FindIndex(x => x == parameters.TaskType);
            if (index > -1)
            {
                StartTask(index, parameters.VisualTaskParameters);
            }
        }

        public void StartTask (int taskIndex, VisualTaskParameters visualTaskParameters = null)
        {
            var gameObject = new GameObject("VisualTask");
            ActiveTask = (AbstractVisualTask) gameObject.AddComponent(TaskList[taskIndex]);
            if (visualTaskParameters == null)
                ActiveTask.InitTask();
            else
                CustomizableExtension.CallSetUpParams(ActiveTask, visualTaskParameters);
        }

        public void StopTask ()
        {
            _activeTask.DestroyTask();
            Destroy(_activeTask.gameObject);
        }

        //public void StopTask<T> (Predicate<T> predicate) where T : AbstractVisualTask
        //{
        //    foreach (var acitveTask in ActiveTasks)
        //    {
        //        if (acitveTask is T task && predicate(task))
        //        {
        //            ActiveTasks.Remove(task);
        //            task.StopTask();
        //        }
        //    }
        //}
    }

    [Serializable]
    public class SceneControllerParameters : AbstractCustomizableParameter
    {
        public Type TaskType { get; }
        public VisualTaskParameters VisualTaskParameters { get; }

        public SceneControllerParameters (Type taskType, VisualTaskParameters visualTaskParameters)
        {
            TaskType = taskType ?? throw new ArgumentNullException(nameof(taskType));
            VisualTaskParameters = visualTaskParameters ?? throw new ArgumentNullException(nameof(visualTaskParameters));
        }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (����)
