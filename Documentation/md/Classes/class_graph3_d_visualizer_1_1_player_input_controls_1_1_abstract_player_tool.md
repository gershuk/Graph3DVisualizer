---
title: Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool
summary: Abstract class that describes the player's tool for interacting with the world. 

---

# Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool



Abstract class that describes the player's tool for interacting with the world. Inherits from MonoBehaviour

Inherited by [Graph3DVisualizer.PlayerInputControls.ClickTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md), [Graph3DVisualizer.PlayerInputControls.EdgeCreaterTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md), [Graph3DVisualizer.PlayerInputControls.GrabItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md), [Graph3DVisualizer.PlayerInputControls.SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract void | **[RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-registerevents)**(IInputActionCollection inputActions) =0 |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-raycast)**(float range) |

## Public Functions Documentation

### function RegisterEvents

```cpp
virtual abstract void RegisterEvents(
    IInputActionCollection inputActions
) =0
```


**Reimplemented by**: [Graph3DVisualizer::PlayerInputControls::ClickTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md#function-registerevents), [Graph3DVisualizer::PlayerInputControls::EdgeCreaterTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-registerevents), [Graph3DVisualizer::PlayerInputControls::GrabItemTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md#function-registerevents), [Graph3DVisualizer::PlayerInputControls::SelectItemTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md#function-registerevents)


## Protected Functions Documentation

### function RayCast

```cpp
inline RaycastHit RayCast(
    float range
)
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)