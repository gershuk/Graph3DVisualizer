---
title: Graph3DVisualizer::GraphTasks::GraphInfo
summary: Support class for graph serialization. 

---

# Graph3DVisualizer::GraphTasks::GraphInfo



Support class for graph serialization. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[GraphInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-graphinfo)**(Type graphType, [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) graphParameters) |
| void | **[Deconstruct](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-deconstruct)**(out Type graphType, out [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) graphParameters) |
| override bool | **[Equals](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-equals)**(object obj) |
| override int | **[GetHashCode](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-gethashcode)**() |
| implicit | **[operator](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-operator)**(Type graphType, [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) graphParameters) |
| implicit | **[operator GraphInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#function-operator-graphinfo)**((Type graphType, [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) graphParameters) value) |

## Public Attributes

|                | Name           |
| -------------- | -------------- |
| [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) | **[graphParameters](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#variable-graphparameters)**  |
| Type | **[graphType](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md#variable-graphtype)**  |

## Public Functions Documentation

### function GraphInfo

```cpp
inline GraphInfo(
    Type graphType,
    GraphParameters graphParameters
)
```


### function Deconstruct

```cpp
inline void Deconstruct(
    out Type graphType,
    out GraphParameters graphParameters
)
```


### function Equals

```cpp
inline override bool Equals(
    object obj
)
```


### function GetHashCode

```cpp
inline override int GetHashCode()
```


### function operator

```cpp
static inline implicit operator(
    Type graphType,
    GraphParameters graphParameters
)
```


### function operator GraphInfo

```cpp
static inline implicit operator GraphInfo(
    (Type graphType, GraphParameters graphParameters) value
)
```


## Public Attributes Documentation

### variable graphParameters

```cpp
GraphParameters graphParameters;
```


### variable graphType

```cpp
Type graphType;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)