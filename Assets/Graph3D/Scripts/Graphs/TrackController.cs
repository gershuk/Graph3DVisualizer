// This file is part of Graph3DVisualizer.
// Copyright © Gershuk Vladislav 2022.
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

using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

#nullable enable

namespace Graph3DVisualizer.SceneController
{
    public class TrackController : AbstractGraph
    {
        private const string _ringPath = @"Prefabs\Ring";
        private static GameObject _ringPrefab;
        private MovementComponent _movementComponent;

        [SerializeField]
        private Vector3[] _positions;

        private TrackRing[]? _rings;

        [SerializeField]
        private Vector3 _scale = Vector3.one;

        public int CurrentIndex { get; protected set; }

        public override MovementComponent MovementComponent
        {
            get => _movementComponent;
            protected set => _movementComponent = value;
        }

        public override string? Name
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public Vector3[] Positions
        {
            get => _positions;
            set => _positions = value;
        }

        public Vector3 Scale
        {
            get => _scale;
            set => _scale = value;
        }

        public override int VertexesCount => throw new System.NotImplementedException();

        private void Awake ()
        {
            _movementComponent = GetComponent<MovementComponent>();
            _transform = GetComponent<Transform>();
            _ringPrefab ??= Resources.Load<GameObject>(_ringPath);
            BuildTrack();
        }

        private void BuildTrack ()
        {
            if (_positions == null || _positions.Length == 0)
                return;

            CurrentIndex = 0;
            _rings = new TrackRing[_positions.Length];
            for (var i = 0; i < Positions.Length; i++)
            {
                var position = Positions[i];
                _rings[i] = Instantiate(_ringPrefab, position, Quaternion.identity, _transform).GetComponent<TrackRing>();

                _rings[i].Scale = Scale;
                if (i > 0)
                {
                    _rings[i].GetComponent<TrackRing>().Color = Color.yellow;
                    _rings[i].transform.LookAt(_rings[i - 1].transform);
                    _rings[i - 1].Link<StretchableEdge, StretchableEdgeParameters>(_rings[i],
                                                                                   new(new(Color.green, true), new SpringParameters(1, 5)));
                }
                else
                {
                    _rings[i].GetComponent<TrackRing>().Color = Color.green;
                }

                _rings[i].GetComponent<TrackRing>().Event += TrackController_Event;
            }
        }

        private void ClearAll ()
        {
            if (_rings == null)
                return;
            foreach (var ring in _rings)
                Destroy(ring);
            _rings = null;
        }

        private void TrackController_Event (TrackRing trackRing, Collider collider)
        {
            var currentRing = _rings[CurrentIndex].GetComponent<TrackRing>();
            if (trackRing != currentRing)
                return;

            if (CurrentIndex + 1 < _rings.Length)
                _rings[CurrentIndex + 1].GetComponent<TrackRing>().Color = Color.green;
            currentRing.Visibility = false;
            CurrentIndex++;
        }

        public override bool ContainsVertex (string id) => throw new System.NotImplementedException();

        public override bool DeleteVeretex (string id) => throw new System.NotImplementedException();

        public override AbstractVertex GetVertexById (string id) => throw new System.NotImplementedException();

        public override IReadOnlyList<AbstractVertex> GetVertexes () => throw new System.NotImplementedException();

        public override TVertex SpawnVertex<TVertex, TParams> (TParams vertexParameters) => throw new System.NotImplementedException();

        public override AbstractVertex SpawnVertex (System.Type vertexType, AbstractVertexParameters parameters) =>
            throw new System.NotImplementedException();

        [ContextMenu("UpdateTrack")]
        public void UpdateTrack ()
        {
            ClearAll();
            BuildTrack();
        }
    }
}