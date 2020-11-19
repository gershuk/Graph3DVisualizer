using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Grpah3DVisualser;

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
        public abstract ReadOnlyCollection<FlyPlayer> Players { get; protected set; }
        public abstract ReadOnlyCollection<Graph> Graphs { get; protected set; }
        public abstract Graph CreateGraph ();
        public abstract void InitTask ();
        public abstract void StartTask ();
        public abstract void StopTask ();
        public abstract void DestroyTask ();
        public abstract List<Verdict> GetResult ();
    }
}
