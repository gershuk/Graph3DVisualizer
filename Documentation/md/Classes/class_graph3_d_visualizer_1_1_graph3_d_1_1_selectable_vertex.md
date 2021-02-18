---
title: Graph3DVisualizer::Graph3D::SelectableVertex
summary: Realization of Vertex with selection support. 

---

# Graph3DVisualizer::Graph3D::SelectableVertex



Realization of [Vertex]() with selection support. Inherits from [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md), [Graph3DVisualizer.Customizable.ICustomizable< SelectableVertexParameters >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), [Graph3DVisualizer.SupportComponents.ISelectable](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md), [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md), [Graph3DVisualizer.Graph3D.AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md), [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md), [Graph3DVisualizer.SupportComponents.IDestructible](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md), MonoBehaviour

Inherited by [Graph3DVisualizer.GraphTasks.DecembristVertex](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_decembrist_vertex.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object, bool > | **[HighlightedChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#event-highlightedchanged)**() |
| Action< UnityEngine.Object, bool > | **[SelectedChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#event-selectedchanged)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[SetSelectFrame](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#function-setselectframe)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#function-setupparams)**([SelectableVertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md) parameters) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[UpdateColliderRange](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#function-updatecolliderrange)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool | **[IsHighlighted](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#property-ishighlighted)**  |
| bool? | **[IsSelected](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#property-isselected)**  |
| override [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md) | **[MovementComponent](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#property-movementcomponent)**  |
| Color | **[SelectFrameColor](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#property-selectframecolor)**  |
| Vector2 | **[SetSelectFrameSize](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md#property-setselectframesize)**  |

## Additional inherited members

**Public Events inherited from [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| override Action< UnityEngine.Object > | **[Destroyed](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#event-destroyed)**() |
| override Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#event-visiblechanged)**() |

**Public Functions inherited from [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| override Vector2 | **[SetMainImageSize](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#property-setmainimagesize)**  |
| override bool? | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#property-visibility)**  |

**Protected Attributes inherited from [Graph3DVisualizer.Graph3D.Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| SphereCollider | **[_sphereCollider](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#variable-_spherecollider)**  |

**Public Events inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| abstract Action< UnityEngine.Object > | **[Destroyed](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#event-destroyed)**() |
| abstract Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#event-visiblechanged)**() |

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| TEdge | **[Link< TEdge, TParameters >](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link<-tedge,-tparameters->)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, TParameters edgeParameters) |
| [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md) | **[Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-link)**([AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md) toVertex, Type edgeType, [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md) edgeParameters) |
| virtual abstract void | **[SetMainImage](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) billboardParameters) =0 |
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
| abstract Vector2 | **[SetMainImageSize](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-setmainimagesize)**  |
| abstract bool | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md#property-visibility)**  |

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

**Public Events inherited from [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md)**

|                | Name           |
| -------------- | -------------- |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md#event-visiblechanged)**() |

**Public Properties inherited from [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md)**

|                | Name           |
| -------------- | -------------- |
| bool | **[Visibility](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md#property-visibility)**  |

**Public Events inherited from [Graph3DVisualizer.SupportComponents.IDestructible](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md)**

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object > | **[Destroyed](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md#event-destroyed)**() |


## Public Events Documentation

### event HighlightedChanged

```cpp
Action< UnityEngine.Object, bool > HighlightedChanged()
```


### event SelectedChanged

```cpp
Action< UnityEngine.Object, bool > SelectedChanged()
```


## Public Functions Documentation

### function SetSelectFrame

```cpp
inline void SetSelectFrame(
    BillboardParameters billboardParameters
)
```


### function SetupParams

```cpp
inline void SetupParams(
    SelectableVertexParameters parameters
)
```


## Protected Functions Documentation

### function UpdateColliderRange

```cpp
inline virtual override void UpdateColliderRange()
```


**Reimplements**: [Graph3DVisualizer::Graph3D::Vertex::UpdateColliderRange](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md#function-updatecolliderrange)


## Public Property Documentation

### property IsHighlighted

```cpp
bool IsHighlighted;
```


### property IsSelected

```cpp
bool? IsSelected;
```


### property MovementComponent

```cpp
override MovementComponent MovementComponent;
```


### property SelectFrameColor

```cpp
Color SelectFrameColor;
```


### property SetSelectFrameSize

```cpp
Vector2 SetSelectFrameSize;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)