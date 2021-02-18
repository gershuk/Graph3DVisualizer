---
title: Assets/Graph3D/Scripts/GraphObject.cs

---

# Assets/Graph3D/Scripts/GraphObject.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::Graph3D::AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md)** <br>Base class for all [Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md) objects.  |
| class | **[Graph3DVisualizer::Graph3D::AbstractGraphObjectParameters](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object_parameters.md)** <br>Class that describes common [AbstractGraphObject](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph_object.md) parameters for ICustomizable<TParams>.  |




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

using Graph3DVisualizer.Customizable;

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    public abstract class AbstractGraphObject : MonoBehaviour
    {
        //attribute for debug only
        [SerializeField]
        private string _id;

        public string Id { get => _id; protected set => _id = value ?? Guid.NewGuid().ToString(); }
    }

    [Serializable]
    public abstract class AbstractGraphObjectParameters : AbstractCustomizableParameter
    {
        public string Id { get; protected set; }

        public AbstractGraphObjectParameters (string id) => Id = id;
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
