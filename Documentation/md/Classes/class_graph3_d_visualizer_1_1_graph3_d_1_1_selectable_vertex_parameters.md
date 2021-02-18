---
title: Graph3DVisualizer::Graph3D::SelectableVertexParameters
summary: Class that describes SelectableVertex parameters for ICustomizable<TParams>. 

---

# Graph3DVisualizer::Graph3D::SelectableVertexParameters



Class that describes [SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md) parameters for ICustomizable<TParams>. Inherits from [Graph3DVisualizer.Graph3D.VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md), [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md), [Graph3DVisualizer.Customizable.AbstractCustomizableParameter](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_abstract_customizable_parameter.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [SelectableVertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md)([BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) imageParameters, [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) selectFrameParameters, Vector3 position=default, Quaternion rotation=default, bool isSelected=false, string id=null) | **[SelectableVertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md#function-selectablevertexparameters)**([VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md) vertexParameters, [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) selectFrameParameters, bool isSelected =false, string id =null) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool | **[IsSelected](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md#property-isselected)**  |
| [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) | **[SelectFrameParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md#property-selectframeparameters)**  |

## Additional inherited members

**Public Properties inherited from [Graph3DVisualizer.Graph3D.VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md)**

|                | Name           |
| -------------- | -------------- |
| [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) | **[ImageParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md#property-imageparameters)**  |
| Vector3 | **[Position](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md#property-position)**  |
| Quaternion | **[Rotation](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md#property-rotation)**  |

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)**

|                | Name           |
| -------------- | -------------- |
| | **[AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md#function-abstractgraphobjectparameters)**(string id) |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)**

|                | Name           |
| -------------- | -------------- |
| string | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md#property-id)**  |


## Public Functions Documentation

### function SelectableVertexParameters

```cpp
inline SelectableVertexParameters(BillboardParameters imageParameters, BillboardParameters selectFrameParameters, Vector3 position=default, Quaternion rotation=default, bool isSelected=false, string id=null) SelectableVertexParameters(
    VertexParameters vertexParameters,
    BillboardParameters selectFrameParameters,
    bool isSelected =false,
    string id =null
)
```


## Public Property Documentation

### property IsSelected

```cpp
bool IsSelected;
```


### property SelectFrameParameters

```cpp
BillboardParameters SelectFrameParameters;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)