---
title: Assets/SupportComponents/Scripts/AbstractClickableObject.cs

---

# Assets/SupportComponents/Scripts/AbstractClickableObject.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::SupportComponents](Namespaces/namespace_graph3_d_visualizer_1_1_support_components.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::SupportComponents::AbstractClickableObject](Classes/class_graph3_d_visualizer_1_1_support_components_1_1_abstract_clickable_object.md)** <br>Component for interacting with an object using Physics.Raycast(Ray).  |




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

using UnityEngine;

namespace Graph3DVisualizer.SupportComponents
{
    public abstract class AbstractClickableObject : MonoBehaviour
    {
        protected GameObject _gameObject;
        protected Transform _transform;

        public event Action<GameObject> Clicked;

        protected abstract void ClickAction (GameObject gameObject);

        public void Click (GameObject gameObject)
        {
            ClickAction(gameObject);
            Clicked?.Invoke(gameObject);
        }

        public abstract void SetDisabled ();
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
