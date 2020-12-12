---
title: Grpah3DVisualizer::Vertex


---

# Grpah3DVisualizer::Vertex








Inherits from MonoBehaviour, [SupportComponents.IVisibile](Classes/interface_support_components_1_1_i_visibile.md), [SupportComponents.IDestructible](Classes/interface_support_components_1_1_i_destructible.md), [SupportComponents.ISelectable](Classes/interface_support_components_1_1_i_selectable.md), [SupportComponents.ICustomizable< VertexParameters >](Classes/interface_support_components_1_1_i_customizable.md)

Inherited by [Grpah3DVisualizer.DecembristVertex](Classes/class_grpah3_d_visualizer_1_1_decembrist_vertex.md)










## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object > | **[Destroyed](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-destroyed)**()  |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-visiblechanged)**()  |
| Action< UnityEngine.Object, bool > | **[SelectedChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-selectedchanged)**()  |
| Action< UnityEngine.Object, bool > | **[HighlightedChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-highlightedchanged)**()  |


## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[SetMainImage](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) billboardParameters)  |
| void | **[SetSelectFrame](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setselectframe)**([BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) billboardParameters)  |
| [Edge](Classes/class_grpah3_d_visualizer_1_1_edge.md) | **[Link](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-link)**([Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md) toVertex, Type edgeType, in [LinkParameters](Classes/struct_grpah3_d_visualizer_1_1_link_parameters.md) linkParameters)  |
| void | **[UnLink](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-unlink)**([Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md) toVertex, Type edgeType)  |
| void | **[SetupParams](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setupparams)**([VertexParameters](Classes/class_grpah3_d_visualizer_1_1_vertex_parameters.md) parameters)  |
| [VertexParameters](Classes/class_grpah3_d_visualizer_1_1_vertex_parameters.md) | **[DownloadParams](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-downloadparams)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| [MoveComponent](Classes/class_support_components_1_1_move_component.md) | **[MoveComponent](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-movecomponent)**  |
| bool? | **[Visibility](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-visibility)**  |
| bool? | **[IsSelected](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-isselected)**  |
| bool | **[IsHighlighted](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-ishighlighted)**  |
| Color | **[SelectFrameColor](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-selectframecolor)**  |
| Vector2 | **[SetSelectFrameSize](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-setselectframesize)**  |
| Vector2 | **[SetMainImageSize](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-setmainimagesize)**  |

















## Public Events Documentation

### event Destroyed

```cpp
Action< UnityEngine.Object > Destroyed()
```





























### event VisibleChanged

```cpp
Action< bool, UnityEngine.Object > VisibleChanged()
```





























### event SelectedChanged

```cpp
Action< UnityEngine.Object, bool > SelectedChanged()
```





























### event HighlightedChanged

```cpp
Action< UnityEngine.Object, bool > HighlightedChanged()
```































## Public Functions Documentation

### function SetMainImage

```cpp
inline void SetMainImage(
    BillboardParameters billboardParameters
)
```





























### function SetSelectFrame

```cpp
inline void SetSelectFrame(
    BillboardParameters billboardParameters
)
```





























### function Link

```cpp
inline Edge Link(
    Vertex toVertex,
    Type edgeType,
    in LinkParameters linkParameters
)
```





























### function UnLink

```cpp
inline void UnLink(
    Vertex toVertex,
    Type edgeType
)
```





























### function SetupParams

```cpp
inline void SetupParams(
    VertexParameters parameters
)
```





























### function DownloadParams

```cpp
VertexParameters DownloadParams()
```































## Public Property Documentation

### property MoveComponent

```cpp
MoveComponent MoveComponent;
```





























### property Visibility

```cpp
bool? Visibility;
```





























### property IsSelected

```cpp
bool? IsSelected;
```





























### property IsHighlighted

```cpp
bool IsHighlighted;
```





























### property SelectFrameColor

```cpp
Color SelectFrameColor;
```





























### property SetSelectFrameSize

```cpp
Vector2 SetSelectFrameSize;
```





























### property SetMainImageSize

```cpp
Vector2 SetMainImageSize;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)