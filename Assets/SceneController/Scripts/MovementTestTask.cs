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

using Graph3DVisualizer.PlayerInputControls;
using Graph3DVisualizer.SupportComponents;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class MovementTestTask : VisualTaskController
    {
        public override List<Verdict> GetResult ()
        {
            var track = (TrackController) Graphs[0];
            var verdicts = new List<Verdict>
            {
                new Verdict($"Track passed {track.CurrentIndex}/{track.Positions.Length}", track.CurrentIndex == track.Positions.Length ? VerdictStatus.Correct : VerdictStatus.Incorrect)
            };

            return verdicts;
        }

        public override void InitTask ()
        {
            var track = new GameObject("Track").AddComponent<TrackController>();
            track.Positions = new[] { new Vector3(0, 0, 0), new Vector3(0, 0, 10), new Vector3(0, 20, 20), new Vector3(-10, 30, 30), new Vector3(-10, 60, 30), new Vector3(0, 40, 35) };
            track.Scale = Vector3.one * 2;
            track.UpdateTrack();
            Graphs.Add(track);
            var player = CreatePlayer();
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -10), isVR: true));
        }
    }
}