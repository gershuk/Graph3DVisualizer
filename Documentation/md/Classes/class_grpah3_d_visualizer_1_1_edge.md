---
title: Grpah3DVisualizer::Edge


---

# Grpah3DVisualizer::Edge








Inherits from MonoBehaviour, [SupportComponents.ICustomizable< EdgeParameters >](Classes/interface_support_components_1_1_i_customizable.md)













## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[UpdateType](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-updatetype)**()  |
| void | **[UpdateCoordinates](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-updatecoordinates)**()  |
| void | **[UpdateVisibility](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-updatevisibility)**()  |
| void | **[UpdateEdge](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-updateedge)**()  |
| void | **[SetupParams](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-setupparams)**([EdgeParameters](Classes/class_grpah3_d_visualizer_1_1_edge_parameters.md) parameters)  |
| [EdgeParameters](Classes/class_grpah3_d_visualizer_1_1_edge_parameters.md) | **[DownloadParams](Classes/class_grpah3_d_visualizer_1_1_edge.md#function-downloadparams)**()  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| [AdjacentVertices](Classes/struct_grpah3_d_visualizer_1_1_adjacent_vertices.md) | **[AdjacentVertices](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-adjacentvertices)**  |
| [EdgeType](Namespaces/namespace_grpah3_d_visualizer.md#enum-edgetype) | **[Type](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-type)**  |
| float | **[SourceOffsetDist](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-sourceoffsetdist)**  |
| float | **[TargetOffsetDist](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-targetoffsetdist)**  |
| [EdgeVisibility](Namespaces/namespace_grpah3_d_visualizer.md#enum-edgevisibility) | **[Visibility](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-visibility)**  |
| Texture2D | **[LineTexture](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-linetexture)**  |
| Texture2D | **[ArrowTexture](Classes/class_grpah3_d_visualizer_1_1_edge.md#property-arrowtexture)**  |



















## Public Functions Documentation

### function UpdateType

```cpp
inline void UpdateType()
```





























### function UpdateCoordinates

```cpp
inline void UpdateCoordinates()
```





























### function UpdateVisibility

```cpp
inline void UpdateVisibility()
```





























### function UpdateEdge

```cpp
inline void UpdateEdge()
```





























### function SetupParams

```cpp
inline void SetupParams(
    EdgeParameters parameters
)
```





























### function DownloadParams

```cpp
EdgeParameters DownloadParams()
```































## Public Property Documentation

### property AdjacentVertices

```cpp
AdjacentVertices AdjacentVertices;
```





























### property Type

```cpp
EdgeType Type;
```





























### property SourceOffsetDist

```cpp
float SourceOffsetDist;
```





























### property TargetOffsetDist

```cpp
float TargetOffsetDist;
```





























### property Visibility

```cpp
EdgeVisibility Visibility;
```





























### property LineTexture

```cpp
Texture2D LineTexture;
```





























### property ArrowTexture

```cpp
Texture2D ArrowTexture;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)