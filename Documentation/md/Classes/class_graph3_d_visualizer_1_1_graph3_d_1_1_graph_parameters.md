---
title: Graph3DVisualizer::Graph3D::GraphParameters
summary: Class that describes default graph parameters for ICustomizable<TParams>. 

---

# Graph3DVisualizer::Graph3D::GraphParameters



Class that describes default graph parameters for ICustomizable<TParams>. Inherits from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md), [Graph3DVisualizer.Customizable.AbstractCustomizableParameter](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_abstract_customizable_parameter.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md#function-graphparameters)**([VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md)[] vertexParameters =default, List< [LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md) > links =default, string id =null) =default |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| List< [LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md) > | **[Links](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md#property-links)**  |
| [VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md)[] | **[VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md#property-vertexparameters)**  |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)**

|                | Name           |
| -------------- | -------------- |
| | **[AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md#function-abstractgraphobjectparameters)**(string id) |

**Public Properties inherited from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)**

|                | Name           |
| -------------- | -------------- |
| string | **[Id](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md#property-id)**  |


## Public Functions Documentation

### function GraphParameters

```cpp
inline GraphParameters(
    VertexInfo[] vertexParameters =default,
    List< LinkInfo > links =default,
    string id =null
) =default
```


## Public Property Documentation

### property Links

```cpp
List< LinkInfo > Links;
```


### property VertexParameters

```cpp
VertexInfo[] VertexParameters;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)