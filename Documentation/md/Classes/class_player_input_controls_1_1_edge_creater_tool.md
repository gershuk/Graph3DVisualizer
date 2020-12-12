---
title: PlayerInputControls::EdgeCreaterTool


---

# PlayerInputControls::EdgeCreaterTool








Inherits from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md), [SupportComponents.ICustomizable< EdgeCreaterToolParams >](Classes/interface_support_components_1_1_i_customizable.md), MonoBehaviour













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[SelectFirstPoint](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-selectfirstpoint)**()  |
| void | **[SelectSecondPoint](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-selectsecondpoint)**()  |
| void | **[DeleteEdge](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-deleteedge)**()  |
| void | **[CreateEdge](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-createedge)**()  |
| virtual override void | **[RegisterEvents](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-registerevents)**(IInputActionCollection inputActions)  |
| void | **[ChangeIndex](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-changeindex)**(int deltaIndex)  |
| void | **[SetupParams](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-setupparams)**([EdgeCreaterToolParams](Classes/class_player_input_controls_1_1_edge_creater_tool_params.md) parameters)  |
| [EdgeCreaterToolParams](Classes/class_player_input_controls_1_1_edge_creater_tool_params.md) | **[DownloadParams](Classes/class_player_input_controls_1_1_edge_creater_tool.md#function-downloadparams)**()  |









## Additional inherited members













**Protected Functions inherited from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_player_input_controls_1_1_player_tool.md#function-raycast)**(float range)  |























































## Public Functions Documentation

### function SelectFirstPoint

```cpp
inline void SelectFirstPoint()
```





























### function SelectSecondPoint

```cpp
inline void SelectSecondPoint()
```





























### function DeleteEdge

```cpp
inline void DeleteEdge()
```





























### function CreateEdge

```cpp
inline void CreateEdge()
```





























### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


























**Reimplements**: [PlayerInputControls::PlayerTool::RegisterEvents](Classes/class_player_input_controls_1_1_player_tool.md#function-registerevents)




### function ChangeIndex

```cpp
void ChangeIndex(
    int deltaIndex
)
```





























### function SetupParams

```cpp
void SetupParams(
    EdgeCreaterToolParams parameters
)
```





























### function DownloadParams

```cpp
EdgeCreaterToolParams DownloadParams()
```





































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)