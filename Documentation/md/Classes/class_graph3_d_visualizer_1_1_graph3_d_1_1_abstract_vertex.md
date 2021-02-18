---
title: Graph3DVisualizer::Graph3D::AbstractVertex
summary: Abstract class that describes vertex component. 

---

# Graph3DVisualizer::Graph3D::AbstractVertex



Abstract class that describes vertex component. Inherits from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md), [Graph3DVisualizer.SupportComponents.IDestructible](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md), [Graph3DVisualizer.Customizable.ICustomizable< VertexParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

Inherited by [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| abstract Action< UnityEngine.Object > | **[Destroyed](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#event-destroyed)**() |
| abstract Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#event-visiblechanged)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-downloadparams)**() |
| TEdge | **[Link< TEdge, TParameters >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link<-tedge,-tparameters->)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, TParameters edgeParameters) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| virtual abstract void | **[SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) =0 |
| virtual void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-setupparams)**([VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) parameters) |
| void | **[UnLink< TEdge >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-unlink<-tedge->)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex) |
| void | **[UnLink](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-unlink)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| void | **[CheckLinkForCorrectness](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-checklinkforcorrectness)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[FindOppositeEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-findoppositeedge)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[RemoveLinkFromArray](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-removelinkfromarray)**(List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > links, [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| IReadOnlyList< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[IncomingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-incominglinks)**  |
| abstract [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md) | **[MovementComponent](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-movementcomponent)**  |
| IReadOnlyList< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[OutgoingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-outgoinglinks)**  |
| abstract Vector2 | **[SetMainImageSize](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-setmainimagesize)**  |
| abstract bool | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-visibility)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| [BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md) | **[_billboardControler](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_billboardcontroler)**  |
| GameObject | **[_edgePrefab](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_edgeprefab)**  |
| List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[_incomingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_incominglinks)**  |
| [BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) | **[_mainImageId](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_mainimageid)**  |
| List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[_outgoingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_outgoinglinks)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_transform)**  |
| bool | **[_visible](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_visible)**  |

## Additional inherited members

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |


## Public Events Documentation

### event Destroyed

```cpp
abstract Action< UnityEngine.Object > Destroyed()
```


### event VisibleChanged

```cpp
abstract Action< bool, UnityEngine.Object > VisibleChanged()
```


## Public Functions Documentation

### function DownloadParams

```cpp
VertexParameters DownloadParams()
```


### function Link< TEdge, TParameters >

```cpp
inline TEdge Link< TEdge, TParameters >(
    AbstractVertex toVertex,
    TParameters edgeParameters
)
```


### function Link

```cpp
inline AbstractEdge Link(
    AbstractVertex toVertex,
    Type edgeType,
    EdgeParameters edgeParameters
)
```


### function SetMainImage

```cpp
virtual abstract void SetMainImage(
    BillboardParameters billboardParameters
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::Vertex::SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#function-setmainimage)


### function SetupParams

```cpp
inline virtual void SetupParams(
    VertexParameters parameters
)
```


### function UnLink< TEdge >

```cpp
void UnLink< TEdge >(
    AbstractVertex toVertex
)
```


### function UnLink

```cpp
inline void UnLink(
    AbstractVertex toVertex,
    Type edgeType
)
```


## Protected Functions Documentation

### function CheckLinkForCorrectness

```cpp
inline void CheckLinkForCorrectness(
    AbstractVertex toVertex,
    Type edgeType
)
```


### function FindOppositeEdge

```cpp
inline AbstractEdge FindOppositeEdge(
    AbstractVertex toVertex,
    Type edgeType
)
```


### function RemoveLinkFromArray

```cpp
inline AbstractEdge RemoveLinkFromArray(
    List< Link > links,
    AbstractVertex toVertex,
    Type edgeType
)
```


## Public Property Documentation

### property IncomingLinks

```cpp
IReadOnlyList< Link > IncomingLinks;
```


### property MovementComponent

```cpp
abstract MovementComponent MovementComponent;
```


### property OutgoingLinks

```cpp
IReadOnlyList< Link > OutgoingLinks;
```


### property SetMainImageSize

```cpp
abstract Vector2 SetMainImageSize;
```


### property Visibility

```cpp
abstract bool Visibility;
```


## Protected Attributes Documentation

### variable _billboardControler

```cpp
BillboardController _billboardControler;
```


### variable _edgePrefab

```cpp
GameObject _edgePrefab;
```


### variable _incomingLinks

```cpp
List< Link > _incomingLinks;
```


### variable _mainImageId

```cpp
BillboardId _mainImageId;
```


### variable _outgoingLinks

```cpp
List< Link > _outgoingLinks;
```


### variable _transform

```cpp
Transform _transform;
```


### variable _visible

```cpp
bool _visible = true;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)