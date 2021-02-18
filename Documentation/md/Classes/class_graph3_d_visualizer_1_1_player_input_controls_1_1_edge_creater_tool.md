---
title: Graph3DVisualizer::PlayerInputControls::EdgeCreaterTool
summary: Tool for creating links between vertexes. 

---

# Graph3DVisualizer::PlayerInputControls::EdgeCreaterTool



Tool for creating links between vertexes. Inherits from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md), [Graph3DVisualizer.Customizable.ICustomizable< EdgeCreaterToolParams >](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md), MonoBehaviour

## Public Functions

|                | Name           |
| -------------- | -------------- |
| void | **[ChangeIndex](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-changeindex)**(int deltaIndex) |
| void | **[CreateEdge](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-createedge)**() |
| void | **[DeleteEdge](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-deleteedge)**() |
| [EdgeCreaterToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool_params.md) | **[DownloadParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-downloadparams)**() |
| virtual override void | **[RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-registerevents)**(IInputActionCollection inputActions) |
| void | **[SelectFirstPoint](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-selectfirstpoint)**() |
| void | **[SelectSecondPoint](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-selectsecondpoint)**() |
| void | **[SetupParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md#function-setupparams)**([EdgeCreaterToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool_params.md) parameters) |

## Additional inherited members

**Protected Functions inherited from [Graph3DVisualizer.PlayerInputControls.AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)**

|                | Name           |
| -------------- | -------------- |
| RaycastHit | **[RayCast](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-raycast)**(float range) |


## Public Functions Documentation

### function ChangeIndex

```cpp
void ChangeIndex(
    int deltaIndex
)
```


### function CreateEdge

```cpp
inline void CreateEdge()
```


### function DeleteEdge

```cpp
inline void DeleteEdge()
```


### function DownloadParams

```cpp
EdgeCreaterToolParams DownloadParams()
```


### function RegisterEvents

```cpp
inline virtual override void RegisterEvents(
    IInputActionCollection inputActions
)
```


**Reimplements**: [Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool::RegisterEvents](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md#function-registerevents)


### function SelectFirstPoint

```cpp
inline void SelectFirstPoint()
```


### function SelectSecondPoint

```cpp
inline void SelectSecondPoint()
```


### function SetupParams

```cpp
void SetupParams(
    EdgeCreaterToolParams parameters
)
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)