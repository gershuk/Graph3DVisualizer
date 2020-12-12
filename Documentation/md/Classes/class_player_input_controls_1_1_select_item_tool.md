---
title: PlayerInputControls::SelectItemTool


---

# PlayerInputControls::SelectItemTool








Inherits from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md), [SupportComponents.ICustomizable< SelectItemToolParams >](Classes/interface_support_components_1_1_i_customizable.md), MonoBehaviour













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[SelectItem](Classes/class_player_input_controls_1_1_select_item_tool.md#function-selectitem)**()  |
| void | **[ChangeColor](Classes/class_player_input_controls_1_1_select_item_tool.md#function-changecolor)**(int deltaIndex)  |
| virtual override void | **[RegisterEvents](Classes/class_player_input_controls_1_1_select_item_tool.md#function-registerevents)**(IInputActionCollection inputActions)  |
| void | **[SetupParams](Classes/class_player_input_controls_1_1_select_item_tool.md#function-setupparams)**([SelectItemToolParams](Classes/class_player_input_controls_1_1_select_item_tool_params.md) parameters)  |
| [SelectItemToolParams](Classes/class_player_input_controls_1_1_select_item_tool_params.md) | **[DownloadParams](Classes/class_player_input_controls_1_1_select_item_tool.md#function-downloadparams)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[RayCastRange](Classes/class_player_input_controls_1_1_select_item_tool.md#property-raycastrange)**  |







## Additional inherited members













**Protected Functions inherited from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_player_input_controls_1_1_player_tool.md#function-raycast)**(float range)  |























































## Public Functions Documentation

### function SelectItem

```cpp
inline void SelectItem()
```





























### function ChangeColor

```cpp
void ChangeColor(
    int deltaIndex
)
```





























### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


























**Reimplements**: [PlayerInputControls::PlayerTool::RegisterEvents](Classes/class_player_input_controls_1_1_player_tool.md#function-registerevents)




### function SetupParams

```cpp
void SetupParams(
    SelectItemToolParams parameters
)
```





























### function DownloadParams

```cpp
SelectItemToolParams DownloadParams()
```































## Public Property Documentation

### property RayCastRange

```cpp
float RayCastRange;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)