---
title: Assets/GUIFactory/Scripts/Button3DComponnet.cs

---

# Assets/GUIFactory/Scripts/Button3DComponnet.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::GUI](Namespaces/namespace_graph3_d_visualizer_1_1_g_u_i.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::GUI::Button3DComponnet](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_button3_d_componnet.md)** <br>A component that simulates a 3d button.  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright В© Gershuk Vladislav 2021.
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

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.GUI
{

    public class Button3DComponnet : AbstractClickableObject
    {
        //ToDo : change to expression
        public Action<GameObject> Action { get; set; }

        protected override void ClickAction (GameObject gameObject) => Action?.Invoke(gameObject);

        public override void SetDisabled ()
        {
        }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
