﻿// This file is part of Graph3DVisualizer.
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

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.GraphTasks;
using Graph3DVisualizer.SurrogateTypesForSerialization;

using UnityEngine;

using Yuzu;
using Yuzu.Json;
using Yuzu.Metadata;

namespace Graph3DVisualizer.Scene
{
    /// <summary>
    /// Component that manages the loading/saving of <see cref="AbstractVisualTask"/>.
    /// </summary>
    [CustomizableGrandType(typeof(SceneControllerParameters))]
    public class SceneController : MonoBehaviour, ICustomizable<SceneControllerParameters>
    {
        private readonly Dictionary<string, Assembly> _asseblies = new Dictionary<string, Assembly>();

        public AbstractVisualTask? ActiveTask { get; private set; }
        public List<Type> TaskList { get; } = new List<Type>();

        private static SurrogateSelector MakeSurrogateSelector ()
        {
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new SurrogateVector2());
            surrogateSelector.AddSurrogate(typeof(Vector2Int), new StreamingContext(StreamingContextStates.All), new SurrogateVector2Int());
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), new SurrogateVector3());
            surrogateSelector.AddSurrogate(typeof(Vector4), new StreamingContext(StreamingContextStates.All), new SurrogateVector4());
            surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new SurrogateColor());
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), new SurrogateQuaternion());
            surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new SurrogateTexture2D());
            surrogateSelector.AddSurrogate(typeof(Shader), new StreamingContext(StreamingContextStates.All), new SurrogateShader());
            return surrogateSelector;
        }

        private static CommonOptions MakeYuzuCommonOptions ()
        {
            var yuzuCommonOptions = new CommonOptions()
            {
                Meta = new MetaOptions().
                AddOverride(typeof(Vector2), o => o.AddAttr(new YuzuAlias("Vector2")).AddItem(nameof(Vector2.x), i => i.AddAttr(new YuzuMember("X"))).
                                                                                      AddItem(nameof(Vector2.y), i => i.AddAttr(new YuzuMember("Y")))).

                AddOverride(typeof(Vector2Int), o => o.AddAttr(new YuzuAlias("Vector2Int")).AddItem(nameof(Vector2Int.x), i => i.AddAttr(new YuzuMember("X"))).
                                                                                      AddItem(nameof(Vector2Int.y), i => i.AddAttr(new YuzuMember("Y")))).

                AddOverride(typeof(Vector3), o => o.AddAttr(new YuzuAlias("Vector3")).AddItem(nameof(Vector3.x), i => i.AddAttr(new YuzuMember("X"))).
                                                                                      AddItem(nameof(Vector3.y), i => i.AddAttr(new YuzuMember("Y"))).
                                                                                      AddItem(nameof(Vector3.z), i => i.AddAttr(new YuzuMember("Z")))).

                AddOverride(typeof(Vector4), o => o.AddAttr(new YuzuAlias("Vector4")).AddItem(nameof(Vector4.x), i => i.AddAttr(new YuzuMember("X"))).
                                                                                      AddItem(nameof(Vector4.y), i => i.AddAttr(new YuzuMember("Y"))).
                                                                                      AddItem(nameof(Vector4.z), i => i.AddAttr(new YuzuMember("Z"))).
                                                                                      AddItem(nameof(Vector4.w), i => i.AddAttr(new YuzuMember("W")))).

                AddOverride(typeof(Quaternion), o => o.AddAttr(new YuzuAlias("Quaternion")).AddItem(nameof(Quaternion.x), i => i.AddAttr(new YuzuMember("X"))).
                                                                                      AddItem(nameof(Quaternion.y), i => i.AddAttr(new YuzuMember("Y"))).
                                                                                      AddItem(nameof(Quaternion.z), i => i.AddAttr(new YuzuMember("Z"))).
                                                                                      AddItem(nameof(Quaternion.w), i => i.AddAttr(new YuzuMember("W")))).

                AddOverride(typeof(Color), o => o.AddAttr(new YuzuAlias("Color")).AddItem(nameof(Color.r), i => i.AddAttr(new YuzuMember("R"))).
                                                                                      AddItem(nameof(Color.g), i => i.AddAttr(new YuzuMember("G"))).
                                                                                      AddItem(nameof(Color.b), i => i.AddAttr(new YuzuMember("B"))).
                                                                                      AddItem(nameof(Color.a), i => i.AddAttr(new YuzuMember("A")))),
                AllowEmptyTypes = true,
            };

            var surrogateTexture2D = Meta.Get(typeof(Texture2D), yuzuCommonOptions).Surrogate;
            surrogateTexture2D.SurrogateType = typeof(JsonTexture2D);
            surrogateTexture2D.FuncTo = JsonTexture2D.ToSurrogate;
            surrogateTexture2D.FuncFrom = JsonTexture2D.FromSurrogate;
            JsonTexture2D.ReinitializeCache();

            var surrogateType = Meta.Get(typeof(Type), yuzuCommonOptions).Surrogate;
            surrogateType.SurrogateType = typeof(JsonSystemType);
            surrogateType.FuncTo = JsonSystemType.ToSurrogate;
            surrogateType.FuncFrom = JsonSystemType.FromSurrogate;

            return yuzuCommonOptions;
        }

        private void Awake () => DontDestroyOnLoad(gameObject);

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

        public SceneControllerParameters DownloadParams () => new SceneControllerParameters(ActiveTask.GetType(), (VisualTaskParameters) CustomizableExtension.CallDownloadParams(ActiveTask));

        public void FindAllTasks ()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(AbstractVisualTask)))
                        TaskList.Add(type);
                }
            }
        }

        public void LoadBinary (string name)
        {
            using var fs = new FileStream(name, FileMode.Open);
            var formatter = new BinaryFormatter { SurrogateSelector = MakeSurrogateSelector() };
            var parameters = (SceneControllerParameters) formatter.Deserialize(fs);
            SetupParams(parameters);
        }

        public void LoadJson (string name)
        {
            using var sr = new StreamReader(name);
            var jsonDeserializer = new JsonDeserializer() { Options = MakeYuzuCommonOptions() };
            SetupParams(jsonDeserializer.FromString<SceneControllerParameters>(sr.ReadToEnd()));
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

        public void SaveBinary (string name)
        {
            using var fs = new FileStream(name, FileMode.Create);
            var formatter = new BinaryFormatter { SurrogateSelector = MakeSurrogateSelector() };
            formatter.Serialize(fs, DownloadParams());
        }

        public void SaveJson (string name)
        {
            using var sw = new StreamWriter(name);
            var jsonSerializer = new JsonSerializer() { Options = MakeYuzuCommonOptions() };
            sw.WriteLine(jsonSerializer.ToString(DownloadParams()));
        }

        public void SetupParams (SceneControllerParameters parameters)
        {
            var index = TaskList.FindIndex(x => x == parameters.TaskType);
            if (index > -1)
            {
                StartTask(index, parameters.VisualTaskParameters);
            }
        }

        public void StartTask (int taskIndex, VisualTaskParameters? visualTaskParameters = default)
        {
            if (ActiveTask != null)
                StopTask();
            var gameObject = new GameObject("VisualTask");
            ActiveTask = (AbstractVisualTask) gameObject.AddComponent(TaskList[taskIndex]);
            if (visualTaskParameters == null)
                ActiveTask.InitTask();
            else
                CustomizableExtension.CallSetUpParams(ActiveTask, visualTaskParameters);
        }

        public void StopTask ()
        {
            CacheForCustomizableObjects.ClearAll(true);

            if (ActiveTask != null)
            {
                ActiveTask.DestroyTask();
                DestroyImmediate(ActiveTask.gameObject);
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Debug.LogWarning("No active tasks");
            }
        }
    }

    /// <summary>
    /// Class that describes <see cref="SceneController"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class SceneControllerParameters : AbstractCustomizableParameter
    {
        public Type TaskType { get; protected set; }

        public VisualTaskParameters VisualTaskParameters { get; protected set; }

        public SceneControllerParameters (Type taskType, VisualTaskParameters visualTaskParameters, string? parameterId = default) : base(parameterId)
        {
            TaskType = taskType ?? throw new ArgumentNullException(nameof(taskType));
            VisualTaskParameters = visualTaskParameters ?? throw new ArgumentNullException(nameof(visualTaskParameters));
        }
    }
}