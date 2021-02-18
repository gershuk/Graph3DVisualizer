---
title: Classes

---

# Classes




* **namespace [Graph3D](Namespaces/namespace_graph3_d.md)** 
    * **namespace [SurrogateTypesForSerialization](Namespaces/namespace_graph3_d_1_1_surrogate_types_for_serialization.md)** 
        * **struct [JsonColor](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_color.md)** 
        * **struct [JsonQuaternion](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_quaternion.md)** 
        * **struct [JsonVector2](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector2.md)** 
        * **struct [JsonVector2Int](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector2_int.md)** 
        * **struct [JsonVector3](Classes/struct_graph3_d_1_1_surrogate_types_for_serialization_1_1_json_vector3.md)** 
        * **class [NewtonsoftSurrogateConverter](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_newtonsoft_surrogate_converter.md)** 
        * **class [SurrogateColor](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_color.md)** 
        * **class [SurrogateQuaternion](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_quaternion.md)** 
        * **class [SurrogateTexture2D](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_texture2_d.md)** 
        * **class [SurrogateVector2](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector2.md)** 
        * **class [SurrogateVector2Int](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector2_int.md)** 
        * **class [SurrogateVector3](Classes/class_graph3_d_1_1_surrogate_types_for_serialization_1_1_surrogate_vector3.md)** 
