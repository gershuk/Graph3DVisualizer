---
title: Graph3DVisualizer::PlayerInputControls::AbstractPlayer
summary: Abstract class that describes player. 

---

# Graph3DVisualizer::PlayerInputControls::AbstractPlayer



Abstract class that describes player. Inherits from MonoBehaviour, [Graph3DVisualizer.Customizable.ICustomizable< PlayerParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

Inherited by [Graph3DVisualizer.PlayerInputControls.FlyPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[NewToolSelected](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#event-newtoolselected)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-downloadparams)**() |
| void | **[SelectTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-selecttool)**(int index) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-setupparams)**([PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParams) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract void | **[GiveNewTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-givenewtool)**(params [ToolConfig](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_tool_config.md)[] toolsConfig) =0 |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| int | **[CurrentToolIndex](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#property-currenttoolindex)**  |
| IReadOnlyList< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[GetToolsList](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#property-gettoolslist)**  |
| abstract [InputType](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-inputtype) | **[InputType](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#property-inputtype)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| int | **[_currentToolIndex](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_currenttoolindex)**  |
| [InputType](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-inputtype) | **[_inputType](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_inputtype)**  |
| [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md) | **[_moveComponent](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_movecomponent)**  |
| List< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[_playerTools](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_playertools)**  |

## Public Events Documentation

### event NewToolSelected

```cpp
Action< AbstractPlayerTool > NewToolSelected()
```


## Public Functions Documentation

### function DownloadParams

```cpp
PlayerParameters DownloadParams()
```


### function SelectTool

```cpp
inline void SelectTool(
    int index
)
```


### function SetupParams

```cpp
inline void SetupParams(
    PlayerParameters playerParams
)
```


## Protected Functions Documentation

### function GiveNewTool

```cpp
virtual abstract void GiveNewTool(
    params ToolConfig[] toolsConfig
) =0
```


**Reimplemented by**: [Graph3DVisualizer::PlayerInputControls::FlyPlayer::GiveNewTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-givenewtool)


## Public Property Documentation

### property CurrentToolIndex

```cpp
int CurrentToolIndex;
```


### property GetToolsList

```cpp
IReadOnlyList< AbstractPlayerTool > GetToolsList;
```


### property InputType

```cpp
abstract InputType InputType;
```


## Protected Attributes Documentation

### variable _currentToolIndex

```cpp
int _currentToolIndex = 0;
```


### variable _inputType

```cpp
InputType _inputType;
```


### variable _moveComponent

```cpp
MovementComponent _moveComponent;
```


### variable _playerTools

```cpp
List< AbstractPlayerTool > _playerTools = new List<[AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)>();
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)