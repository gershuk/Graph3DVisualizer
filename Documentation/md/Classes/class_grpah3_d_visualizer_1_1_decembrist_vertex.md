---
title: Grpah3DVisualizer::DecembristVertex


---

# Grpah3DVisualizer::DecembristVertex








Inherits from [Grpah3DVisualizer.Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md), MonoBehaviour, [SupportComponents.IVisibile](Classes/interface_support_components_1_1_i_visibile.md), [SupportComponents.IDestructible](Classes/interface_support_components_1_1_i_destructible.md), [SupportComponents.ISelectable](Classes/interface_support_components_1_1_i_selectable.md), [SupportComponents.ICustomizable< VertexParameters >](Classes/interface_support_components_1_1_i_customizable.md)















## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool | **[IsDec](Classes/class_grpah3_d_visualizer_1_1_decembrist_vertex.md#property-isdec)**  |
| string | **[Name](Classes/class_grpah3_d_visualizer_1_1_decembrist_vertex.md#property-name)**  |







## Additional inherited members










**Public Events inherited from [Grpah3DVisualizer.Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object > | **[Destroyed](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-destroyed)**()  |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-visiblechanged)**()  |
| Action< UnityEngine.Object, bool > | **[SelectedChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-selectedchanged)**()  |
| Action< UnityEngine.Object, bool > | **[HighlightedChanged](Classes/class_grpah3_d_visualizer_1_1_vertex.md#event-highlightedchanged)**()  |


**Public Functions inherited from [Grpah3DVisualizer.Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[SetMainImage](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setmainimage)**([BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) billboardParameters)  |
| void | **[SetSelectFrame](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setselectframe)**([BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) billboardParameters)  |
| [Edge](Classes/class_grpah3_d_visualizer_1_1_edge.md) | **[Link](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-link)**([Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md) toVertex, Type edgeType, in [LinkParameters](Classes/struct_grpah3_d_visualizer_1_1_link_parameters.md) linkParameters)  |
| void | **[UnLink](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-unlink)**([Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md) toVertex, Type edgeType)  |
| void | **[SetupParams](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-setupparams)**([VertexParameters](Classes/class_grpah3_d_visualizer_1_1_vertex_parameters.md) parameters)  |
| [VertexParameters](Classes/class_grpah3_d_visualizer_1_1_vertex_parameters.md) | **[DownloadParams](Classes/class_grpah3_d_visualizer_1_1_vertex.md#function-downloadparams)**()  |


**Public Properties inherited from [Grpah3DVisualizer.Vertex](Classes/class_grpah3_d_visualizer_1_1_vertex.md)**

|                | Name           |
| -------------- | -------------- |
| [MoveComponent](Classes/class_support_components_1_1_move_component.md) | **[MoveComponent](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-movecomponent)**  |
| bool? | **[Visibility](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-visibility)**  |
| bool? | **[IsSelected](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-isselected)**  |
| bool | **[IsHighlighted](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-ishighlighted)**  |
| Color | **[SelectFrameColor](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-selectframecolor)**  |
| Vector2 | **[SetSelectFrameSize](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-setselectframesize)**  |
| Vector2 | **[SetMainImageSize](Classes/class_grpah3_d_visualizer_1_1_vertex.md#property-setmainimagesize)**  |
































**Public Events inherited from [SupportComponents.IVisibile](Classes/interface_support_components_1_1_i_visibile.md)**

|                | Name           |
| -------------- | -------------- |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/interface_support_components_1_1_i_visibile.md#event-visiblechanged)**()  |




**Public Properties inherited from [SupportComponents.IVisibile](Classes/interface_support_components_1_1_i_visibile.md)**

|                | Name           |
| -------------- | -------------- |
| bool | **[Visibility](Classes/interface_support_components_1_1_i_visibile.md#property-visibility)**  |














**Public Events inherited from [SupportComponents.IDestructible](Classes/interface_support_components_1_1_i_destructible.md)**

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object > | **[Destroyed](Classes/interface_support_components_1_1_i_destructible.md#event-destroyed)**()  |


















**Public Events inherited from [SupportComponents.ISelectable](Classes/interface_support_components_1_1_i_selectable.md)**

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object, bool > | **[SelectedChanged](Classes/interface_support_components_1_1_i_selectable.md#event-selectedchanged)**()  |
| Action< UnityEngine.Object, bool > | **[HighlightedChanged](Classes/interface_support_components_1_1_i_selectable.md#event-highlightedchanged)**()  |




**Public Properties inherited from [SupportComponents.ISelectable](Classes/interface_support_components_1_1_i_selectable.md)**

|                | Name           |
| -------------- | -------------- |
| bool | **[IsSelected](Classes/interface_support_components_1_1_i_selectable.md#property-isselected)**  |
| bool | **[IsHighlighted](Classes/interface_support_components_1_1_i_selectable.md#property-ishighlighted)**  |
| Color | **[SelectFrameColor](Classes/interface_support_components_1_1_i_selectable.md#property-selectframecolor)**  |
















**Public Functions inherited from [SupportComponents.ICustomizable< VertexParameters >](Classes/interface_support_components_1_1_i_customizable.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[SetupParams](Classes/interface_support_components_1_1_i_customizable.md#function-setupparams)**(TParams parameters)  |
| TParams | **[DownloadParams](Classes/interface_support_components_1_1_i_customizable.md#function-downloadparams)**()  |






















## Public Property Documentation

### property IsDec

```cpp
bool IsDec = false;
```





























### property Name

```cpp
string Name;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)