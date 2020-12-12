---
title: PlayerInputControls::FlyPlayer


---

# PlayerInputControls::FlyPlayer








Inherits from [PlayerInputControls.AbstractPLayer](Classes/class_player_input_controls_1_1_abstract_p_layer.md), [SupportComponents.ICustomizable< PlayerParams >](Classes/interface_support_components_1_1_i_customizable.md), MonoBehaviour













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[OnPlayerMove](Classes/class_player_input_controls_1_1_fly_player.md#function-onplayermove)**(InputAction.CallbackContext obj)  |
| void | **[OnPlayerChangeAltitude](Classes/class_player_input_controls_1_1_fly_player.md#function-onplayerchangealtitude)**(InputAction.CallbackContext obj)  |
| void | **[OnMoveToPoint](Classes/class_player_input_controls_1_1_fly_player.md#function-onmovetopoint)**(InputAction.CallbackContext obj)  |
| void | **[SelectTool](Classes/class_player_input_controls_1_1_fly_player.md#function-selecttool)**(int index)  |
| void | **[OnSelectItem](Classes/class_player_input_controls_1_1_fly_player.md#function-onselectitem)**(InputAction.CallbackContext obj)  |
| void | **[SetupParams](Classes/class_player_input_controls_1_1_fly_player.md#function-setupparams)**([PlayerParams](Classes/class_player_input_controls_1_1_player_params.md) playerParams)  |
| [PlayerParams](Classes/class_player_input_controls_1_1_player_params.md) | **[DownloadParams](Classes/class_player_input_controls_1_1_fly_player.md#function-downloadparams)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| override [InputType](Namespaces/namespace_player_input_controls.md#enum-inputtype) | **[InputType](Classes/class_player_input_controls_1_1_fly_player.md#property-inputtype)**  |







## Additional inherited members

















**Protected Attributes inherited from [PlayerInputControls.AbstractPLayer](Classes/class_player_input_controls_1_1_abstract_p_layer.md)**

|                | Name           |
| -------------- | -------------- |
| [InputType](Namespaces/namespace_player_input_controls.md#enum-inputtype) | **[_inputType](Classes/class_player_input_controls_1_1_abstract_p_layer.md#variable-_inputtype)**  |



















































## Public Functions Documentation

### function OnPlayerMove

```cpp
inline void OnPlayerMove(
    InputAction.CallbackContext obj
)
```





























### function OnPlayerChangeAltitude

```cpp
void OnPlayerChangeAltitude(
    InputAction.CallbackContext obj
)
```





























### function OnMoveToPoint

```cpp
inline void OnMoveToPoint(
    InputAction.CallbackContext obj
)
```





























### function SelectTool

```cpp
inline void SelectTool(
    int index
)
```





























### function OnSelectItem

```cpp
void OnSelectItem(
    InputAction.CallbackContext obj
)
```





























### function SetupParams

```cpp
inline void SetupParams(
    PlayerParams playerParams
)
```





























### function DownloadParams

```cpp
PlayerParams DownloadParams()
```































## Public Property Documentation

### property InputType

```cpp
override InputType InputType;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)