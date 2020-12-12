---
title: GraphTasks::VisualTask


---

# GraphTasks::VisualTask








Inherits from MonoBehaviour

Inherited by [GraphTasks.HistoryTask1](Classes/class_graph_tasks_1_1_history_task1.md), [GraphTasks.SimpleTask](Classes/class_graph_tasks_1_1_simple_task.md)












## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract [Graph](Classes/class_grpah3_d_visualizer_1_1_graph.md) | **[CreateGraph](Classes/class_graph_tasks_1_1_visual_task.md#function-creategraph)**() =0  |
| virtual abstract void | **[InitTask](Classes/class_graph_tasks_1_1_visual_task.md#function-inittask)**() =0  |
| virtual abstract void | **[StartTask](Classes/class_graph_tasks_1_1_visual_task.md#function-starttask)**() =0  |
| virtual abstract void | **[StopTask](Classes/class_graph_tasks_1_1_visual_task.md#function-stoptask)**() =0  |
| virtual abstract void | **[DestroyTask](Classes/class_graph_tasks_1_1_visual_task.md#function-destroytask)**() =0  |
| virtual abstract List< [Verdict](Classes/class_graph_tasks_1_1_verdict.md) > | **[GetResult](Classes/class_graph_tasks_1_1_visual_task.md#function-getresult)**() =0  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| abstract IReadOnlyCollection< [AbstractPLayer](Classes/class_player_input_controls_1_1_abstract_p_layer.md) > | **[Players](Classes/class_graph_tasks_1_1_visual_task.md#property-players)**  |
| abstract IReadOnlyCollection< [Graph](Classes/class_grpah3_d_visualizer_1_1_graph.md) > | **[Graphs](Classes/class_graph_tasks_1_1_visual_task.md#property-graphs)**  |



















## Public Functions Documentation

### function CreateGraph

```cpp
virtual abstract Graph CreateGraph() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::CreateGraph](Classes/class_graph_tasks_1_1_history_task1.md#function-creategraph), [GraphTasks::SimpleTask::CreateGraph](Classes/class_graph_tasks_1_1_simple_task.md#function-creategraph)




### function InitTask

```cpp
virtual abstract void InitTask() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::InitTask](Classes/class_graph_tasks_1_1_history_task1.md#function-inittask), [GraphTasks::SimpleTask::InitTask](Classes/class_graph_tasks_1_1_simple_task.md#function-inittask)




### function StartTask

```cpp
virtual abstract void StartTask() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::StartTask](Classes/class_graph_tasks_1_1_history_task1.md#function-starttask), [GraphTasks::SimpleTask::StartTask](Classes/class_graph_tasks_1_1_simple_task.md#function-starttask)




### function StopTask

```cpp
virtual abstract void StopTask() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::StopTask](Classes/class_graph_tasks_1_1_history_task1.md#function-stoptask), [GraphTasks::SimpleTask::StopTask](Classes/class_graph_tasks_1_1_simple_task.md#function-stoptask)




### function DestroyTask

```cpp
virtual abstract void DestroyTask() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::DestroyTask](Classes/class_graph_tasks_1_1_history_task1.md#function-destroytask), [GraphTasks::SimpleTask::DestroyTask](Classes/class_graph_tasks_1_1_simple_task.md#function-destroytask)




### function GetResult

```cpp
virtual abstract List< Verdict > GetResult() =0
```


























**Reimplemented by**: [GraphTasks::HistoryTask1::GetResult](Classes/class_graph_tasks_1_1_history_task1.md#function-getresult), [GraphTasks::SimpleTask::GetResult](Classes/class_graph_tasks_1_1_simple_task.md#function-getresult)






## Public Property Documentation

### property Players

```cpp
abstract IReadOnlyCollection< AbstractPLayer > Players;
```





























### property Graphs

```cpp
abstract IReadOnlyCollection< Graph > Graphs;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)