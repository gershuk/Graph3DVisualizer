---
title: Assets/Player/Scripts/FlyPlayer/AbstractPLayer.cs


---

# Assets/Player/Scripts/FlyPlayer/AbstractPLayer.cs







## Namespaces

| Name           |
| -------------- |
| **[PlayerInputControls](Namespaces/namespace_player_input_controls.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[PlayerInputControls::PlayerParams](Classes/class_player_input_controls_1_1_player_params.md)**  |
| class | **[PlayerInputControls::AbstractPLayer](Classes/class_player_input_controls_1_1_abstract_p_layer.md)**  |
















## Source code

```cpp
// This file is part of Grpah3DVisualizer.
// Copyright В© Gershuk Vladislav 2020.
//
// Grpah3DVisualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Grpah3DVisualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Grpah3DVisualizer.  If not, see <https://www.gnu.org/licenses/>.

using System;

using UnityEngine;

namespace PlayerInputControls
{
    public enum InputType
    {
        Off = 0,
        MenuOnly = 1,
        ToolsOnly = 2,
        All = 3,
    }

    public class PlayerParams
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        public float MovingSpeed { get; }
        public float RotationSpeed { get; }
        public ToolConfig[] ToolConfigs { get; }

        public PlayerParams (Vector3 position, Vector3 eulerAngles, float movingSpeed, float rotationSpeed, ToolConfig[] toolConfigs)
        {
            Position = position;
            EulerAngles = eulerAngles;
            RotationSpeed = rotationSpeed;
            MovingSpeed = movingSpeed;
            ToolConfigs = toolConfigs ?? throw new ArgumentNullException(nameof(toolConfigs));
        }
    }

    public abstract class AbstractPLayer : MonoBehaviour
    {
        protected InputType _inputType;
        public abstract InputType InputType { get; set; }
    }
}
```


-------------------------------

Updated on 12 December 2020 at 00:14:19 RTZ 9 (зима)
