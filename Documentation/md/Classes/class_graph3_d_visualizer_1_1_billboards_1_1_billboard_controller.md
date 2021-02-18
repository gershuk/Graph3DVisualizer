---
title: Graph3DVisualizer::Billboards::BillboardController
summary: A collection containing Billboards. 

---

# Graph3DVisualizer::Billboards::BillboardController



A collection containing [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md)s. Inherits from MonoBehaviour, [Graph3DVisualizer.SupportComponents.IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md)

## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#event-visiblechanged)**() |

## Public Functions

|                | Name           |
| -------------- | -------------- |
| [BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) | **[CreateBillboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-createbillboard)**(in [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md) parameters, string name, string description) |
| void | **[DeleteBillboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-deletebillboard)**([BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) billboardId) |
| void | **[DisableBillboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-disablebillboard)**([BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) billboardId) |
| void | **[EnableBillboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-enablebillboard)**([BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) billboardId) |
| [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md) | **[GetBillboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-getbillboard)**([BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md) billboardId) |
| void | **[UpdateBounds](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#function-updatebounds)**() |

## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool? | **[Visibility](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md#property-visibility)**  |

## Public Events Documentation

### event VisibleChanged

```cpp
Action< bool, UnityEngine.Object > VisibleChanged()
```


## Public Functions Documentation

### function CreateBillboard

```cpp
inline BillboardId CreateBillboard(
    in BillboardParameters parameters,
    string name,
    string description
)
```


### function DeleteBillboard

```cpp
inline void DeleteBillboard(
    BillboardId billboardId
)
```


### function DisableBillboard

```cpp
inline void DisableBillboard(
    BillboardId billboardId
)
```


### function EnableBillboard

```cpp
void EnableBillboard(
    BillboardId billboardId
)
```


### function GetBillboard

```cpp
Billboard GetBillboard(
    BillboardId billboardId
)
```


### function UpdateBounds

```cpp
inline void UpdateBounds()
```


## Public Property Documentation

### property Visibility

```cpp
bool? Visibility;
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)