---
title: Graph3DVisualizer::Graph3D::Graph
summary: Simple realization of AbstractGraph. 

---

# Graph3DVisualizer::Graph3D::Graph



Simple realization of [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md). Inherits from [Graph3DVisualizer.Graph3D.AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md), [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.Customizable.ICustomizable< GraphParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override bool | **[ContainsVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-containsvertex)**(string id) |
| virtual override bool | **[DeleteVeretex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-deleteveretex)**(string id) |
| virtual override [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[GetVertexById](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-getvertexbyid)**(string id) |
| virtual override IReadOnlyList< [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) > | **[GetVertexes](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-getvertexes)**() |
| virtual override TVertex | **[SpawnVertex< TVertex, TParams >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-spawnvertex<-tvertex,-tparams->)**(TParams vertexParameters) |
| virtual override [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) | **[SpawnVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#function-spawnvertex)**(Type vertexType, [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) parameters) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| override int | **[VertexesCount](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md#property-vertexescount)**  |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md)**

|                | Name           |
| -------------- | -------------- |
| [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-setupparams)**([GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md) parameters) |

**Protected Attributes inherited from [Graph3DVisualizer.Graph3D.AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md)**

|                | Name           |
| -------------- | -------------- |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#variable-_transform)**  |
| GameObject | **[_vertexPrefab](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#variable-_vertexprefab)**  |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |

**Public Functions inherited from [Graph3DVisualizer.Customizable.ICustomizable< GraphParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| TParams | **[DownloadParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-setupparams)**(TParams parameters) |


## Public Functions Documentation

### function ContainsVertex

```cpp
virtual override bool ContainsVertex(
    string id
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::ContainsVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-containsvertex)


### function DeleteVeretex

```cpp
inline virtual override bool DeleteVeretex(
    string id
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::DeleteVeretex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-deleteveretex)


### function GetVertexById

```cpp
virtual override AbstractVertex GetVertexById(
    string id
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::GetVertexById](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-getvertexbyid)


### function GetVertexes

```cpp
virtual override IReadOnlyList< AbstractVertex > GetVertexes()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::GetVertexes](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-getvertexes)


### function SpawnVertex< TVertex, TParams >

```cpp
inline virtual override TVertex SpawnVertex< TVertex, TParams >(
    TParams vertexParameters
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::SpawnVertex< TVertex, TParams >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-spawnvertex<-tvertex,-tparams->)


### function SpawnVertex

```cpp
inline virtual override AbstractVertex SpawnVertex(
    Type vertexType,
    VertexParameters parameters
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractGraph::SpawnVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md#function-spawnvertex)


## Public Property Documentation

### property VertexesCount

```cpp
override int VertexesCount;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)