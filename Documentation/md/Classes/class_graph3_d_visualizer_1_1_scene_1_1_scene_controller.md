---
title: Graph3DVisualizer::Scene::SceneController
summary: Component that manages the loading/saving of AbstractVisualTask. 

---

# Graph3DVisualizer::Scene::SceneController



Component that manages the loading/saving of AbstractVisualTask. Inherits from MonoBehaviour, [Graph3DVisualizer.Customizable.ICustomizable< SceneControllerParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [SceneControllerParameters](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-downloadparams)**() |
| void | **[FindAllTasks](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-findalltasks)**() |
| void | **[Load](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-load)**() |
| void | **[LoadBinary](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-loadbinary)**(string path) |
| void | **[LoadMods](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-loadmods)**() |
| void | **[SaveBinary](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-savebinary)**(string path) |
| void | **[SaveJson](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-savejson)**(string path) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-setupparams)**([SceneControllerParameters](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller_parameters.md) parameters) |
| void | **[StartTask](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-starttask)**(int taskIndex, [VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md) visualTaskParameters =null) |
| void | **[StopTask](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#function-stoptask)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| [AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md) | **[ActiveTask](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#property-activetask)**  |
| List< Type > | **[TaskList](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md#property-tasklist)**  |

## Public Functions Documentation

### function DownloadParams

```cpp
SceneControllerParameters DownloadParams()
```


### function FindAllTasks

```cpp
inline void FindAllTasks()
```


### function Load

```cpp
inline void Load()
```


### function LoadBinary

```cpp
inline void LoadBinary(
    string path
)
```


### function LoadMods

```cpp
inline void LoadMods()
```


### function SaveBinary

```cpp
inline void SaveBinary(
    string path
)
```


### function SaveJson

```cpp
inline void SaveJson(
    string path
)
```


### function SetupParams

```cpp
inline void SetupParams(
    SceneControllerParameters parameters
)
```


### function StartTask

```cpp
inline void StartTask(
    int taskIndex,
    VisualTaskParameters visualTaskParameters =null
)
```


### function StopTask

```cpp
inline void StopTask()
```


## Public Property Documentation

### property ActiveTask

```cpp
AbstractVisualTask ActiveTask;
```


### property TaskList

```cpp
List< Type > TaskList;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)