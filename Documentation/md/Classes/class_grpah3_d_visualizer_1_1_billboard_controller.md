---
title: Grpah3DVisualizer::BillboardController
summary: A collection containing Billboards.  

---

# Grpah3DVisualizer::BillboardController




A collection containing [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md)s. 



Inherits from MonoBehaviour, [SupportComponents.IVisibile](Classes/interface_support_components_1_1_i_visibile.md)











## Public Events

|                | Name           |
| -------------- | -------------- |
| Action< bool, UnityEngine.Object > | **[VisibleChanged](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#event-visiblechanged)**()  |


## Public Functions

|                | Name           |
| -------------- | -------------- |
| [BillboardId](Classes/class_grpah3_d_visualizer_1_1_billboard_id.md) | **[CreateBillboard](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-createbillboard)**(in [BillboardParameters](Classes/class_grpah3_d_visualizer_1_1_billboard_parameters.md) parameters, string name, string description)  |
| [Billboard](Classes/class_grpah3_d_visualizer_1_1_billboard.md) | **[GetBillboard](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-getbillboard)**([BillboardId](Classes/class_grpah3_d_visualizer_1_1_billboard_id.md) billboardId)  |
| void | **[DeleteBillboard](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-deletebillboard)**([BillboardId](Classes/class_grpah3_d_visualizer_1_1_billboard_id.md) billboardId)  |
| void | **[UpdateBounds](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-updatebounds)**()  |
| void | **[DisableBillboard](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-disablebillboard)**([BillboardId](Classes/class_grpah3_d_visualizer_1_1_billboard_id.md) billboardId)  |
| void | **[EnableBillboard](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#function-enablebillboard)**([BillboardId](Classes/class_grpah3_d_visualizer_1_1_billboard_id.md) billboardId)  |


## Public Properties

|                | Name           |
| -------------- | -------------- |
| bool? | **[Visibility](Classes/class_grpah3_d_visualizer_1_1_billboard_controller.md#property-visibility)**  |

















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





























### function GetBillboard

```cpp
Billboard GetBillboard(
    BillboardId billboardId
)
```





























### function DeleteBillboard

```cpp
inline void DeleteBillboard(
    BillboardId billboardId
)
```





























### function UpdateBounds

```cpp
inline void UpdateBounds()
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































## Public Property Documentation

### property Visibility

```cpp
bool? Visibility;
```



































-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)