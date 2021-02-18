---
title: Graph3DVisualizer::Graph3D::Edge
summary: Simple realization of AbstractEdge. 

---

# Graph3DVisualizer::Graph3D::Edge



Simple realization of [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md). Inherits from [Graph3DVisualizer.Graph3D.AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md), [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.Customizable.ICustomizable< EdgeParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[UpdateCoordinates](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatecoordinates)**() |
| virtual override void | **[UpdateEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updateedge)**() |
| virtual override void | **[UpdateType](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatetype)**() |
| virtual override void | **[UpdateVisibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatevisibility)**() |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-subscribeonverticesevents)**() |
| virtual override void | **[UnsubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-unsubscribeonverticesevents)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| override Texture2D | **[ArrowTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-arrowtexture)**  |
| override Texture2D | **[LineTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-linetexture)**  |
| override float | **[SourceOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-sourceoffsetdist)**  |
| override float | **[TargetOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-targetoffsetdist)**  |
| override EdgeType | **[Type](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-type)**  |
| override EdgeVisibility | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#property-visibility)**  |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md)**

|                | Name           |
| -------------- | -------------- |
| [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-setupparams)**([EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) parameters) |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md)**

|                | Name           |
| -------------- | -------------- |
| [AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md) | **[AdjacentVertices](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-adjacentvertices)**  |

**Protected Attributes inherited from [Graph3DVisualizer.Graph3D.AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md)**

|                | Name           |
| -------------- | -------------- |
| [AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md) | **[_adjacentVertices](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_adjacentvertices)**  |
| Texture2D | **[_arrowTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_arrowtexture)**  |
| GameObject | **[_gameObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_gameobject)**  |
| Texture2D | **[_lineTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_linetexture)**  |
| float | **[_sourceOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_sourceoffsetdist)**  |
| float | **[_targetOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_targetoffsetdist)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_transform)**  |
| EdgeType | **[_type](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_type)**  |
| EdgeVisibility | **[_visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#variable-_visibility)**  |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |

**Public Functions inherited from [Graph3DVisualizer.Customizable.ICustomizable< EdgeParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| TParams | **[DownloadParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-setupparams)**(TParams parameters) |


## Public Functions Documentation

### function UpdateCoordinates

```cpp
inline virtual override void UpdateCoordinates()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::UpdateCoordinates](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatecoordinates)


### function UpdateEdge

```cpp
inline virtual override void UpdateEdge()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::UpdateEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updateedge)


### function UpdateType

```cpp
inline virtual override void UpdateType()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::UpdateType](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatetype)


### function UpdateVisibility

```cpp
inline virtual override void UpdateVisibility()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::UpdateVisibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatevisibility)


## Protected Functions Documentation

### function SubscribeOnVerticesEvents

```cpp
inline virtual override void SubscribeOnVerticesEvents()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::SubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-subscribeonverticesevents)


### function UnsubscribeOnVerticesEvents

```cpp
inline virtual override void UnsubscribeOnVerticesEvents()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractEdge::UnsubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-unsubscribeonverticesevents)


## Public Property Documentation

### property ArrowTexture

```cpp
override Texture2D ArrowTexture;
```


### property LineTexture

```cpp
override Texture2D LineTexture;
```


### property SourceOffsetDist

```cpp
override float SourceOffsetDist;
```


### property TargetOffsetDist

```cpp
override float TargetOffsetDist;
```


### property Type

```cpp
override EdgeType Type;
```


### property Visibility

```cpp
override EdgeVisibility Visibility;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)