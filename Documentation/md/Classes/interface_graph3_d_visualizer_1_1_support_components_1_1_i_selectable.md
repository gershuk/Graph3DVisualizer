---
title: Graph3DVisualizer::SupportComponents::ISelectable
summary: Interface that allows you to select and highlight objects. It can report changes selected/highlighted state of the object. 

---

# Graph3DVisualizer::SupportComponents::ISelectable



Interface that allows you to select and highlight objects. It can report changes selected/highlighted state of the object. Inherited by [Graph3DVisualizer.Graph3D.SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< UnityEngine.Object, bool > | **[HighlightedChanged](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md#event-highlightedchanged)**() |
| Action< UnityEngine.Object, bool > | **[SelectedChanged](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md#event-selectedchanged)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool | **[IsHighlighted](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md#property-ishighlighted)**  |
| bool | **[IsSelected](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md#property-isselected)**  |
| Color | **[SelectFrameColor](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md#property-selectframecolor)**  |

## Public Events Documentation

### event HighlightedChanged

```cpp
Action< UnityEngine.Object, bool > HighlightedChanged()
```


### event SelectedChanged

```cpp
Action< UnityEngine.Object, bool > SelectedChanged()
```


## Public Property Documentation

### property IsHighlighted

```cpp
bool IsHighlighted;
```


### property IsSelected

```cpp
bool IsSelected;
```


### property SelectFrameColor

```cpp
Color SelectFrameColor;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)