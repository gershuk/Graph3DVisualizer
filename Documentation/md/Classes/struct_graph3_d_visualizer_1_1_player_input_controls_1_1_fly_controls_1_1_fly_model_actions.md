---
title: Graph3DVisualizer::PlayerInputControls::FlyControls::FlyModelActions

---

# Graph3DVisualizer::PlayerInputControls::FlyControls::FlyModelActions



## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[FlyModelActions](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-flymodelactions)**(@[FlyControls](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md) wrapper) |
| void | **[Disable](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-disable)**() |
| void | **[Enable](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-enable)**() |
| InputActionMap | **[Get](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-get)**() |
| void | **[SetCallbacks](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-setcallbacks)**([IFlyModelActions](Classes/interface_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_i_fly_model_actions.md) instance) |
| implicit | **[operator InputActionMap](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#function-operator-inputactionmap)**([FlyModelActions](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md) set) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| InputAction | **[ChangeAltitude](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-changealtitude)**  |
| InputAction | **[LookRotation](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-lookrotation)**  |
| InputAction | **[Move](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-move)**  |
| InputAction | **[MoveToPoint](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-movetopoint)**  |
| InputAction | **[ScrollItemList](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-scrollitemlist)**  |
| InputAction | **[SelectItem](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-selectitem)**  |
| bool | **[enabled](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md#property-enabled)**  |

## Public Functions Documentation

### function FlyModelActions

```cpp
FlyModelActions(
    @FlyControls wrapper
)
```


### function Disable

```cpp
void Disable()
```


### function Enable

```cpp
void Enable()
```


### function Get

```cpp
InputActionMap Get()
```


### function SetCallbacks

```cpp
inline void SetCallbacks(
    IFlyModelActions instance
)
```


### function operator InputActionMap

```cpp
static implicit operator InputActionMap(
    FlyModelActions set
)
```


## Public Property Documentation

### property ChangeAltitude

```cpp
InputAction ChangeAltitude;
```


### property LookRotation

```cpp
InputAction LookRotation;
```


### property Move

```cpp
InputAction Move;
```


### property MoveToPoint

```cpp
InputAction MoveToPoint;
```


### property ScrollItemList

```cpp
InputAction ScrollItemList;
```


### property SelectItem

```cpp
InputAction SelectItem;
```


### property enabled

```cpp
bool enabled;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)