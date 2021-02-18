---
title: Graph3DVisualizer::Graph3D::VertexLinksMenu
summary: A class that is temporarily used to create an interactive vertex menu. 

---

# Graph3DVisualizer::Graph3D::VertexLinksMenu



A class that is temporarily used to create an interactive vertex menu.  [More...](#detailed-description)

Inherits from [Graph3DVisualizer.GUI.PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md), [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md), MonoBehaviour

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[ClickAction](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_links_menu.md#function-clickaction)**(GameObject callingObject) |

## Additional inherited members

**Public Functions inherited from [Graph3DVisualizer.GUI.PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)**

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SetDisabled](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-setdisabled)**() |
| void | **[SetSubObjectList](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-setsubobjectlist)**(IReadOnlyList<(float offset, Transform transform)> subObjects, Transform source) |

**Public Properties inherited from [Graph3DVisualizer.GUI.PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)**

|                | Name           |
| -------------- | -------------- |
| float | **[PlainOffset](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#property-plainoffset)**  |

**Protected Attributes inherited from [Graph3DVisualizer.GUI.PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)**

|                | Name           |
| -------------- | -------------- |
| GameObject | **[_plane](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#variable-_plane)**  |
| List<(float offset, Transform transform)> | **[_subObjects](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#variable-_subobjects)**  |

**Public Events inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| Action< GameObject > | **[Clicked](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#event-clicked)**() |

**Public Functions inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[Click](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-click)**(GameObject gameObject) |
| virtual abstract void | **[SetDisabled](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-setdisabled)**() =0 |

**Protected Attributes inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| GameObject | **[_gameObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_gameobject)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_transform)**  |


## Detailed Description

```cpp
class Graph3DVisualizer::Graph3D::VertexLinksMenu;
```

A class that is temporarily used to create an interactive vertex menu. 

In the next versions, it will be replaced with a regular UI interface. 

## Protected Functions Documentation

### function ClickAction

```cpp
inline virtual override void ClickAction(
    GameObject callingObject
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::AbstractClickableObject::ClickAction](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-clickaction)


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)