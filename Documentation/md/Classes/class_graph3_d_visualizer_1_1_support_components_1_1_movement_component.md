---
title: Graph3DVisualizer::SupportComponents::MovementComponent
summary: Component that controls the movement of the object. 

---

# Graph3DVisualizer::SupportComponents::MovementComponent



Component that controls the movement of the object. Inherits from MonoBehaviour, [Graph3DVisualizer.SupportComponents.IMoveable](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< Vector3, UnityEngine.Object > | **[ObjectMoved](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#event-objectmoved)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| IEnumerator | **[MoveAlongTrajectory](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-movealongtrajectory)**(IReadOnlyList< Vector3 > trajectory) |
| void | **[Rotate](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-rotate)**(Vector2 rotationChange, float deltaTime) |
| void | **[Translate](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-translate)**(Vector3 directionVector, float deltaTime) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| int? | **[CurrentGearIndex](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-currentgearindex)**  |
| List<(float deltaTimeFromStart, float multiplier)>? | **[Gears](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-gears)**  |
| Vector3? | **[GlobalCoordinates](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-globalcoordinates)**  |
| Vector3? | **[LocalCoordinates](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-localcoordinates)**  |
| float | **[MovingSpeed](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-movingspeed)**  |
| Vector3 | **[Rotation](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-rotation)**  |
| float | **[RotationSpeed](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-rotationspeed)**  |
| TransmissionMode | **[TransmissionMode](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#property-transmissionmode)**  |

## Public Events Documentation

### event ObjectMoved

```cpp
Action< Vector3, UnityEngine.Object > ObjectMoved()
```


## Public Functions Documentation

### function MoveAlongTrajectory

```cpp
inline IEnumerator MoveAlongTrajectory(
    IReadOnlyList< Vector3 > trajectory
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::IMoveable::MoveAlongTrajectory](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-movealongtrajectory)


### function Rotate

```cpp
inline void Rotate(
    Vector2 rotationChange,
    float deltaTime
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::IMoveable::Rotate](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-rotate)


### function Translate

```cpp
inline void Translate(
    Vector3 directionVector,
    float deltaTime
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::IMoveable::Translate](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-translate)


## Public Property Documentation

### property CurrentGearIndex

```cpp
int? CurrentGearIndex;
```


### property Gears

```cpp
List<(float deltaTimeFromStart, float multiplier)>? Gears;
```


### property GlobalCoordinates

```cpp
Vector3? GlobalCoordinates;
```


### property LocalCoordinates

```cpp
Vector3? LocalCoordinates;
```


### property MovingSpeed

```cpp
float MovingSpeed;
```


### property Rotation

```cpp
Vector3 Rotation;
```


### property RotationSpeed

```cpp
float RotationSpeed;
```


### property TransmissionMode

```cpp
TransmissionMode TransmissionMode;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)