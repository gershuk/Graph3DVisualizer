---
title: SupportComponents::MoveComponent


---

# SupportComponents::MoveComponent








Inherits from MonoBehaviour, [SupportComponents.IMoveable](Classes/interface_support_components_1_1_i_moveable.md)











## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< Vector3, UnityEngine.Object > | **[ObjectMoved](Classes/class_support_components_1_1_move_component.md#event-objectmoved)**()  |


## Public Functions

|                | Name           |
| -------------- | -------------- |
| IEnumerator | **[MoveAlongTrajectory](Classes/class_support_components_1_1_move_component.md#function-movealongtrajectory)**(ReadOnlyCollection< Vector3 > trajectory)  |
| void | **[Translate](Classes/class_support_components_1_1_move_component.md#function-translate)**(Vector3 directionVector, float deltaTime)  |
| void | **[Rotate](Classes/class_support_components_1_1_move_component.md#function-rotate)**(Vector2 rotationChange, float deltaTime)  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| List<(float deltaTimeFromStart, float multiplier)>? | **[Gears](Classes/class_support_components_1_1_move_component.md#property-gears)**  |
| int? | **[CurrentGearIndex](Classes/class_support_components_1_1_move_component.md#property-currentgearindex)**  |
| [TransmissionMode](Namespaces/namespace_support_components.md#enum-transmissionmode) | **[TransmissionMode](Classes/class_support_components_1_1_move_component.md#property-transmissionmode)**  |
| float | **[MovingSpeed](Classes/class_support_components_1_1_move_component.md#property-movingspeed)**  |
| float | **[RotationSpeed](Classes/class_support_components_1_1_move_component.md#property-rotationspeed)**  |
| Vector3? | **[GlobalCoordinates](Classes/class_support_components_1_1_move_component.md#property-globalcoordinates)**  |
| Vector3? | **[LocalCoordinates](Classes/class_support_components_1_1_move_component.md#property-localcoordinates)**  |

















## Public Events Documentation

### event ObjectMoved

```cpp
Action< Vector3, UnityEngine.Object > ObjectMoved()
```































## Public Functions Documentation

### function MoveAlongTrajectory

```cpp
inline IEnumerator MoveAlongTrajectory(
    ReadOnlyCollection< Vector3 > trajectory
)
```


























**Reimplements**: [SupportComponents::IMoveable::MoveAlongTrajectory](Classes/interface_support_components_1_1_i_moveable.md#function-movealongtrajectory)




### function Translate

```cpp
inline void Translate(
    Vector3 directionVector,
    float deltaTime
)
```


























**Reimplements**: [SupportComponents::IMoveable::Translate](Classes/interface_support_components_1_1_i_moveable.md#function-translate)




### function Rotate

```cpp
inline void Rotate(
    Vector2 rotationChange,
    float deltaTime
)
```


























**Reimplements**: [SupportComponents::IMoveable::Rotate](Classes/interface_support_components_1_1_i_moveable.md#function-rotate)






## Public Property Documentation

### property Gears

```cpp
List<(float deltaTimeFromStart, float multiplier)>? Gears;
```





























### property CurrentGearIndex

```cpp
int? CurrentGearIndex;
```





























### property TransmissionMode

```cpp
TransmissionMode TransmissionMode;
```





























### property MovingSpeed

```cpp
float MovingSpeed;
```





























### property RotationSpeed

```cpp
float RotationSpeed;
```





























### property GlobalCoordinates

```cpp
Vector3? GlobalCoordinates;
```





























### property LocalCoordinates

```cpp
Vector3? LocalCoordinates;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)