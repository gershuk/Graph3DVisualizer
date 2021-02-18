---
title: Graph3DVisualizer::SupportComponents::AbstractClickableObject
summary: Component for interacting with an object using Physics.Raycast(Ray). 

---

# Graph3DVisualizer::SupportComponents::AbstractClickableObject



Component for interacting with an object using Physics.Raycast(Ray). Inherits from MonoBehaviour

Inherited by [Graph3DVisualizer.GUI.Button3DComponnet](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md), [Graph3DVisualizer.GUI.PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< GameObject > | **[Clicked](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#event-clicked)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[Click](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-click)**(GameObject gameObject) |
| virtual abstract void | **[SetDisabled](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-setdisabled)**() =0 |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual abstract void | **[ClickAction](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-clickaction)**(GameObject gameObject) =0 |

## Protected Attributes

|                | Name           |
| -------------- | -------------- |
| GameObject | **[_gameObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_gameobject)**  |
| Transform | **[_transform](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#variable-_transform)**  |

## Public Events Documentation

### event Clicked

```cpp
Action< GameObject > Clicked()
```


## Public Functions Documentation

### function Click

```cpp
inline void Click(
    GameObject gameObject
)
```


### function SetDisabled

```cpp
virtual abstract void SetDisabled() =0
```


**Reimplemented by**: [Graph3DVisualizer::GUI::Button3DComponnet::SetDisabled](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md#function-setdisabled), [Graph3DVisualizer::GUI::PopUpVerticalStackMenu::SetDisabled](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-setdisabled)


## Protected Functions Documentation

### function ClickAction

```cpp
virtual abstract void ClickAction(
    GameObject gameObject
) =0
```


**Reimplemented by**: [Graph3DVisualizer::Graph3D::VertexLinksMenu::ClickAction](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_links_menu.md#function-clickaction), [Graph3DVisualizer::GUI::Button3DComponnet::ClickAction](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md#function-clickaction), [Graph3DVisualizer::GUI::PopUpVerticalStackMenu::ClickAction](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md#function-clickaction)


## Protected Attributes Documentation

### variable _gameObject

```cpp
GameObject _gameObject;
```


### variable _transform

```cpp
Transform _transform;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)