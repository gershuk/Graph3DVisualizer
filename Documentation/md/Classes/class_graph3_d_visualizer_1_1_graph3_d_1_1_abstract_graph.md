---
title: Graph3DVisualizer::Graph3D::AbstractGraph
summary: Abstract class that describes graph component. 

---

# Graph3DVisualizer::Graph3D::AbstractGraph



Abstract class that describes graph component. Inherits from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.Customizable.ICustomizable< GraphParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

Inherited by [Graph3DVisualizer.Graph3D.Graph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract bool | **[ContainsVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-containsvertex)**(string id) =0 |
| virtual abstract bool | **[DeleteVeretex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-deleteveretex)**(string id) =0 |
| [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-downloadparams)**() |
| virtual abstract [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[GetVertexById](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-getvertexbyid)**(string id) =0 |
| virtual abstract IReadOnlyList< [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) > | **[GetVertexes](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-getvertexes)**() =0 |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-setupparams)**([GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) parameters) |
| virtual abstract TVertex | **[SpawnVertex< TVertex, TParams >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-spawnvertex<-tvertex,-tparams->)**(TParams vertexParameters) =0 |
| virtual abstract [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[SpawnVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-spawnvertex)**(Type vertexType, [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) parameters) =0 |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| abstract int | **[VertexesCount](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#property-vertexescount)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#variable-_transform)**  |
| GameObject | **[_vertexPrefab](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#variable-_vertexprefab)**  |

## Additional inherited members

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |


## Public Functions Documentation

### function ContainsVertex

```cpp
virtual abstract bool ContainsVertex(
    string id
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::ContainsVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-containsvertex)


### function DeleteVeretex

```cpp
virtual abstract bool DeleteVeretex(
    string id
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::DeleteVeretex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-deleteveretex)


### function DownloadParams

```cpp
inline GraphParameters DownloadParams()
```


### function GetVertexById

```cpp
virtual abstract AbstractVertex GetVertexById(
    string id
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::GetVertexById](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-getvertexbyid)


### function GetVertexes

```cpp
virtual abstract IReadOnlyList< AbstractVertex > GetVertexes() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::GetVertexes](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-getvertexes)


### function SetupParams

```cpp
inline void SetupParams(
    GraphParameters parameters
)
```


### function SpawnVertex< TVertex, TParams >

```cpp
virtual abstract TVertex SpawnVertex< TVertex, TParams >(
    TParams vertexParameters
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::SpawnVertex< TVertex, TParams >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-spawnvertex<-tvertex,-tparams->)


### function SpawnVertex

```cpp
virtual abstract AbstractVertex SpawnVertex(
    Type vertexType,
    VertexParameters parameters
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Graph::SpawnVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-spawnvertex)


## Public Property Documentation

### property VertexesCount

```cpp
abstract int VertexesCount;
```


## Protected Attributes Documentation

### variable _transform

```cpp
Transform _transform;
```


### variable _vertexPrefab

```cpp
GameObject _vertexPrefab;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)