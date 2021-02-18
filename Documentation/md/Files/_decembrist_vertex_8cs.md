---
title: Assets/Decembrists/Scripts/DecembristVertex.cs

---

# Assets/Decembrists/Scripts/DecembristVertex.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::GraphTasks](Namespaces/namespace_graph3_d_visualizer_1_1_graph_tasks.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::GraphTasks::DecembristVertex](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_decembrist_vertex.md)**  |




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

using Graph3DVisualizer.Graph3D;

namespace Graph3DVisualizer.GraphTasks
{
    public class DecembristVertex : SelectableVertex
    {
        public bool IsDec { get; set; } = false;
        public string Name { get; set; }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (зима)
