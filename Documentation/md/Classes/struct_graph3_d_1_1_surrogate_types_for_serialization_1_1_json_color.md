---
title: Graph3D::SurrogateTypesForSerialization::JsonColor

---

# Graph3D::SurrogateTypesForSerialization::JsonColor



## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[JsonColor](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#function-jsoncolor)**(float r, float g, float b, float a) |
| implicit | **[operator Color](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#function-operator-color)**([JsonColor](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md) jsonColor) |
| implicit | **[operator JsonColor](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#function-operator-jsoncolor)**(Color color) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[A](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#property-a)**  |
| float | **[B](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#property-b)**  |
| float | **[G](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#property-g)**  |
| float | **[R](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md#property-r)**  |

## Public Functions Documentation

### function JsonColor

```cpp
JsonColor(
    float r,
    float g,
    float b,
    float a
)
```


### function operator Color

```cpp
static implicit operator Color(
    JsonColor jsonColor
)
```


### function operator JsonColor

```cpp
static implicit operator JsonColor(
    Color color
)
```


## Public Property Documentation

### property A

```cpp
float A;
```


### property B

```cpp
float B;
```


### property G

```cpp
float G;
```


### property R

```cpp
float R;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)