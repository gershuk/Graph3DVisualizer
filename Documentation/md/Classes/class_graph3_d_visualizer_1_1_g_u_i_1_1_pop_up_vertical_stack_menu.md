---
title: Graph3DVisualizer::GUI::PopUpVerticalStackMenu
summary: A component that simulates a 3d popup stack menu. 

---

# Graph3DVisualizer::GUI::PopUpVerticalStackMenu



A component that simulates a 3d popup stack menu.  [More...](#detailed-description)

Inherits from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md), MonoBehaviour

Inherited by [Graph3DVisualizer.Graph3D.VertexLinksMenu](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_links_menu.md)

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SetDisabled](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-setdisabled)**() |
| void | **[SetSubObjectList](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-setsubobjectlist)**(IReadOnlyList<(float offset, Transform transform)> subObjects, Transform source) |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[ClickAction](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-clickaction)**(GameObject gameObject) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| float | **[PlainOffset](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#property-plainoffset)**  |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| GameObject | **[_plane](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#variable-_plane)**  |
| List<(float offset, Transform transform)> | **[_subObjects](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#variable-_subobjects)**  |

## Additional inherited members

**Public Events inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| Action< GameObject > | **[Clicked](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#event-clicked)**() |

**Public Functions inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| void | **[Click](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-click)**(GameObject gameObject) |

**Protected Attributes inherited from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)**

|                | Name           |
| -------------- | -------------- |
| GameObject | **[_gameObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_gameobject)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_transform)**  |


## Detailed Description

```cpp
class Graph3DVisualizer::GUI::PopUpVerticalStackMenu;
```

A component that simulates a 3d popup stack menu. 

In the next versions, it will be replaced with a regular UI interface. 

## Public Functions Documentation

### function SetDisabled

```cpp
virtual override void SetDisabled()
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::AbstractClickableObject::SetDisabled](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-setdisabled)


### function SetSubObjectList

```cpp
inline void SetSubObjectList(
    IReadOnlyList<(float offset, Transform transform)> subObjects,
    Transform source
)
```


## Protected Functions Documentation

### function ClickAction

```cpp
virtual override void ClickAction(
    GameObject gameObject
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::AbstractClickableObject::ClickAction](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-clickaction)


## Public Property Documentation

### property PlainOffset

```cpp
float PlainOffset = 12;
```


## Protected Attributes Documentation

### variable _plane

```cpp
GameObject _plane;
```


### variable _subObjects

```cpp
List<(float offset, Transform transform)> _subObjects;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)