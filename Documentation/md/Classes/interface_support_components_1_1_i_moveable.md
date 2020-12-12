---
title: SupportComponents::IMoveable


---

# SupportComponents::IMoveable









Inherited by [SupportComponents.MoveComponent](Classes/class_support_components_1_1_move_component.md)










## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< Vector3, UnityEngine.Object > | **[ObjectMoved](Classes/interface_support_components_1_1_i_moveable.md#event-objectmoved)**()  |


## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[Translate](Classes/interface_support_components_1_1_i_moveable.md#function-translate)**(Vector3 moveVector, float deltaTime)  |
| void | **[Rotate](Classes/interface_support_components_1_1_i_moveable.md#function-rotate)**(Vector2 rotationChange, float deltaTime)  |
| IEnumerator | **[MoveAlongTrajectory](Classes/interface_support_components_1_1_i_moveable.md#function-movealongtrajectory)**(ReadOnlyCollection< Vector3 > trajectory)  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[MovingSpeed](Classes/interface_support_components_1_1_i_moveable.md#property-movingspeed)**  |
| float | **[RotationSpeed](Classes/interface_support_components_1_1_i_moveable.md#property-rotationspeed)**  |
| Vector3 | **[GlobalCoordinates](Classes/interface_support_components_1_1_i_moveable.md#property-globalcoordinates)**  |
| Vector3 | **[LocalCoordinates](Classes/interface_support_components_1_1_i_moveable.md#property-localcoordinates)**  |

















## Public Events Documentation

### event ObjectMoved

```cpp
Action< Vector3, UnityEngine.Object > ObjectMoved()
```































## Public Functions Documentation

### function Translate

```cpp
void Translate(
    Vector3 moveVector,
    float deltaTime
)
```


























**Reimplemented by**: [SupportComponents::MoveComponent::Translate](Classes/class_support_components_1_1_move_component.md#function-translate)




### function Rotate

```cpp
void Rotate(
    Vector2 rotationChange,
    float deltaTime
)
```


























**Reimplemented by**: [SupportComponents::MoveComponent::Rotate](Classes/class_support_components_1_1_move_component.md#function-rotate)




### function MoveAlongTrajectory

```cpp
IEnumerator MoveAlongTrajectory(
    ReadOnlyCollection< Vector3 > trajectory
)
```


























**Reimplemented by**: [SupportComponents::MoveComponent::MoveAlongTrajectory](Classes/class_support_components_1_1_move_component.md#function-movealongtrajectory)






## Public Property Documentation

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
Vector3 GlobalCoordinates;
```





























### property LocalCoordinates

```cpp
Vector3 LocalCoordinates;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)