---
title: Assets/SupportComponents/Scripts/LookAtObject.cs

---

# Assets/SupportComponents/Scripts/LookAtObject.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::SupportComponents](Namespaces/namespace_graph3_d_visualizer_1_1_support_components.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::SupportComponents::LookAtObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_look_at_object.md)** <br>Simple implementation of the target tracking component.  |




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

using UnityEngine;

namespace Graph3DVisualizer.SupportComponents
{
    public class LookAtObject : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _vectorUp = Vector3.up;

        public Transform TargetObject { get; set; }
        public Vector3 VectorWorldUp { get => _vectorUp; set => _vectorUp = value; }

        private void LateUpdate ()
        {
            if (TargetObject)
                transform.LookAt(TargetObject, VectorWorldUp);
        }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
