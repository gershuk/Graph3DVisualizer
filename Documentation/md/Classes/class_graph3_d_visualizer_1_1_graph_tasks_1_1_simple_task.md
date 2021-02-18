---
title: Graph3DVisualizer::GraphTasks::SimpleTask
summary: Example of implementing a graph visual task. 

---

# Graph3DVisualizer::GraphTasks::SimpleTask



Example of implementing a graph visual task. Inherits from [Graph3DVisualizer.GraphTasks.AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md), MonoBehaviour, [Graph3DVisualizer.Customizable.ICustomizable< VisualTaskParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [Graph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md) | **[CreateGraph](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#function-creategraph)**() |
| virtual override List< [Verdict](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_verdict.md) > | **[GetResult](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#function-getresult)**() |
| virtual override void | **[InitTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#function-inittask)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| override IReadOnlyCollection< [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md) > | **[Graphs](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#property-graphs)**  |
| override IReadOnlyCollection< [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) > | **[Players](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md#property-players)**  |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.GraphTasks.AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[DestroyTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-destroytask)**() |
| [VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-setupparams)**([VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md) parameters) |

**Protected Functions inherited from [Graph3DVisualizer.GraphTasks.AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md)**

|                | Name           |
| -------------- | -------------- |
| virtual void | **[OnTaskDestoryed](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-ontaskdestoryed)**() |

**Protected Attributes inherited from [Graph3DVisualizer.GraphTasks.AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md)**

|                | Name           |
| -------------- | -------------- |
| List< [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md) > | **[_graphs](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#variable-_graphs)**  |
| List< [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) > | **[_players](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#variable-_players)**  |

**Public Functions inherited from [Graph3DVisualizer.Customizable.ICustomizable< VisualTaskParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| TParams | **[DownloadParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-setupparams)**(TParams parameters) |


## Public Functions Documentation

### function CreateGraph

```cpp
inline Graph CreateGraph()
```


### function GetResult

```cpp
virtual override List< Verdict > GetResult()
```


**Reimplements**: [Graph3DVisualizer::GraphTasks::AbstractVisualTask::GetResult](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-getresult)


### function InitTask

```cpp
inline virtual override void InitTask()
```


**Reimplements**: [Graph3DVisualizer::GraphTasks::AbstractVisualTask::InitTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md#function-inittask)


## Public Property Documentation

### property Graphs

```cpp
override IReadOnlyCollection< AbstractGraph > Graphs;
```


### property Players

```cpp
override IReadOnlyCollection< AbstractPlayer > Players;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)