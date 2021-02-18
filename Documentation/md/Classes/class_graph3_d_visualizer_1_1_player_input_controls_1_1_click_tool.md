---
title: Graph3DVisualizer::PlayerInputControls::ClickTool
summary: Tool for working with 3d menu components. 

---

# Graph3DVisualizer::PlayerInputControls::ClickTool



Tool for working with 3d menu components. Inherits from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md), [Graph3DVisualizer.Customizable.ICustomizable< ClickToolParams >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[Click](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#function-click)**() |
| [ClickToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool_params.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#function-downloadparams)**() |
| virtual override void | **[RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#function-registerevents)**(IInputActionCollection inputActions) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#function-setupparams)**([ClickToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool_params.md) parameters) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[RayCastRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#property-raycastrange)**  |

## Additional inherited members

**Protected Functions inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-raycast)**(float range) |


## Public Functions Documentation

### function Click

```cpp
inline void Click()
```


### function DownloadParams

```cpp
ClickToolParams DownloadParams()
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
inline void SetupParams(
    ClickToolParams parameters
)
```


## Public Property Documentation

### property RayCastRange

```cpp
float RayCastRange;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)