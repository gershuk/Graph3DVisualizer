---
title: Graph3DVisualizer::GUI::GUIFactory
summary: Class that encapsulates UnityEngine.UI object creation functions. 

---

# Graph3DVisualizer::GUI::GUIFactory



Class that encapsulates UnityEngine.UI object creation functions. ## Public Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[ButtonParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_button_parameters.md)**  |
| struct | **[RectTransformParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_rect_transform_parameters.md)**  |
| struct | **[TextParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_text_parameters.md)**  |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| GameObject | **[CreateButton](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory.md#function-createbutton)**(in [ButtonParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_button_parameters.md) parameters) |
| GameObject | **[CreateText](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory.md#function-createtext)**(in [TextParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_text_parameters.md) parameters) |
| void | **[SetUpRectTransform](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory.md#function-setuprecttransform)**(this RectTransform rectTransform, in [RectTransformParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_rect_transform_parameters.md) parameters) |

## Public Functions Documentation

### function CreateButton

```cpp
static inline GameObject CreateButton(
    in ButtonParameters parameters
)
```


### function CreateText

```cpp
static inline GameObject CreateText(
    in TextParameters parameters
)
```


### function SetUpRectTransform

```cpp
static inline void SetUpRectTransform(
    this RectTransform rectTransform,
    in RectTransformParameters parameters
)
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)