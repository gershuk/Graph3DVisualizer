---
title: Assets/GUIFactory/Scripts/PopUpVerticalStackMenu.cs

---

# Assets/GUIFactory/Scripts/PopUpVerticalStackMenu.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::GUI](Namespaces/namespace_graph3_d_visualizer_1_1_g_u_i.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::GUI::PopUpVerticalStackMenu](Classes/class_graph3_d_visualizer_1_1_g_u_i_1_1_pop_up_vertical_stack_menu.md)** <br>A component that simulates a 3d popup stack menu.  |




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

using System.Collections.Generic;

using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.GUI
{
    public class PopUpVerticalStackMenu : AbstractClickableObject
    {
        protected GameObject _plane;
        protected List<(float offset, Transform transform)> _subObjects;
        public float PlainOffset { get; set; } = 12;

        private void Awake ()
        {
            _transform = transform;
            _gameObject = gameObject;
            _subObjects = new List<(float offset, Transform transform)>();
        }

        protected override void ClickAction (GameObject gameObject) => _plane.SetActive(true);

        public override void SetDisabled () => _plane.SetActive(false);

        public void SetSubObjectList (IReadOnlyList<(float offset, Transform transform)> subObjects, Transform source)
        {
            if (_plane)
                Destroy(_plane);
            _subObjects.Clear();

            _plane = new GameObject("Plane");
            _plane.transform.parent = _transform;
            _plane.transform.localPosition = new Vector3(0, PlainOffset, 0);
            var lookAtScript = _plane.AddComponent<LookAtObject>();
            lookAtScript.TargetObject = source;
            lookAtScript.VectorWorldUp = Vector3.up;
            var incOffset = 0f;
            foreach (var (offset, transform) in subObjects)
            {
                transform.parent = _plane.transform;
                incOffset += offset;
                transform.localPosition = new Vector3(0, incOffset, 0);
            }

            _subObjects.AddRange(subObjects);
        }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
