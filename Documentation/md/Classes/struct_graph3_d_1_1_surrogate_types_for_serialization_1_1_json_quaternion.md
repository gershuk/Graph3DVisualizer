---
title: Graph3D::SurrogateTypesForSerialization::JsonQuaternion

---

# Graph3D::SurrogateTypesForSerialization::JsonQuaternion



## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[JsonQuaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#function-jsonquaternion)**(float x, float y, float z, float w) |
| implicit | **[operator JsonQuaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#function-operator-jsonquaternion)**(Quaternion color) |
| implicit | **[operator Quaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#function-operator-quaternion)**([JsonQuaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md) jsonQuaternion) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[W](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#property-w)**  |
| float | **[X](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#property-x)**  |
| float | **[Y](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#property-y)**  |
| float | **[Z](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md#property-z)**  |

## Public Functions Documentation

### function JsonQuaternion

```cpp
JsonQuaternion(
    float x,
    float y,
    float z,
    float w
)
```


### function operator JsonQuaternion

```cpp
static implicit operator JsonQuaternion(
    Quaternion color
)
```


### function operator Quaternion

```cpp
static implicit operator Quaternion(
    JsonQuaternion jsonQuaternion
)
```


## Public Property Documentation

### property W

```cpp
float W;
```


### property X

```cpp
float X;
```


### property Y

```cpp
float Y;
```


### property Z

```cpp
float Z;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)