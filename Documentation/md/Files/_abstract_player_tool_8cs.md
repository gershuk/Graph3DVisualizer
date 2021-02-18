---
title: Assets/Player/Scripts/PlayerTools/AbstractPlayerTool.cs

---

# Assets/Player/Scripts/PlayerTools/AbstractPlayerTool.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::PlayerInputControls](Namespaces/namespace_graph3_d_visualizer_1_1_player_input_controls.md)**  |
| **[UnityEngine::Physics](Namespaces/namespace_unity_engine_1_1_physics.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[Graph3DVisualizer::PlayerInputControls::ToolConfig](Classes/struct_graph3_d_visualizer_1_1_player_input_controls_1_1_tool_config.md)** <br>Ð¡lass that describes the data needed when creating a new player tool. AbstractPlayer.GiveNewTool(ToolConfig[]) |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractPlayerTool](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_player_tool.md)** <br>Abstract class that describes the player's tool for interacting with the world.  |
| class | **[Graph3DVisualizer::PlayerInputControls::AbstractToolParams](Classes/class_graph3_d_visualizer_1_1_player_input_controls_1_1_abstract_tool_params.md)** <br>A class that describes default player's tool parameters for ICustomizable<TParams>.  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright Â© Gershuk Vladislav 2021.
//
// Graph3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Graph3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Graph3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;

using Graph3DVisualizer.Customizable;

using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Physics;

namespace Graph3DVisualizer.PlayerInputControls
{
    [Serializable]
    public readonly struct ToolConfig
    {
        public AbstractToolParams ToolParams { get; }
        public Type ToolType { get; }

        public ToolConfig (Type toolType, AbstractToolParams toolParams)
        {
            if (!toolType.IsSubclassOf(typeof(AbstractPlayerTool)))
                throw new Exception($"{toolType} is not subclass of PlayerTool");
            ToolType = toolType ?? throw new ArgumentNullException(nameof(toolType));
            ToolParams = toolParams;
        }
    }

    public abstract class AbstractPlayerTool : MonoBehaviour
    {
        protected RaycastHit RayCast (float range)
        {
            Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, range);
            return hit;
        }

        public abstract void RegisterEvents (IInputActionCollection inputActions);
    }

    [Serializable]
    public abstract class AbstractToolParams : AbstractCustomizableParameter
    { }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (çèìà)
