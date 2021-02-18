---
title: Graph3DVisualizer::Graph3D::LinkInfo
summary: Support class for edge serialization. 

---

# Graph3DVisualizer::Graph3D::LinkInfo



Support class for edge serialization. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-linkinfo)**(string firstVertexId, string secondVertexId, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| void | **[Deconstruct](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-deconstruct)**(out string firstVertexId, out string secondVertexId, out Type edgeType, out [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| override bool | **[Equals](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-equals)**(object obj) |
| override int | **[GetHashCode](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-gethashcode)**() |
| implicit | **[operator](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-operator)**(string firstVertexId, string secondVertexId, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| implicit | **[operator LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#function-operator-linkinfo)**((string firstVertexId, string secondVertexId, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) value) |

## Public Attributes

|                | Name           |
| -------------- | -------------- |
| [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) | **[edgeParameters](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#variable-edgeparameters)**  |
| Type | **[edgeType](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#variable-edgetype)**  |
| string | **[firstVertexId](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#variable-firstvertexid)**  |
| string | **[secondVertexId](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md#variable-secondvertexid)**  |

## Public Functions Documentation

### function LinkInfo

```cpp
inline LinkInfo(
    string firstVertexId,
    string secondVertexId,
    Type edgeType,
    EdgeParameters edgeParameters
)
```


### function Deconstruct

```cpp
inline void Deconstruct(
    out string firstVertexId,
    out string secondVertexId,
    out Type edgeType,
    out EdgeParameters edgeParameters
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
    string firstVertexId,
    string secondVertexId,
    Type edgeType,
    EdgeParameters edgeParameters
)
```


### function operator LinkInfo

```cpp
static inline implicit operator LinkInfo(
    (string firstVertexId, string secondVertexId, Type edgeType, EdgeParameters edgeParameters) value
)
```


## Public Attributes Documentation

### variable edgeParameters

```cpp
EdgeParameters edgeParameters;
```


### variable edgeType

```cpp
Type edgeType;
```


### variable firstVertexId

```cpp
string firstVertexId;
```


### variable secondVertexId

```cpp
string secondVertexId;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)