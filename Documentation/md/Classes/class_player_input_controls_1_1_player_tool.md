---
title: PlayerInputControls::PlayerTool


---

# PlayerInputControls::PlayerTool








Inherits from MonoBehaviour

Inherited by [PlayerInputControls.EdgeCreaterTool](Classes/class_player_input_controls_1_1_edge_creater_tool.md), [PlayerInputControls.GrabItemTool](Classes/class_player_input_controls_1_1_grab_item_tool.md), [PlayerInputControls.SelectItemTool](Classes/class_player_input_controls_1_1_select_item_tool.md)












## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract void | **[RegisterEvents](Classes/class_player_input_controls_1_1_player_tool.md#function-registerevents)**(IInputActionCollection inputActions) =0  |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_player_input_controls_1_1_player_tool.md#function-raycast)**(float range)  |




















## Public Functions Documentation

### function RegisterEvents

```cpp
virtual abstract void RegisterEvents(
    IInputActionCollection inputActions
) =0
```


























**Reimplemented by**: [PlayerInputControls::EdgeCreaterTool::RegisterEvents](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-registerevents), [PlayerInputControls::GrabItemTool::RegisterEvents](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-registerevents), [PlayerInputControls::SelectItemTool::RegisterEvents](Classes/class_player_input_controls_1_1_select_item_tool.md#function-registerevents)





## Protected Functions Documentation

### function RayCast

```cpp
inline RaycastHit RayCast(
    float range
)
```




































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)