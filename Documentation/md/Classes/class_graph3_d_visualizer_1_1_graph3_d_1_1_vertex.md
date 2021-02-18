---
title: Graph3DVisualizer::Graph3D::Vertex
summary: Simple realization of AbstractVertex. 

---

# Graph3DVisualizer::Graph3D::Vertex



Simple realization of [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md). Inherits from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md), [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md), [Graph3DVisualizer.SupportComponents.IDestructible](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md), [Graph3DVisualizer.Customizable.ICustomizable< VertexParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

Inherited by [Graph3DVisualizer.Graph3D.SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| override Action< UnityEngine.Object > | **[Destroyed](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#event-destroyed)**() |
| override Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#event-visiblechanged)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual void | **[UpdateColliderRange](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#function-updatecolliderrange)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| override [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md) | **[MovementComponent](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#property-movementcomponent)**  |
| override Vector2 | **[SetMainImageSize](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#property-setmainimagesize)**  |
| override bool? | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#property-visibility)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| SphereCollider | **[_sphereCollider](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#variable-_spherecollider)**  |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-downloadparams)**() |
| TEdge | **[Link< TEdge, TParameters >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link<-tedge,-tparameters->)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, TParameters edgeParameters) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| virtual void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-setupparams)**([VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) parameters) |
| void | **[UnLink< TEdge >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-unlink<-tedge->)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex) |
| void | **[UnLink](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-unlink)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |

**Protected Functions inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[CheckLinkForCorrectness](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-checklinkforcorrectness)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[FindOppositeEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-findoppositeedge)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[RemoveLinkFromArray](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-removelinkfromarray)**(List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > links, [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType) |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| IReadOnlyList< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[IncomingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-incominglinks)**  |
| IReadOnlyList< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[OutgoingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-outgoinglinks)**  |

**Protected Attributes inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| [BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md) | **[_billboardControler](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_billboardcontroler)**  |
| GameObject | **[_edgePrefab](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_edgeprefab)**  |
| List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[_incomingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_incominglinks)**  |
| [BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) | **[_mainImageId](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_mainimageid)**  |
| List< [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md) > | **[_outgoingLinks](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_outgoinglinks)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_transform)**  |
| bool | **[_visible](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#variable-_visible)**  |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)**

|                | Name           |
| -------------- | -------------- |
| string?? | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md#property-id)**  |

**Public Functions inherited from [Graph3DVisualizer.Customizable.ICustomizable< VertexParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| TParams | **[DownloadParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-downloadparams)**() |
| void | **[SetupParams](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md#function-setupparams)**(TParams parameters) |


## Public Events Documentation

### event Destroyed

```cpp
override Action< UnityEngine.Object > Destroyed()
```


### event VisibleChanged

```cpp
override Action< bool, UnityEngine.Object > VisibleChanged()
```


## Public Functions Documentation

### function SetMainImage

```cpp
inline virtual override void SetMainImage(
    BillboardParameters billboardParameters
)
```


**Reimplements**: [Graph3DVisualizer::Graph3D::AbstractVertex::SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-setmainimage)


## Protected Functions Documentation

### function UpdateColliderRange

```cpp
inline virtual void UpdateColliderRange()
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::SelectableVertex::UpdateColliderRange](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#function-updatecolliderrange)


## Public Property Documentation

### property MovementComponent

```cpp
override MovementComponent MovementComponent;
```


### property SetMainImageSize

```cpp
override Vector2 SetMainImageSize;
```


### property Visibility

```cpp
override bool? Visibility;
```


## Protected Attributes Documentation

### variable _sphereCollider

```cpp
SphereCollider _sphereCollider;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)