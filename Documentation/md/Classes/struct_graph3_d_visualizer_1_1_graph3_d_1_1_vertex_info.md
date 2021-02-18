---
title: Graph3DVisualizer::Graph3D::VertexInfo
summary: Support class for vertex serialization. 

---

# Graph3DVisualizer::Graph3D::VertexInfo



Support class for vertex serialization. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-vertexinfo)**(Type vertexType, [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) vertexParameters) |
| void | **[Deconstruct](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-deconstruct)**(out Type vertexType, out [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) vertexParameters) |
| override bool | **[Equals](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-equals)**(object obj) |
| override int | **[GetHashCode](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-gethashcode)**() |
| implicit | **[operator](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-operator)**(Type vertexType, [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) vertexParameters) |
| implicit | **[operator VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#function-operator-vertexinfo)**((Type vertexType, [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) vertexParameters) value) |

## Public Attributes

|                | Name           |
| -------------- | -------------- |
| [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) | **[vertexParameters](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#variable-vertexparameters)**  |
| Type | **[vertexType](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md#variable-vertextype)**  |

## Public Functions Documentation

### function VertexInfo

```cpp
inline VertexInfo(
    Type vertexType,
    VertexParameters vertexParameters
)
```


### function Deconstruct

```cpp
inline void Deconstruct(
    out Type vertexType,
    out VertexParameters vertexParameters
)
```


### function Equals

```cpp
inline override bool Equals(
    object obj
)
```


### function GetHashCode

```cpp
inline override int GetHashCode()
```


### function operator

```cpp
static inline implicit operator(
    Type vertexType,
    VertexParameters vertexParameters
)
```


### function operator VertexInfo

```cpp
static inline implicit operator VertexInfo(
    (Type vertexType, VertexParameters vertexParameters) value
)
```


## Public Attributes Documentation

### variable vertexParameters

```cpp
VertexParameters vertexParameters;
```


### variable vertexType

```cpp
Type vertexType;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)