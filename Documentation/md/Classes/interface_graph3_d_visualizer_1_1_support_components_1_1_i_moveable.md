---
title: Graph3DVisualizer::SupportComponents::IMoveable
summary: Interface that controls the changing of Transform object. It can report changes coordinates of the object. 

---

# Graph3DVisualizer::SupportComponents::IMoveable



Interface that controls the changing of Transform object. It can report changes coordinates of the object. Inherited by [Graph3DVisualizer.SupportComponents.MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< Vector3, UnityEngine.Object > | **[ObjectMoved](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#event-objectmoved)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| IEnumerator | **[MoveAlongTrajectory](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-movealongtrajectory)**(IReadOnlyList< Vector3 > trajectory) |
| void | **[Rotate](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-rotate)**(Vector2 rotationChange, float deltaTime) |
| void | **[Translate](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#function-translate)**(Vector3 moveVector, float deltaTime) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| Vector3 | **[GlobalCoordinates](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#property-globalcoordinates)**  |
| Vector3 | **[LocalCoordinates](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#property-localcoordinates)**  |
| float | **[MovingSpeed](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#property-movingspeed)**  |
| float | **[RotationSpeed](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md#property-rotationspeed)**  |

## Public Events Documentation

### event ObjectMoved

```cpp
Action< Vector3, UnityEngine.Object > ObjectMoved()
```


## Public Functions Documentation

### function MoveAlongTrajectory

```cpp
IEnumerator MoveAlongTrajectory(
    IReadOnlyList< Vector3 > trajectory
)
```


**Reimplemented by**: [Graph3DVisualizer::SupportComponents::MovementComponent::MoveAlongTrajectory](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-movealongtrajectory)


### function Rotate

```cpp
void Rotate(
    Vector2 rotationChange,
    float deltaTime
)
```


**Reimplemented by**: [Graph3DVisualizer::SupportComponents::MovementComponent::Rotate](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-rotate)


### function Translate

```cpp
void Translate(
    Vector3 moveVector,
    float deltaTime
)
```


**Reimplemented by**: [Graph3DVisualizer::SupportComponents::MovementComponent::Translate](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md#function-translate)


## Public Property Documentation

### property GlobalCoordinates

```cpp
Vector3 GlobalCoordinates;
```


### property LocalCoordinates

```cpp
Vector3 LocalCoordinates;
```


### property MovingSpeed

```cpp
float MovingSpeed;
```


### property RotationSpeed

```cpp
float RotationSpeed;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)