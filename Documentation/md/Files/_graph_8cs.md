---
title: Assets/Graph3D/Scripts/Graph.cs

---

# Assets/Graph3D/Scripts/Graph.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::Graph3D](Namespaces/namespace_graph3_d_visualizer_1_1_graph3_d.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| class | **[Graph3DVisualizer::Graph3D::Graph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_graph.md)** <br>Simple realization of [AbstractGraph](Classes/class_graph3_d_visualizer_1_1_graph3_d_1_1_abstract_graph.md).  |




## Source code

```cpp
// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
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
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;

using UnityEngine;

namespace Graph3DVisualizer.Graph3D
{
    public class Graph : AbstractGraph
    {
        private Dictionary<string, AbstractVertex> _vertexes;

        public override int VertexesCount => _vertexes.Count;

        private void Awake ()
        {
            _vertexPrefab = _vertexPrefab == null ? Resources.Load<GameObject>("Prefabs/Vertex") : _vertexPrefab;
            _transform = GetComponent<Transform>();
            _vertexes = new Dictionary<string, AbstractVertex>();
        }

        public override bool ContainsVertex (string id) => _vertexes.ContainsKey(id);

        public override bool DeleteVeretex (string id)
        {
            var result = _vertexes.Remove(id);
            if (result)
                Destroy(GetVertexById(id).gameObject);
            return result;
        }

        public override AbstractVertex GetVertexById (string id) => _vertexes[id];

        public override IReadOnlyList<AbstractVertex> GetVertexes () => _vertexes.Values.ToArray();

        public override TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters)
        {
            var vertex = Instantiate(_vertexPrefab, vertexParameters.Position, vertexParameters.Rotation, _transform);
            var vertexComponent = vertex.gameObject.AddComponent<TVertex>();
            (vertexComponent as ICustomizable<TParams>).SetupParams(vertexParameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }

        public override AbstractVertex SpawnVertex (Type vertexType, VertexParameters parameters)
        {
            if (!vertexType.IsSubclassOf(typeof(AbstractVertex)))
                throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);

            var vertex = Instantiate(_vertexPrefab, parameters.Position, parameters.Rotation, _transform);
            var vertexComponent = (AbstractVertex) vertex.gameObject.AddComponent(vertexType);
            CustomizableExtension.CallSetUpParams(vertexComponent, parameters);
            _vertexes.Add(vertexComponent.Id, vertexComponent);
            return vertexComponent;
        }
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (����)
