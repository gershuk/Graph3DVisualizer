---
title: PlayerInputControls::GrabItemTool


---

# PlayerInputControls::GrabItemTool








Inherits from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md), MonoBehaviour













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[ChangeRange](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-changerange)**(float normalizedDelta)  |
| void | **[GrabItem](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-grabitem)**()  |
| void | **[FreeItem](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-freeitem)**()  |
| void | **[StartChangingRange](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-startchangingrange)**()  |
| void | **[StopChangingRange](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-stopchangingrange)**()  |
| virtual override void | **[RegisterEvents](Classes/class_player_input_controls_1_1_grab_item_tool.md#function-registerevents)**(IInputActionCollection inputActions)  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[CapturedRange](Classes/class_player_input_controls_1_1_grab_item_tool.md#property-capturedrange)**  |
| float | **[RangeChangeSpeed](Classes/class_player_input_controls_1_1_grab_item_tool.md#property-rangechangespeed)**  |







## Additional inherited members













**Protected Functions inherited from [PlayerInputControls.PlayerTool](Classes/class_player_input_controls_1_1_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_player_input_controls_1_1_player_tool.md#function-raycast)**(float range)  |





































## Public Functions Documentation

### function ChangeRange

```cpp
void ChangeRange(
    float normalizedDelta
)
```





























### function GrabItem

```cpp
inline void GrabItem()
```





























### function FreeItem

```cpp
inline void FreeItem()
```





























### function StartChangingRange

```cpp
inline void StartChangingRange()
```





























### function StopChangingRange

```cpp
inline void StopChangingRange()
```





























### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


























**Reimplements**: [PlayerInputControls::PlayerTool::RegisterEvents](Classes/class_player_input_controls_1_1_player_tool.md#function-registerevents)






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

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)