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

using System.Collections.Generic;

using Grpah3DVisualizer.GUI;
using Grpah3DVisualizer.SupportComponents;

using UnityEngine;

using static Grpah3DVisualizer.GUI.GUIFactory3D;

namespace Grpah3DVisualizer.Graph3D
{
    public class VertexLinksMenu : PopUpVerticalStackMenu
    {
        private AbstractVertex _vertex;

        private void Awake ()
        {
            _transform = transform;
            _gameObject = gameObject;
            _vertex = gameObject.GetComponent<AbstractVertex>();
            _subObjects = new List<(float offset, Transform transform)>();
        }

        protected override void ClickAction (GameObject callingObject)
        {
            if (_vertex != null)
            {
                var abstractVertices = new HashSet<AbstractVertex>();

                foreach (var link in _vertex.OutgoingLinks)
                    abstractVertices.Add(link.AdjacentVertex);
                foreach (var link in _vertex.IncomingLinks)
                    abstractVertices.Add(link.AdjacentVertex);

                var content = new List<(float offset, Transform transform)>(abstractVertices.Count);
                var isFirst = true;
                foreach (var abstractVertex in abstractVertices)
                {
                    var parameters = new TextMeshParameters(null, Vector3.zero, $"GoTo{abstractVertex.gameObject.name}", $"Go to { abstractVertex.gameObject.name }", 1,
                        Resources.GetBuiltinResource<Font>("Arial.ttf"), 18, 6, Color.black);

                    var button = CreateButton<Button3DComponnet>(parameters);
                    button.transform.eulerAngles = new Vector3(0, 180, 0);
                    button.GetComponent<Button3DComponnet>().Action = (GameObject target) =>
                    {
                        var targetPosition = target.transform.position;
                        var secondPoint = abstractVertex.transform.position;
                        var edgeDir = secondPoint - _transform.position;
                        var playerDir = _transform.position - targetPosition;
                        if (playerDir.magnitude < 0.01)
                            playerDir = Vector3.right;

                        var distLine = Vector3.Cross(playerDir, edgeDir).magnitude / edgeDir.magnitude;

                        var cross = Vector3.Cross(edgeDir, playerDir);
                        var offset = Vector3.Cross(cross, edgeDir);
                        offset = offset.normalized;

                        if (Vector3.Distance(_transform.position - offset, targetPosition) > Vector3.Distance(_transform.position + offset, targetPosition))
                            offset *= -1;
                        var offsetDist = 20;
                        var range = Mathf.Min(distLine - offsetDist, offsetDist);
                        var newPoint = target.transform.position + offset * range;
                        var firstPointOffset = _transform.position - offset * offsetDist;
                        var secondPointOffset = secondPoint - offset * offsetDist;

                        if (Vector3.Distance(secondPoint, newPoint) > Vector3.Distance(secondPoint, firstPointOffset))
                            newPoint = firstPointOffset;

                        var points = new List<Vector3>(2);
                        if (Vector3.Distance(secondPoint, targetPosition) > 20)
                            points.Add(newPoint);
                        points.Add(secondPointOffset);
                        StartCoroutine(target.GetComponent<MovementComponent>().MoveAlongTrajectory(points));
                        SetDisabled();
                    };

                    if (isFirst)
                    {
                        content.Add((0, button.transform));
                        isFirst = false;
                    }
                    else
                    {
                        content.Add((3, button.transform));
                    }
                }
                var infoParams = new TextMeshParameters(null, Vector3.zero, $"objectInfo", $"Info of {gameObject.name}", 1,
                        Resources.GetBuiltinResource<Font>("Arial.ttf"), 18, 6, Color.white);
                var text = CreateText(infoParams);
                text.transform.eulerAngles = new Vector3(0, 180, 0);
                content.Add((3, text.transform));
                SetSubObjectList(content, callingObject.transform);
                base.ClickAction(callingObject);
            }
        }
    }
}