* **namespace [Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)** 
    * **namespace [Billboards](Namespaces/namespace_graph3_d_visualizer_1_1_billboards.md)** 
        * **class [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md)** <br>An object containing information for displaying the [Billboard]() image. Contained in the [BillboardController](). 
        * **class [BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md)** <br>A collection containing [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md)s. 
        * **class [BillboardId](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_id.md)** <br>Id to search for a billboard in [BillboardController](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_controller.md). 
        * **class [BillboardParameters](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard_parameters.md)** <br>Parameters for creating a new [Billboard](Classes/class_graph3_d_visualizer_1_1_billboards_1_1_billboard.md) object. 
    * **namespace [Customizable](Namespaces/namespace_graph3_d_visualizer_1_1_customizable.md)** 
        * **class [AbstractCustomizableParameter](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_abstract_customizable_parameter.md)** <br>Abstract class for setup and download object parameters. 
        * **class [CustomizableExtension](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_extension.md)** <br>A class containing functions for dynamically calling methods ICustomizable<TParams>.DownloadParams, ICustomizable<TParams>.SetupParams(TParams). 
        * **class [CustomizableGrandTypeAttribute](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_customizable_grand_type_attribute.md)** <br>An attribute that specifies which type of parameters to use for CustomizableExtension.CallDownloadParams(object), CustomizableExtension.CallSetUpParams(object, object). 
        * **interface [ICustomizable](Classes/interface_graph3_d_visualizer_1_1_customizable_1_1_i_customizable.md)** <br>Interface for setup and download object parameters. 
        * **class [WrongTypeInCustomizableParameterException](Classes/class_graph3_d_visualizer_1_1_customizable_1_1_wrong_type_in_customizable_parameter_exception.md)** 
    * **namespace [GUI](Namespaces/namespace_graph3_d_visualizer_1_1_g_u_i.md)** 
        * **class [Button3DComponnet](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md)** <br>A component that simulates a 3d button. 
        * **class [GUIFactory](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory.md)** <br>Class that encapsulates UnityEngine.UI object creation functions. 
            * **struct [ButtonParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_button_parameters.md)** 
            * **struct [RectTransformParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_rect_transform_parameters.md)** 
            * **struct [TextParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory_1_1_text_parameters.md)** 
        * **class [GUIFactory3D](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory3_d.md)** <br>Class that encapsulates 3D UI object creation functions. 
            * **struct [TextMeshParameters](Classes/struct_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory3_d_1_1_text_mesh_parameters.md)** 
        * **class [PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)** <br>A component that simulates a 3d popup stack menu. 
    * **namespace [Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md)** 
        * **class [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md)** <br>Abstarct class for describing visual part of the graph edge. 
        * **class [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md)** <br>Abstract class that describes graph component. 
        * **class [AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)** <br>Base class for all [Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md) objects. 
        * **class [AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)** <br>Class that describes common [AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md) parameters for ICustomizable<TParams>. 
        * **class [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md)** <br>Abstract class that describes vertex component. 
        * **struct [AdjacentVertices](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_adjacent_vertices.md)** <br>A class for describing adjacent vertexes. 
        * **class [Edge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge.md)** <br>Simple realization of [AbstractEdge](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_edge.md). 
        * **class [EdgeParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_edge_parameters.md)** <br>A class that describes default edge parameters for ICustomizable<TParams>. 
        * **class [Graph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md)** <br>Simple realization of [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md). 
        * **class [GraphParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph_parameters.md)** <br>Class that describes default graph parameters for ICustomizable<TParams>. 
        * **class [Link](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link.md)** <br>Abstract class that describes unidirectional link between to current and adjacent vertexes. 
        * **struct [LinkInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_link_info.md)** <br>Support class for edge serialization. 
        * **class [LinkNotFoundException](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_link_not_found_exception.md)** 
        * **class [SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md)** <br>Realization of [Vertex]() with selection support. 
        * **class [SelectableVertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex_parameters.md)** <br>Class that describes [SelectableVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_selectable_vertex.md) parameters for ICustomizable<TParams>. 
        * **class [Vertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex.md)** <br>Simple realization of [AbstractVertex](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_vertex.md). 
        * **struct [VertexInfo](Classes/struct_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_info.md)** <br>Support class for vertex serialization. 
        * **class [VertexLinksMenu](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_links_menu.md)** <br>A class that is temporarily used to create an interactive vertex menu. 
        * **class [VertexParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_vertex_parameters.md)** <br>Class that describes default vertex parameters for ICustomizable<TParams>. 
    * **namespace [GraphTasks](Namespaces/namespace_graph3_d_visualizer_1_1_graph_tasks.md)** 
        * **class [AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md)** <br>Class that describes a task for working with a graph in 3d 
        * **class [DecembristVertex](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_decembrist_vertex.md)** 
        * **struct [GraphInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md)** <br>Support class for graph serialization. 
        * **class [HistoryTask1](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_history_task1.md)** 
        * **struct [PlayerInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md)** <br>Support class for player serialization. 
        * **class [SimpleTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_simple_task.md)** <br>Example of implementing a graph visual task. 
        * **class [Verdict](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_verdict.md)** <br>Class that describes the state of execution of some part of the task. 
        * **class [VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md)** <br>Class that describes [AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md) parameters for ICustomizable<TParams>. 
    * **namespace [PlayerInputControls](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md)** 
        * **class [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md)** <br>Abstract class that describes player. 
        * **class [AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)** <br>Abstract class that describes the player's tool for interacting with the world. 
        * **class [AbstractToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_tool_params.md)** <br>A class that describes default player's tool parameters for ICustomizable<TParams>. 
        * **class [ClickTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md)** <br>Tool for working with 3d menu components. 
        * **class [ClickToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool_params.md)** <br>Class that describes [ClickTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_click_tool.md) parameters for ICustomizable<TParams>. 
        * **class [EdgeCreaterTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md)** <br>Tool for creating links between vertexes. 
        * **class [EdgeCreaterToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool_params.md)** <br>Class that describes [EdgeCreaterTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_edge_creater_tool.md) parameters for ICustomizable<TParams>. 
        * **class [FlyControls](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls.md)** 
            * **struct [FlyModelActions](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_fly_model_actions.md)** 
            * **interface [IFlyModelActions](Classes/interface_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_controls_1_1_i_fly_model_actions.md)** 
        * **class [FlyPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_fly_player.md)** <br>Simple realization of [AbstractPlayer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player.md) for keyboard/mouse controls. 
        * **class [GrabItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md)** <br>Tool for dragging objects with MovementComponent. 
        * **class [GrabItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool_params.md)** <br>Class that describes [GrabItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_grab_item_tool.md) parameters for ICustomizable<TParams>. 
        * **class [LaserPointer](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_laser_pointer.md)** <br>A component that simulates a laser pointer. Simplifies the use of tools in VR. 
        * **class [PlayerParameters](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_player_parameters.md)** <br>A class that describes default player parameters for ICustomizable<TParams>. 
        * **class [SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md)** <br>The tool allows you to select objects with components that implement the ISelectable. 
        * **class [SelectItemToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool_params.md)** <br>Class that describes [SelectItemTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_select_item_tool.md) parameters for ICustomizable<TParams>. 
        * **struct [ToolConfig](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_tool_config.md)** <br>Сlass that describes the data needed when creating a new player tool. AbstractPlayer.GiveNewTool(ToolConfig[])
    * **namespace [Scene](Namespaces/namespace_graph3_d_visualizer_1_1_scene.md)** 
        * **class [Menu](Classes/class_graph3_d_visualizer_1_1_scene_1_1_menu.md)** <br>Component that creates a menu for working with [SceneController](). 
        * **class [SceneController](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md)** <br>Component that manages the loading/saving of AbstractVisualTask. 
        * **class [SceneControllerParameters](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller_parameters.md)** <br>Class that describes [SceneController](Classes/class_graph3_d_visualizer_1_1_scene_1_1_scene_controller.md) parameters for ICustomizable<TParams>. 
    * **namespace [SupportComponents](Namespaces/namespace_graph3_d_visualizer_1_1_support_components.md)** 
        * **class [AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)** <br>Component for interacting with an object using Physics.Raycast(Ray). 
        * **interface [IDestructible](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_destructible.md)** <br>Interface with notification of object destruction. 
        * **interface [IMoveable](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_moveable.md)** <br>Interface that controls the changing of Transform object. It can report changes coordinates of the object. 
        * **interface [ISelectable](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_selectable.md)** <br>Interface that allows you to select and highlight objects. It can report changes selected/highlighted state of the object. 
        * **interface [IVisibile](Classes/interface_graph3_d_visualizer_1_1_support_components_1_1_i_visibile.md)** <br>Interface that controls the visibility of an object. It can report changes visibility of the object. 
        * **class [LookAtObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_look_at_object.md)** <br>Simple implementation of the target tracking component. 
        * **class [MovementComponent](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_movement_component.md)** <br>Component that controls the movement of the object. 
    * **namespace [TextureFactory](Namespaces/namespace_graph3_d_visualizer_1_1_texture_factory.md)** 
        * **class [CombinedImages](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_combined_images.md)** <br>Class describing an image consisting of several pictures. 
        * **class [PositionedImage](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_positioned_image.md)** <br>Class that describes an image with a 2 dimensional coordinate reference. 
        * **class [TextTextureFactory](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_text_texture_factory.md)** <br>Fabricator that allows you to create text on a texture. 
        * **class [Texture2DExtension](Classes/class_graph3_d_visualizer_1_1_texture_factory_1_1_texture2_d_extension.md)** <br>Сlass containing functions for changing textures that are not included in Unity3D engine. 
