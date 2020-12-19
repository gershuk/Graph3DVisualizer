// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2020.
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
using System.Collections.Generic;
using System.Linq;

using SupportComponents;

using UnityEngine;

namespace Grpah3DVisualizer
{
    public class Graph : AbstractGraph
    {
        private HashSet<AbstractVertex> _vertexes;

        private void Awake ()
        {
            _vertexPrefab = _vertexPrefab == null ? Resources.Load<GameObject>("Prefabs/Vertex") : _vertexPrefab;
            _transform = GetComponent<Transform>();
            _vertexes = new HashSet<AbstractVertex>();
        }

        public override TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters)
        {
            var vertex = Instantiate(_vertexPrefab, vertexParameters.Position, vertexParameters.Rotation, _transform);
            var vertexComponent = vertex.gameObject.AddComponent<TVertex>();
            (vertexComponent as ICustomizable<TParams>).SetupParams(vertexParameters);
            _vertexes.Add(vertexComponent);
            return vertexComponent;
        }

        public override AbstractVertex SpawnVertex (Type vertexType, VertexParameters parameters)
        {
            if (vertexType.IsSubclassOf(typeof(AbstractVertex)))
                throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);

            var vertex = Instantiate(_vertexPrefab, parameters.Position, parameters.Rotation, _transform);
            var vertexComponent = (AbstractVertex) vertex.gameObject.AddComponent(vertexType);
            CustomizableExtension.CallSetUpParams(vertexComponent, new[] { parameters });
            _vertexes.Add(vertexComponent);
            return vertexComponent;
        }

        public override bool ContainsVertex (Vertex vertex) => _vertexes.Contains(vertex);

        public override bool DeleteVeretex (Vertex vertex)
        {
            var result = _vertexes.Remove(vertex);
            if (result)
                Destroy(vertex.gameObject);
            return result;
        }

        public override int VertexesCount => _vertexes.Count;

        public override IReadOnlyList<AbstractVertex> GetVertexes () => _vertexes.ToList();
    }
}
