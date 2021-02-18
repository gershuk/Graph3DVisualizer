---
title: Graph3DVisualizer::PlayerInputControls::GrabItemTool
summary: Tool for dragging objects with MovementComponent. 

---

# Graph3DVisualizer::PlayerInputControls::GrabItemTool



Tool for dragging objects with MovementComponent. Inherits from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md), [Graph3DVisualizer.Customizable.ICustomizable< GrabItemToolParams >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[ChangeRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-changerange)**(float normalizedDelta) |
| [GrabItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool_params.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-downloadparams)**() |
| void | **[FreeItem](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-freeitem)**() |
| void | **[GrabItem](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-grabitem)**() |
| virtual override void | **[RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-registerevents)**(IInputActionCollection inputActions) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-setupparams)**([GrabItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool_params.md) parameters) |
| void | **[StartChangingRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-startchangingrange)**() |
| void | **[StopChangingRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-stopchangingrange)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[CapturedRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#property-capturedrange)**  |
| float | **[RangeChangeSpeed](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#property-rangechangespeed)**  |

## Additional inherited members

**Protected Functions inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-raycast)**(float range) |


## Public Functions Documentation

### function ChangeRange

```cpp
void ChangeRange(
    float normalizedDelta
)
```


### function DownloadParams

```cpp
inline GrabItemToolParams DownloadParams()
```


### function FreeItem

```cpp
inline void FreeItem()
```


### function GrabItem

```cpp
inline void GrabItem()
```


### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


**Reimplements**: [Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-registerevents)


### function SetupParams

```cpp
void SetupParams(
    GrabItemToolParams parameters
)
```


### function StartChangingRange

```cpp
inline void StartChangingRange()
```


### function StopChangingRange

```cpp
inline void StopChangingRange()
```


## Public Property Documentation

### property CapturedRange

```cpp
float CapturedRange;
```


### property RangeChangeSpeed

```cpp
float RangeChangeSpeed;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)