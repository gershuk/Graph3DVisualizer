---
title: Graph3DVisualizer::PlayerInputControls

---

# Graph3DVisualizer::PlayerInputControls

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)** <br>Abstract class that describes player.  |
| class | **[Graph3DVisualizer::PlayerInputControls::PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md)** <br>A class that describes default player parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::PlayerInputControls::FlyControls](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md)**  |
| class | **[Graph3DVisualizer::PlayerInputControls::FlyPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md)** <br>Simple realization of [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) for keyboard/mouse controls.  |
| struct | **[Graph3DVisualizer::PlayerInputControls::ToolConfig](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_tool_config.md)** <br>Ð¡lass that describes the data needed when creating a new player tool. AbstractPlayer.GiveNewTool(ToolConfig[]) |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)** <br>Abstract class that describes the player's tool for interacting with the world.  |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_tool_params.md)** <br>A class that describes default player's tool parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::PlayerInputControls::ClickTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md)** <br>Tool for working with 3d menu components.  |
| class | **[Graph3DVisualizer::PlayerInputControls::ClickToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool_params.md)** <br>Class that describes [ClickTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md) parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::PlayerInputControls::EdgeCreaterTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md)** <br>Tool for creating links between vertexes.  |
| class | **[Graph3DVisualizer::PlayerInputControls::EdgeCreaterToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool_params.md)** <br>Class that describes [EdgeCreaterTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md) parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::PlayerInputControls::GrabItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md)** <br>Tool for dragging objects with MovementComponent.  |
| class | **[Graph3DVisualizer::PlayerInputControls::GrabItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool_params.md)** <br>Class that describes [GrabItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md) parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::PlayerInputControls::LaserPointer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_laser_pointer.md)** <br>A component that simulates a laser pointer. Simplifies the use of tools in VR.  |
| class | **[Graph3DVisualizer::PlayerInputControls::SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md)** <br>The tool allows you to select objects with components that implement the ISelectable.  |
| class | **[Graph3DVisualizer::PlayerInputControls::SelectItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool_params.md)** <br>Class that describes [SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md) parameters for ICustomizable<TParams>.  |

## Types

|                | Name           |
| -------------- | -------------- |
| enum| **[InputType](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-inputtype)** { Off = 0, MenuOnly = 1, ToolsOnly = 2, All = 3}<br>Player input type.  |
| enum| **[LaserState](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md#enum-laserstate)** { On = 0, Off = 1} |

## Types Documentation

### enum InputType

| Enumerator | Value | Description |
| ---------- | ----- | ----------- |
| Off | 0|   |
| MenuOnly | 1|   |
| ToolsOnly | 2|   |
| All | 3|   |



Player input type. 

### enum LaserState

| Enumerator | Value | Description |
| ---------- | ----- | ----------- |
| On | 0|   |
| Off | 1|   |









-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (çèìà)