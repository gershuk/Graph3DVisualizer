---
title: Graph3DVisualizer::GraphTasks::PlayerInfo
summary: Support class for player serialization. 

---

# Graph3DVisualizer::GraphTasks::PlayerInfo



Support class for player serialization. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[PlayerInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-playerinfo)**(Type playerType, [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParameters) |
| void | **[Deconstruct](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-deconstruct)**(out Type playerType, out [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParameters) |
| override bool | **[Equals](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-equals)**(object obj) |
| override int | **[GetHashCode](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-gethashcode)**() |
| implicit | **[operator](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-operator)**(Type playerType, [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParameters) |
| implicit | **[operator PlayerInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#function-operator-playerinfo)**((Type playerType, [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParameters) value) |

## Public Attributes

|                | Name           |
| -------------- | -------------- |
| [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) | **[playerParameters](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#variable-playerparameters)**  |
| Type | **[playerType](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md#variable-playertype)**  |

## Public Functions Documentation

### function PlayerInfo

```cpp
inline PlayerInfo(
    Type playerType,
    PlayerParameters playerParameters
)
```


### function Deconstruct

```cpp
inline void Deconstruct(
    out Type playerType,
    out PlayerParameters playerParameters
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
    Type playerType,
    PlayerParameters playerParameters
)
```


### function operator PlayerInfo

```cpp
static inline implicit operator PlayerInfo(
    (Type playerType, PlayerParameters playerParameters) value
)
```


## Public Attributes Documentation

### variable playerParameters

```cpp
PlayerParameters playerParameters;
```


### variable playerType

```cpp
Type playerType;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)