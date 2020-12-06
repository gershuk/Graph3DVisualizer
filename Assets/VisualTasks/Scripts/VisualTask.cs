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
using System.Collections.ObjectModel;

using Grpah3DVisualizer;

using PlayerInputControls;

using UnityEngine;

namespace GraphTasks
{
    public enum VerdictStatus
    {
        Correct = 0,
        Incorrect = 1,
        Undefined = 2,
    }

    public class Verdict
    {
        public string Description { get; set; }
        public VerdictStatus Status { get; set; }

        public Verdict (string description, VerdictStatus status) => (Description, Status) = (description, status);

        public override string ToString () => $"{Description} Status:{Status}";
    }

    public abstract class VisualTask : MonoBehaviour
    {
        public abstract IReadOnlyCollection<AbstractPLayer> Players { get; protected set; }
        public abstract IReadOnlyCollection<Graph> Graphs { get; protected set; }
        public abstract Graph CreateGraph ();
        public abstract void InitTask ();
        public abstract void StartTask ();
        public abstract void StopTask ();
        public abstract void DestroyTask ();
        public abstract List<Verdict> GetResult ();
    }
}
