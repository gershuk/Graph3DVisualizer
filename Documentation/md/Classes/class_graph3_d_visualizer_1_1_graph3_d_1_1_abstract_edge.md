---
title: Graph3DVisualizer::Graph3D::AbstractEdge
summary: Abstarct class for describing visual part of the graph edge. 

---

# Graph3DVisualizer::Graph3D::AbstractEdge



Abstarct class for describing visual part of the graph edge. Inherits from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.Customizable.ICustomizable< EdgeParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

Inherited by [Graph3DVisualizer.Graph3D.Edge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-setupparams)**([EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) parameters) |
| virtual abstract void | **[UpdateCoordinates](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatecoordinates)**() =0 |
| virtual abstract void | **[UpdateEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updateedge)**() =0 |
| virtual abstract void | **[UpdateType](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatetype)**() =0 |
| virtual abstract void | **[UpdateVisibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-updatevisibility)**() =0 |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract void | **[SubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-subscribeonverticesevents)**() =0 |
| virtual abstract void | **[UnsubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#function-unsubscribeonverticesevents)**() =0 |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| [AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md) | **[AdjacentVertices](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-adjacentvertices)**  |
| abstract Texture2D | **[ArrowTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-arrowtexture)**  |
| abstract Texture2D | **[LineTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-linetexture)**  |
| abstract float | **[SourceOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-sourceoffsetdist)**  |
| abstract float | **[TargetOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-targetoffsetdist)**  |
| abstract EdgeType | **[Type](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-type)**  |
| abstract EdgeVisibility | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md#property-visibility)**  |

## Protected Attributes

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

## Additional inherited members

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |


## Public Functions Documentation

### function DownloadParams

```cpp
EdgeParameters DownloadParams()
```


### function SetupParams

```cpp
inline void SetupParams(
    EdgeParameters parameters
)
```


### function UpdateCoordinates

```cpp
virtual abstract void UpdateCoordinates() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::UpdateCoordinates](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatecoordinates)


### function UpdateEdge

```cpp
virtual abstract void UpdateEdge() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::UpdateEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updateedge)


### function UpdateType

```cpp
virtual abstract void UpdateType() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::UpdateType](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatetype)


### function UpdateVisibility

```cpp
virtual abstract void UpdateVisibility() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::UpdateVisibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-updatevisibility)


## Protected Functions Documentation

### function SubscribeOnVerticesEvents

```cpp
virtual abstract void SubscribeOnVerticesEvents() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::SubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-subscribeonverticesevents)


### function UnsubscribeOnVerticesEvents

```cpp
virtual abstract void UnsubscribeOnVerticesEvents() =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Edge::UnsubscribeOnVerticesEvents](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md#function-unsubscribeonverticesevents)


## Public Property Documentation

### property AdjacentVertices

```cpp
AdjacentVertices AdjacentVertices;
```


### property ArrowTexture

```cpp
abstract Texture2D ArrowTexture;
```


### property LineTexture

```cpp
abstract Texture2D LineTexture;
```


### property SourceOffsetDist

```cpp
abstract float SourceOffsetDist;
```


### property TargetOffsetDist

```cpp
abstract float TargetOffsetDist;
```


### property Type

```cpp
abstract EdgeType Type;
```


### property Visibility

```cpp
abstract EdgeVisibility Visibility;
```


## Protected Attributes Documentation

### variable _adjacentVertices

```cpp
AdjacentVertices _adjacentVertices;
```


### variable _arrowTexture

```cpp
Texture2D _arrowTexture;
```


### variable _gameObject

```cpp
GameObject _gameObject;
```


### variable _lineTexture

```cpp
Texture2D _lineTexture;
```


### variable _sourceOffsetDist

```cpp
float _sourceOffsetDist;
```


### variable _targetOffsetDist

```cpp
float _targetOffsetDist;
```


### variable _transform

```cpp
Transform _transform;
```


### variable _type

```cpp
EdgeType _type;
```


### variable _visibility

```cpp
EdgeVisibility _visibility;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)