---
title: Graph3DVisualizer::Graph3D::AdjacentVertices
summary: A class for describing adjacent vertexes. 

---

# Graph3DVisualizer::Graph3D::AdjacentVertices



A class for describing adjacent vertexes. ## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#function-adjacentvertices)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) fromVertex, [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex) |
| void | **[Deconstruct](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#function-deconstruct)**(out [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) fromVertex, out [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex) |
| float | **[GetDistance](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#function-getdistance)**() |
| Vector3 | **[GetMiddlePoint](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#function-getmiddlepoint)**() |
| Vector3 | **[GetUnitVector](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#function-getunitvector)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[FromVertex](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#property-fromvertex)**  |
| [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[ToVertex](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md#property-tovertex)**  |

## Public Functions Documentation

### function AdjacentVertices

```cpp
inline AdjacentVertices(
    AbstractVertex fromVertex,
    AbstractVertex toVertex
)
```


### function Deconstruct

```cpp
void Deconstruct(
    out AbstractVertex fromVertex,
    out AbstractVertex toVertex
)
```


### function GetDistance

```cpp
float GetDistance()
```


### function GetMiddlePoint

```cpp
Vector3 GetMiddlePoint()
```


### function GetUnitVector

```cpp
Vector3 GetUnitVector()
```


## Public Property Documentation

### property FromVertex

```cpp
AbstractVertex FromVertex;
```


### property ToVertex

```cpp
AbstractVertex ToVertex;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)