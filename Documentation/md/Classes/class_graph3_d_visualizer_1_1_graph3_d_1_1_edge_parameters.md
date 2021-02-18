---
title: Graph3DVisualizer::Graph3D::EdgeParameters
summary: A class that describes default edge parameters for ICustomizable<TParams>. 

---

# Graph3DVisualizer::Graph3D::EdgeParameters



A class that describes default edge parameters for ICustomizable<TParams>. Inherits from [Graph3DVisualizer.Graph3D.AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md), [Graph3DVisualizer.Customizable.AbstractCustomizableParameter](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_abstract_customizable_parameter.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| | **[EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#function-edgeparameters)**(float sourceOffsetDist =1f, float targetOffsetDist =1f, Texture2D arrowTexture =null, Texture2D lineTexture =null, EdgeVisibility visibility =EdgeVisibility.DependOnVertices, string id =null) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| Texture2D | **[ArrowTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#property-arrowtexture)**  |
| Texture2D | **[LineTexture](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#property-linetexture)**  |
| float | **[SourceOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#property-sourceoffsetdist)**  |
| float | **[TargetOffsetDist](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#property-targetoffsetdist)**  |
| EdgeVisibility | **[Visibility](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md#property-visibility)**  |

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

### function EdgeParameters

```cpp
inline EdgeParameters(
    float sourceOffsetDist =1f,
    float targetOffsetDist =1f,
    Texture2D arrowTexture =null,
    Texture2D lineTexture =null,
    EdgeVisibility visibility =EdgeVisibility.DependOnVertices,
    string id =null
)
```


## Public Property Documentation

### property ArrowTexture

```cpp
Texture2D ArrowTexture;
```


### property LineTexture

```cpp
Texture2D LineTexture;
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


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)