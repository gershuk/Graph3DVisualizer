---
title: Graph3DVisualizer::GUI::Button3DComponnet
summary: A component that simulates a 3d button. 

---

# Graph3DVisualizer::GUI::Button3DComponnet



A component that simulates a 3d button. Inherits from [Graph3DVisualizer.SupportComponents.AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[SetDisabled](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md#function-setdisabled)**() |

## Protected Functions

|                | Name           |
| -------------- | -------------- |
| virtual override void | **[ClickAction](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md#function-clickaction)**(GameObject gameObject) |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| Action< GameObject > | **[Action](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md#property-action)**  |

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


## Public Functions Documentation

### function SetDisabled

```cpp
inline virtual override void SetDisabled()
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::AbstractClickableObject::SetDisabled](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-setdisabled)


## Protected Functions Documentation

### function ClickAction

```cpp
virtual override void ClickAction(
    GameObject gameObject
)
```


**Reimplements**: [Graph3DVisualizer::SupportComponents::AbstractClickableObject::ClickAction](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md#function-clickaction)


## Public Property Documentation

### property Action

```cpp
Action< GameObject > Action;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)