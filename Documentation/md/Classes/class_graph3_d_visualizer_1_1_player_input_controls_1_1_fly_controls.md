---
title: Graph3DVisualizer::PlayerInputControls::FlyControls

---

# Graph3DVisualizer::PlayerInputControls::FlyControls



Inherits from IInputActionCollection, IDisposable

## Public Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[FlyModelActions](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md)**  |
| interface | **[IFlyModelActions](Classes/interface_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_i_fly_model_actions.md)**  |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[FlyControls](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-flycontrols)**() |
| bool | **[Contains](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-contains)**(InputAction action) |
| void | **[Disable](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-disable)**() |
| void | **[Dispose](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-dispose)**() |
| void | **[Enable](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-enable)**() |
| IEnumerator< InputAction > | **[GetEnumerator](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#function-getenumerator)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| [FlyModelActions](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md) | **[FlyModel](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#property-flymodel)**  |
| InputActionAsset | **[asset](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#property-asset)**  |
| InputBinding? | **[bindingMask](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#property-bindingmask)**  |
| ReadOnlyArray< InputControlScheme > | **[controlSchemes](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#property-controlschemes)**  |
| ReadOnlyArray< InputDevice >? | **[devices](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md#property-devices)**  |

## Public Functions Documentation

### function FlyControls

```cpp
inline FlyControls()
```


### function Contains

```cpp
bool Contains(
    InputAction action
)
```


### function Disable

```cpp
void Disable()
```


### function Dispose

```cpp
void Dispose()
```


### function Enable

```cpp
void Enable()
```


### function GetEnumerator

```cpp
IEnumerator< InputAction > GetEnumerator()
```


## Public Property Documentation

### property FlyModel

```cpp
FlyModelActions FlyModel;
```


### property asset

```cpp
InputActionAsset asset;
```


### property bindingMask

```cpp
InputBinding? bindingMask;
```


### property controlSchemes

```cpp
ReadOnlyArray< InputControlScheme > controlSchemes;
```


### property devices

```cpp
ReadOnlyArray< InputDevice >? devices;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)