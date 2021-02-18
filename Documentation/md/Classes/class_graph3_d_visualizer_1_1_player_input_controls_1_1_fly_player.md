---
title: Graph3DVisualizer::PlayerInputControls::FlyPlayer
summary: Simple realization of AbstractPlayer for keyboard/mouse controls. 

---

# Graph3DVisualizer::PlayerInputControls::FlyPlayer



Simple realization of [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) for keyboard/mouse controls. Inherits from [Graph3DVisualizer.PlayerInputControls.AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md), MonoBehaviour, [Graph3DVisualizer.Customizable.ICustomizable< PlayerParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[OnMoveToPoint](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-onmovetopoint)**(InputAction.CallbackContext obj) |
| void | **[OnPlayerChangeAltitude](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-onplayerchangealtitude)**(InputAction.CallbackContext obj) |
| void | **[OnPlayerMove](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-onplayermove)**(InputAction.CallbackContext obj) |
| void | **[OnSelectItem](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-onselectitem)**(InputAction.CallbackContext obj) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[GiveNewTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#function-givenewtool)**(params [ToolConfig](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_tool_config.md)[] toolsConfig) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| override [InputType](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-inputtype) | **[InputType](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md#property-inputtype)**  |

## Additional inherited members

**Public Events inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)**

|                | Name           |
| -------------- | -------------- |
| Action< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[NewToolSelected](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#event-newtoolselected)**() |

**Public Functions inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)**

|                | Name           |
| -------------- | -------------- |
| [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-downloadparams)**() |
| void | **[SelectTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-selecttool)**(int index) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-setupparams)**([PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md) playerParams) |

**Public Properties inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)**

|                | Name           |
| -------------- | -------------- |
| int | **[CurrentToolIndex](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#property-currenttoolindex)**  |
| IReadOnlyList< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[GetToolsList](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#property-gettoolslist)**  |

**Protected Attributes inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)**

|                | Name           |
| -------------- | -------------- |
| int | **[_currentToolIndex](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_currenttoolindex)**  |
| [InputType](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-inputtype) | **[_inputType](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_inputtype)**  |
| [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md) | **[_moveComponent](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_movecomponent)**  |
| List< [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md) > | **[_playerTools](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#variable-_playertools)**  |

**Public Functions inherited from [Graph3DVisualizer.Customizable.ICustomizable< PlayerParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| TParams | **[DownloadParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-setupparams)**(TParams parameters) |


## Public Functions Documentation

### function OnMoveToPoint

```cpp
inline void OnMoveToPoint(
    InputAction.CallbackContext obj
)
```


### function OnPlayerChangeAltitude

```cpp
void OnPlayerChangeAltitude(
    InputAction.CallbackContext obj
)
```


### function OnPlayerMove

```cpp
inline void OnPlayerMove(
    InputAction.CallbackContext obj
)
```


### function OnSelectItem

```cpp
void OnSelectItem(
    InputAction.CallbackContext obj
)
```


## Protected Functions Documentation

### function GiveNewTool

```cpp
inline virtual override void GiveNewTool(
    params ToolConfig[] toolsConfig
)
```


**Reimplements**: [Graph3DVisualizer::PlayerInputControls::AbstractPlayer::GiveNewTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md#function-givenewtool)


## Public Property Documentation

### property InputType

```cpp
override InputType InputType;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)