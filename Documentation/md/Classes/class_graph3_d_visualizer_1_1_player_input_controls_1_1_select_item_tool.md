---
title: Graph3DVisualizer::PlayerInputControls::SelectItemTool
summary: The tool allows you to select objects with components that implement the ISelectable. 

---

# Graph3DVisualizer::PlayerInputControls::SelectItemTool



The tool allows you to select objects with components that implement the ISelectable. Inherits from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md), [Graph3DVisualizer.Customizable.ICustomizable< SelectItemToolParams >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[ChangeColor](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-changecolor)**(int deltaIndex) |
| [SelectItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool_params.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-downloadparams)**() |
| virtual override void | **[RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-registerevents)**(IInputActionCollection inputActions) |
| void | **[SelectItem](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-selectitem)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-setupparams)**([SelectItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool_params.md) parameters) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[RayCastRange](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#property-raycastrange)**  |

## Additional inherited members

**Protected Functions inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-raycast)**(float range) |


## Public Functions Documentation

### function ChangeColor

```cpp
void ChangeColor(
    int deltaIndex
)
```


### function DownloadParams

```cpp
SelectItemToolParams DownloadParams()
```


### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


**Reimplements**: [Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-registerevents)


### function SelectItem

```cpp
inline void SelectItem()
```


### function SetupParams

```cpp
void SetupParams(
    SelectItemToolParams parameters
)
```


## Public Property Documentation

### property RayCastRange

```cpp
float RayCastRange;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)