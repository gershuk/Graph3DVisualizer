---
title: PlayerInputControls::FlyControls::FlyModelActions


---

# PlayerInputControls::FlyControls::FlyModelActions





















## Public Functions

|                | Name           |
| -------------- | -------------- |
|  | **[FlyModelActions](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-flymodelactions)**(@[FlyControls](Classes/class_player_input_controls_1_1_fly_controls.md) wrapper)  |
| InputActionMap | **[Get](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-get)**()  |
| void | **[Enable](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-enable)**()  |
| void | **[Disable](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-disable)**()  |
| void | **[SetCallbacks](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-setcallbacks)**([IFlyModelActions](Classes/interface_player_input_controls_1_1_fly_controls_1_1_i_fly_model_actions.md) instance)  |
| implicit | **[operator InputActionMap](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-operator-inputactionmap)**([FlyModelActions](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md) set)  |




## Public Attributes

|                | Name           |
| -------------- | -------------- |
| InputAction | **[MoveToPoint](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-movetopoint)**  |
| InputAction | **[Move](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-move)**  |
| InputAction | **[LookRotation](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-lookrotation)**  |
| InputAction | **[ChangeAltitude](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-changealtitude)**  |
| InputAction | **[ScrollItemList](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-scrollitemlist)**  |
| InputAction | **[SelectItem](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-selectitem)**  |
| bool | **[enabled](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#variable-enabled)**  |

















## Public Functions Documentation

### function FlyModelActions

```cpp
inline FlyModelActions(
    @FlyControls wrapper
)
```





























### function Get

```cpp
inline InputActionMap Get()
```





























### function Enable

```cpp
inline void Enable()
```





























### function Disable

```cpp
inline void Disable()
```





























### function SetCallbacks

```cpp
inline void SetCallbacks(
    IFlyModelActions instance
)
```





























### function operator InputActionMap

```cpp
static inline implicit operator InputActionMap(
    FlyModelActions set
)
```

































## Public Attributes Documentation

### variable MoveToPoint

```cpp
InputAction MoveToPoint => m_Wrapper.m_FlyModel_MoveToPoint;
```





























### variable Move

```cpp
InputAction Move => m_Wrapper.m_FlyModel_Move;
```





























### variable LookRotation

```cpp
InputAction LookRotation => m_Wrapper.m_FlyModel_LookRotation;
```





























### variable ChangeAltitude

```cpp
InputAction ChangeAltitude => m_Wrapper.m_FlyModel_ChangeAltitude;
```





























### variable ScrollItemList

```cpp
InputAction ScrollItemList => m_Wrapper.m_FlyModel_ScrollItemList;
```





























### variable SelectItem

```cpp
InputAction SelectItem => m_Wrapper.m_FlyModel_SelectItem;
```





























### variable enabled

```cpp
bool enabled => [Get](Classes/struct_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-get)().enabled;
```

































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)