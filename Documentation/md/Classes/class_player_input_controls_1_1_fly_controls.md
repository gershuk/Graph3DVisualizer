---
title: PlayerInputControls::FlyControls


---

# PlayerInputControls::FlyControls








Inherits from IInputActionCollection, IDisposable



## Public Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[FlyModelActions](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md)**  |
| interface | **[IFlyModelActions](Classes/interface_player_input_controls_1_1_fly_controls_1_1_i_fly_model_actions.md)**  |










## Public Functions

|                | Name           |
| -------------- | -------------- |
|  | **[FlyControls](Classes/class_player_input_controls_1_1_fly_controls.md#function-flycontrols)**()  |
| void | **[Dispose](Classes/class_player_input_controls_1_1_fly_controls.md#function-dispose)**()  |
| bool | **[Contains](Classes/class_player_input_controls_1_1_fly_controls.md#function-contains)**(InputAction action)  |
| IEnumerator< InputAction > | **[GetEnumerator](Classes/class_player_input_controls_1_1_fly_controls.md#function-getenumerator)**()  |
| void | **[Enable](Classes/class_player_input_controls_1_1_fly_controls.md#function-enable)**()  |
| void | **[Disable](Classes/class_player_input_controls_1_1_fly_controls.md#function-disable)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| InputActionAsset | **[asset](Classes/class_player_input_controls_1_1_fly_controls.md#property-asset)**  |
| InputBinding? | **[bindingMask](Classes/class_player_input_controls_1_1_fly_controls.md#property-bindingmask)**  |
| ReadOnlyArray< InputDevice >? | **[devices](Classes/class_player_input_controls_1_1_fly_controls.md#property-devices)**  |


## Public Attributes

|                | Name           |
| -------------- | -------------- |
| ReadOnlyArray< InputControlScheme > | **[controlSchemes](Classes/class_player_input_controls_1_1_fly_controls.md#variable-controlschemes)**  |
| [FlyModelActions](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md) | **[FlyModel](Classes/class_player_input_controls_1_1_fly_controls.md#variable-flymodel)**  |

















## Public Functions Documentation

### function FlyControls

```cpp
inline FlyControls()
```





























### function Dispose

```cpp
inline void Dispose()
```





























### function Contains

```cpp
inline bool Contains(
    InputAction action
)
```





























### function GetEnumerator

```cpp
inline IEnumerator< InputAction > GetEnumerator()
```





























### function Enable

```cpp
inline void Enable()
```





























### function Disable

```cpp
inline void Disable()
```































## Public Property Documentation

### property asset

```cpp
InputActionAsset asset;
```





























### property bindingMask

```cpp
InputBinding? bindingMask;
```





























### property devices

```cpp
ReadOnlyArray< InputDevice >? devices;
```































## Public Attributes Documentation

### variable controlSchemes

```cpp
ReadOnlyArray< InputControlScheme > controlSchemes => asset.controlSchemes;
```





























### variable FlyModel

```cpp
FlyModelActions FlyModel => new [FlyModelActions](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md)(this);
```

































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)