* **namespace [Graph3DVisualizer::GUI::GUIFactory](Namespaces/namespace_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory.md)** 
* **namespace [Graph3DVisualizer::GUI::GUIFactory3D](Namespaces/namespace_graph3_d_visualizer_1_1_g_u_i_1_1_g_u_i_factory3_d.md)** 
* **namespace [Newtonsoft::Json](Namespaces/namespace_newtonsoft_1_1_json.md)** 
* **namespace [Newtonsoft::Json::Linq](Namespaces/namespace_newtonsoft_1_1_json_1_1_linq.md)** 
* **namespace [System](Namespaces/namespace_system.md)** 
* **namespace [System::Collections](Namespaces/namespace_system_1_1_collections.md)** 
* **namespace [System::Collections::Generic](Namespaces/namespace_system_1_1_collections_1_1_generic.md)** 
* **namespace [System::IO](Namespaces/namespace_system_1_1_i_o.md)** 
* **namespace [System::Linq](Namespaces/namespace_system_1_1_linq.md)** 
* **namespace [System::Reflection](Namespaces/namespace_system_1_1_reflection.md)** 
* **namespace [System::Runtime::Serialization](Namespaces/namespace_system_1_1_runtime_1_1_serialization.md)** 
* **namespace [System::Runtime::Serialization::Formatters::Binary](Namespaces/namespace_system_1_1_runtime_1_1_serialization_1_1_formatters_1_1_binary.md)** 
* **namespace [UnityEngine](Namespaces/namespace_unity_engine.md)** 
* **namespace [UnityEngine::Events](Namespaces/namespace_unity_engine_1_1_events.md)** 
* **namespace [UnityEngine::InputSystem](Namespaces/namespace_unity_engine_1_1_input_system.md)** 
* **namespace [UnityEngine::InputSystem::Utilities](Namespaces/namespace_unity_engine_1_1_input_system_1_1_utilities.md)** 
* **namespace [UnityEngine::Physics](Namespaces/namespace_unity_engine_1_1_physics.md)** 
* **namespace [UnityEngine::UI](Namespaces/namespace_unity_engine_1_1_u_i.md)** 
* **namespace [static](Namespaces/namespacestatic.md)** 



-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (����)
