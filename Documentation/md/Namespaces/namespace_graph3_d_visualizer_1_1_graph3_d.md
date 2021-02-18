---
title: Graph3DVisualizer::Graph3D

---

# Graph3DVisualizer::Graph3D

## Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[Graph3DVisualizer::Graph3D::AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md)** <br>A class for describing adjacent vertexes.  |
| class | **[Graph3DVisualizer::Graph3D::AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md)** <br>Abstarct class for describing visual part of the graph edge.  |
| class | **[Graph3DVisualizer::Graph3D::EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md)** <br>A class that describes default edge parameters for ICustomizable<TParams>.  |
| struct | **[Graph3DVisualizer::Graph3D::LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md)** <br>Support class for edge serialization.  |
| struct | **[Graph3DVisualizer::Graph3D::VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md)** <br>Support class for vertex serialization.  |
| class | **[Graph3DVisualizer::Graph3D::AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md)** <br>Abstract class that describes graph component.  |
| class | **[Graph3DVisualizer::Graph3D::GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md)** <br>Class that describes default graph parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::Graph3D::AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)** <br>Abstract class that describes vertex component.  |
| class | **[Graph3DVisualizer::Graph3D::Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md)** <br>Abstract class that describes unidirectional link between to current and adjacent vertexes.  |
| class | **[Graph3DVisualizer::Graph3D::LinkNotFoundException](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link_not_found_exception.md)**  |
| class | **[Graph3DVisualizer::Graph3D::VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md)** <br>Class that describes default vertex parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::Graph3D::Edge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md)** <br>Simple realization of [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md).  |
| class | **[Graph3DVisualizer::Graph3D::Graph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md)** <br>Simple realization of [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md).  |
| class | **[Graph3DVisualizer::Graph3D::AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)** <br>Base class for all [Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md) objects.  |
| class | **[Graph3DVisualizer::Graph3D::AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)** <br>Class that describes common [AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md) parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::Graph3D::SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md)** <br>Realization of [Vertex]() with selection support.  |
| class | **[Graph3DVisualizer::Graph3D::SelectableVertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md)** <br>Class that describes [SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md) parameters for ICustomizable<TParams>.  |
| class | **[Graph3DVisualizer::Graph3D::Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)** <br>Simple realization of [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md).  |
| class | **[Graph3DVisualizer::Graph3D::VertexLinksMenu](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_links_menu.md)** <br>A class that is temporarily used to create an interactive vertex menu.  |

## Types

|                | Name           |
| -------------- | -------------- |
| enum| **[EdgeType](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md#enum-edgetype)** { Unidirectional = 0, Bidirectional = 1} |
| enum| **[EdgeVisibility](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md#enum-edgevisibility)** { Hidden = 0, Visible = 1, DependOnVertices = 2} |

## Types Documentation

### enum EdgeType

| Enumerator | Value | Description |
| ---------- | ----- | ----------- |
| Unidirectional | 0|   |
| Bidirectional | 1|   |




### enum EdgeVisibility

| Enumerator | Value | Description |
| ---------- | ----- | ----------- |
| Hidden | 0|   |
| Visible | 1|   |
| DependOnVertices | 2|   |









-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)