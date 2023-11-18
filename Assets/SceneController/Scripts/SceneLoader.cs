// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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
using Graph3DVisualizer.TypesForSerialization.SurrogateTypes;
using Graph3DVisualizer.TypesForSerialization.YuzuTypes;

using UnityEngine;

using Yuzu;
using Yuzu.Json;
using Yuzu.Metadata;

namespace Graph3DVisualizer.SceneController
{
    /// <summary>
    /// Class that describes <see cref="SceneLoader"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class SceneControllerParameters : AbstractCustomizableParameter
    {
        public SceneParameters SceneParameters { get; protected set; }
        public Type SceneType { get; protected set; }

        public SceneControllerParameters (Type taskType, SceneParameters visualTaskParameters, Guid? parameterId = default) : base(parameterId)
        {
            SceneType = taskType ?? throw new ArgumentNullException(nameof(taskType));
            SceneParameters = visualTaskParameters ?? throw new ArgumentNullException(nameof(visualTaskParameters));
        }
    }

    /// <summary>
    /// Component that manages the loading/saving of <see cref="AbstractSceneController"/>.
    /// </summary>
    [CustomizableGrandType(typeof(SceneControllerParameters))]
    public class SceneLoader : ICustomizable<SceneControllerParameters>
    {
        private readonly Dictionary<string, Assembly> _assemblies = new();

        public static SceneLoader Instance { get; private set; }

        public AbstractSceneController? ActiveController { get; private set; }

        public List<Type> ControllersList { get; } = new List<Type>();

        static SceneLoader ()
        {
            Instance = new SceneLoader();
            Instance.LoadMods();
        }

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

        private void LoadAssemblies (params (string name, string path)[] assemblies)
        {
            foreach (var (name, path) in assemblies)
            {
                try
                {
                    _assemblies[name] = !_assemblies.ContainsKey(name) ? Assembly.LoadFrom(path) : throw new Exception("This assembly alias is already used");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{ex} - {path} can't be loaded");
                }
            }
        }

        public SceneControllerParameters DownloadParams (Dictionary<Guid, object> writeCache) =>
            new(ActiveController.GetType(), (SceneParameters) CustomizableExtension.CallDownloadParams(ActiveController, writeCache));

        AbstractCustomizableParameter ICustomizable.DownloadParams (Dictionary<Guid, object> writeCache) => throw new NotImplementedException();

        public void FindAllControllers ()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(AbstractSceneController)))
                        ControllersList.Add(type);
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
#if !UNITY_EDITOR
            else
            {
                Directory.CreateDirectory(assembliesPath);
            }
#endif
        }

        public void LoadScene (int index, SceneParameters? parameters = default)
        {
            var m = GetType().GetMethod(nameof(LoadScene), new[] { typeof(SceneParameters) });
            m.MakeGenericMethod(ControllersList[index]);
            m.Invoke(this, new[] { parameters });
        }

        public void LoadScene<T> (SceneParameters? parameters = default) where T : AbstractSceneController
        {
            if (ActiveController != null)
                StopAndDestroyActiveController();
            var gameObject = new GameObject($"{typeof(T).Name}");
            ActiveController = gameObject.AddComponent<T>();
            if (parameters == null)
                ActiveController.InitEnvironment();
            else
                CustomizableExtension.CallSetUpParams(ActiveController, parameters);
        }

        public void SaveBinary (string name)
        {
            using var fs = new FileStream(name, FileMode.Create);
            var formatter = new BinaryFormatter { SurrogateSelector = MakeSurrogateSelector() };
            formatter.Serialize(fs, DownloadParams(new Dictionary<Guid, object>()));
        }

        public void SaveJson (string name)
        {
            using var sw = new StreamWriter(name);
            var jsonSerializer = new JsonSerializer() { Options = MakeYuzuCommonOptions() };
            sw.WriteLine(jsonSerializer.ToString(DownloadParams(new Dictionary<Guid, object>())));
        }

        public void SetupParams (SceneControllerParameters parameters)
        {
            var index = ControllersList.FindIndex(x => x == parameters.SceneType);
            if (index > -1)
            {
                LoadScene(index, parameters.SceneParameters);
            }
        }

        public void SetupParams (object parameters) => throw new NotImplementedException();

        public void SetupParams (AbstractCustomizableParameter parameters) => throw new NotImplementedException();

        public void StopAndDestroyActiveController ()
        {
            CacheForCustomizableObjects.ClearAll(true);

            if (ActiveController != null)
            {
                ActiveController.DestroyEnvironment();
                GameObject.DestroyImmediate(ActiveController.gameObject);
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Debug.LogWarning("No active tasks");
            }
        }
    }
}