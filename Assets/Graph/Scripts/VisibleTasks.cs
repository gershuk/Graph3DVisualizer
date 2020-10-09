using System.Collections.Generic;

using UnityEngine;

namespace Grpah3DVisualser
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
    }

    public abstract class VisualTask : MonoBehaviour
    {
        public abstract Graph CreateGraph ();
        public abstract Verdict CheckVertex (Vertex vertex);
        public abstract Verdict CheckEdge (Edge edge);
        public abstract List<Verdict> CheckGraph (Graph graph);
    }
}
