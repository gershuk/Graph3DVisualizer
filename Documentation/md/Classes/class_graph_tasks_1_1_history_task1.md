---
title: GraphTasks::HistoryTask1


---

# GraphTasks::HistoryTask1








Inherits from [GraphTasks.VisualTask](Classes/class_graph_tasks_1_1_visual_task.md), MonoBehaviour













## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override [Graph](Classes/class_grpah3_d_visualizer_1_1_graph.md) | **[CreateGraph](Classes/class_graph_tasks_1_1_history_task1.md#function-creategraph)**()  |
| virtual override void | **[InitTask](Classes/class_graph_tasks_1_1_history_task1.md#function-inittask)**()  |
| virtual override void | **[StartTask](Classes/class_graph_tasks_1_1_history_task1.md#function-starttask)**()  |
| virtual override void | **[StopTask](Classes/class_graph_tasks_1_1_history_task1.md#function-stoptask)**()  |
| virtual override void | **[DestroyTask](Classes/class_graph_tasks_1_1_history_task1.md#function-destroytask)**()  |
| virtual override List< [Verdict](Classes/class_graph_tasks_1_1_verdict.md) > | **[GetResult](Classes/class_graph_tasks_1_1_history_task1.md#function-getresult)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| override IReadOnlyCollection< [AbstractPLayer](Classes/class_player_input_controls_1_1_abstract_p_layer.md) > | **[Players](Classes/class_graph_tasks_1_1_history_task1.md#property-players)**  |
| override IReadOnlyCollection< [Graph](Classes/class_grpah3_d_visualizer_1_1_graph.md) > | **[Graphs](Classes/class_graph_tasks_1_1_history_task1.md#property-graphs)**  |



















## Public Functions Documentation

### function CreateGraph

```cpp
inline virtual override Graph CreateGraph()
```


























**Reimplements**: [GraphTasks::VisualTask::CreateGraph](Classes/class_graph_tasks_1_1_visual_task.md#function-creategraph)




### function InitTask

```cpp
inline virtual override void InitTask()
```


























**Reimplements**: [GraphTasks::VisualTask::InitTask](Classes/class_graph_tasks_1_1_visual_task.md#function-inittask)




### function StartTask

```cpp
virtual override void StartTask()
```


























**Reimplements**: [GraphTasks::VisualTask::StartTask](Classes/class_graph_tasks_1_1_visual_task.md#function-starttask)




### function StopTask

```cpp
virtual override void StopTask()
```


























**Reimplements**: [GraphTasks::VisualTask::StopTask](Classes/class_graph_tasks_1_1_visual_task.md#function-stoptask)




### function DestroyTask

```cpp
inline virtual override void DestroyTask()
```


























**Reimplements**: [GraphTasks::VisualTask::DestroyTask](Classes/class_graph_tasks_1_1_visual_task.md#function-destroytask)




### function GetResult

```cpp
inline virtual override List< Verdict > GetResult()
```


























**Reimplements**: [GraphTasks::VisualTask::GetResult](Classes/class_graph_tasks_1_1_visual_task.md#function-getresult)






## Public Property Documentation

### property Players

```cpp
override IReadOnlyCollection< AbstractPLayer > Players;
```





























### property Graphs

```cpp
override IReadOnlyCollection< Graph > Graphs;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)