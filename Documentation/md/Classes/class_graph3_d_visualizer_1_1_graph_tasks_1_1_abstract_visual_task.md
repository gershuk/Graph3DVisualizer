---
title: Graph3DVisualizer::GraphTasks::AbstractVisualTask
summary: Class that describes a task for working with a graph in 3d 

---

# Graph3DVisualizer::GraphTasks::AbstractVisualTask



Class that describes a task for working with a graph in 3d Inherits from MonoBehaviour, [Graph3DVisualizer.Customizable.ICustomizable< VisualTaskParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

Inherited by [Graph3DVisualizer.GraphTasks.HistoryTask1](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_history_task1.md), [Graph3DVisualizer.GraphTasks.SimpleTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[DestroyTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-destroytask)**() |
| [VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-downloadparams)**() |
| virtual abstract List< [Verdict](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_verdict.md) > | **[GetResult](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-getresult)**() =0 |
| virtual abstract void | **[InitTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-inittask)**() =0 |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-setupparams)**([VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md) parameters) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual void | **[OnTaskDestoryed](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-ontaskdestoryed)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| abstract IReadOnlyCollection< [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md) > | **[Graphs](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#property-graphs)**  |
| abstract IReadOnlyCollection< [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) > | **[Players](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#property-players)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| List< [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md) > | **[_graphs](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#variable-_graphs)**  |
| List< [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) > | **[_players](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#variable-_players)**  |

## Public Functions Documentation

### function DestroyTask

```cpp
inline void DestroyTask()
```


### function DownloadParams

```cpp
VisualTaskParameters DownloadParams()
```


### function GetResult

```cpp
virtual abstract List< Verdict > GetResult() =0
```


**Reimplemented by**: [Graph3DVisualizer::GraphTasks::HistoryTask1::GetResult](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_history_task1.md#function-getresult), [Graph3DVisualizer::GraphTasks::SimpleTask::GetResult](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#function-getresult)


### function InitTask

```cpp
virtual abstract void InitTask() =0
```


**Reimplemented by**: [Graph3DVisualizer::GraphTasks::HistoryTask1::InitTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_history_task1.md#function-inittask), [Graph3DVisualizer::GraphTasks::SimpleTask::InitTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#function-inittask)


### function SetupParams

```cpp
inline void SetupParams(
    VisualTaskParameters parameters
)
```


## Protected Functions Documentation

### function OnTaskDestoryed

```cpp
inline virtual void OnTaskDestoryed()
```


## Public Property Documentation

### property Graphs

```cpp
abstract IReadOnlyCollection< AbstractGraph > Graphs;
```


### property Players

```cpp
abstract IReadOnlyCollection< AbstractPlayer > Players;
```


## Protected Attributes Documentation

### variable _graphs

```cpp
List< AbstractGraph > _graphs = new List<[AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md)>();
```


### variable _players

```cpp
List< AbstractPlayer > _players = new List<[AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)>();
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)