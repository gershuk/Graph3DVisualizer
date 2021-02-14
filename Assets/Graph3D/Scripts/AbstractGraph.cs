// This file is part of Grpah3DVisualizer.
// Copyright © Gershuk Vladislav 2021.
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

using Grpah3DVisualizer.Customizable;

using UnityEngine;

namespace Grpah3DVisualizer.Graph3D
{
    public class GraphParameters : CustomizableParameter
    {
        public (Type vertexType, VertexParameters[] vertexParameters)[] VertexParameters { get; }

        public GraphParameters ((Type vertexType, VertexParameters[] vertexParameters)[] vertexParameters)
        {
            VertexParameters = vertexParameters ?? throw new ArgumentNullException(nameof(vertexParameters));

            foreach (var (vertexType, _) in VertexParameters)
            {
                if (!vertexType.IsSubclassOf(typeof(AbstractVertex)))
                    throw new WrongTypeInCustomizableParameterException(typeof(AbstractVertex), vertexType);
            }
        }
    }

    public abstract class AbstractGraph : MonoBehaviour, ICustomizable<GraphParameters>
    {
        [SerializeField]
        protected GameObject _vertexPrefab;
        protected Transform _transform;

        public abstract int VertexesCount { get; }

        public abstract bool ContainsVertex (Vertex vertex);
        public abstract bool DeleteVeretex (Vertex vertex);
        public abstract IReadOnlyList<AbstractVertex> GetVertexes ();

        public abstract TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters)
            where TVertex : AbstractVertex, new()
            where TParams : VertexParameters;

        public abstract AbstractVertex SpawnVertex (Type vertexType, VertexParameters parameters);

        //ToDo : Add vertex links to parameters
        public void SetupParams (GraphParameters parameters)
        {
            foreach (var (vertexType, vertexParameters) in parameters.VertexParameters)
            {
                foreach (var vertexParameter in vertexParameters)
                    SpawnVertex(vertexType, vertexParameter);
            }
        }

        public GraphParameters DownloadParams () =>
            new GraphParameters(GetVertexes().Select(x => (x.GetType(), CustomizableExtension.CallDownloadParams<VertexParameters>(x).ToArray())).ToArray());
    }
}