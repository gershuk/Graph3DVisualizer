---
title: Assets/VisualTasks/Scripts/AbstractVisualTask.cs

---

# Assets/VisualTasks/Scripts/AbstractVisualTask.cs

## Namespaces

| Name           |
| -------------- |
| **[Graph3DVisualizer](Namespaces/namespace_graph3_d_visualizer.md)**  |
| **[Graph3DVisualizer::GraphTasks](Namespaces/namespace_graph3_d_visualizer_1_1_graph_tasks.md)**  |

## Classes

|                | Name           |
| -------------- | -------------- |
| struct | **[Graph3DVisualizer::GraphTasks::GraphInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_graph_info.md)** <br>Support class for graph serialization.  |
| struct | **[Graph3DVisualizer::GraphTasks::PlayerInfo](Classes/struct_graph3_d_visualizer_1_1_graph_tasks_1_1_player_info.md)** <br>Support class for player serialization.  |
| class | **[Graph3DVisualizer::GraphTasks::AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md)** <br>Class that describes a task for working with a graph in 3d  |
| class | **[Graph3DVisualizer::GraphTasks::Verdict](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_verdict.md)** <br>Class that describes the state of execution of some part of the task.  |
| class | **[Graph3DVisualizer::GraphTasks::VisualTaskParameters](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_visual_task_parameters.md)** <br>Class that describes [AbstractVisualTask](Classes/class_graph3_d_visualizer_1_1_graph_tasks_1_1_abstract_visual_task.md) parameters for ICustomizable<TParams>.  |




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
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;

using UnityEngine;

namespace Graph3DVisualizer.GraphTasks
{
    [Serializable]
    public struct GraphInfo
    {
        public GraphParameters graphParameters;
        public Type graphType;

        public GraphInfo (Type graphType, GraphParameters graphParameters)
        {
            this.graphType = graphType;
            this.graphParameters = graphParameters;
        }

        public static implicit operator (Type graphType, GraphParameters graphParameters) (GraphInfo value)
        {
            return (value.graphType, value.graphParameters);
        }

        public static implicit operator GraphInfo ((Type graphType, GraphParameters graphParameters) value)
        {
            return new GraphInfo(value.graphType, value.graphParameters);
        }

        public void Deconstruct (out Type graphType, out GraphParameters graphParameters)
        {
            graphType = this.graphType;
            graphParameters = this.graphParameters;
        }

        public override bool Equals (object obj)
        {
            return obj is GraphInfo other &&
                   EqualityComparer<Type>.Default.Equals(graphType, other.graphType) &&
                   EqualityComparer<GraphParameters>.Default.Equals(graphParameters, other.graphParameters);
        }

        public override int GetHashCode ()
        {
            var hashCode = 459771302;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(graphType);
            hashCode = hashCode * -1521134295 + EqualityComparer<GraphParameters>.Default.GetHashCode(graphParameters);
            return hashCode;
        }
    }

    [Serializable]
    public struct PlayerInfo
    {
        public PlayerParameters playerParameters;
        public Type playerType;

        public PlayerInfo (Type playerType, PlayerParameters playerParameters)
        {
            this.playerType = playerType;
            this.playerParameters = playerParameters;
        }

        public static implicit operator (Type playerType, PlayerParameters playerParameters) (PlayerInfo value)
        {
            return (value.playerType, value.playerParameters);
        }

        public static implicit operator PlayerInfo ((Type playerType, PlayerParameters playerParameters) value)
        {
            return new PlayerInfo(value.playerType, value.playerParameters);
        }

        public void Deconstruct (out Type playerType, out PlayerParameters playerParameters)
        {
            playerType = this.playerType;
            playerParameters = this.playerParameters;
        }

        public override bool Equals (object obj)
        {
            return obj is PlayerInfo other &&
                   EqualityComparer<Type>.Default.Equals(playerType, other.playerType) &&
                   EqualityComparer<PlayerParameters>.Default.Equals(playerParameters, other.playerParameters);
        }

        public override int GetHashCode ()
        {
            var hashCode = -640872516;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(playerType);
            hashCode = hashCode * -1521134295 + EqualityComparer<PlayerParameters>.Default.GetHashCode(playerParameters);
            return hashCode;
        }
    }

    [CustomizableGrandType(Type = typeof(VisualTaskParameters))]
    public abstract class AbstractVisualTask : MonoBehaviour, ICustomizable<VisualTaskParameters>
    {
        protected List<AbstractGraph> _graphs = new List<AbstractGraph>();
        protected List<AbstractPlayer> _players = new List<AbstractPlayer>();

        public abstract IReadOnlyCollection<AbstractGraph> Graphs { get; protected set; }
        public abstract IReadOnlyCollection<AbstractPlayer> Players { get; protected set; }

        protected virtual void OnTaskDestoryed ()
        {
        }

        public void DestroyTask ()
        {
            OnTaskDestoryed();
            foreach (var player in _players)
                Destroy(player.gameObject);
            foreach (var graph in _graphs)
                Destroy(graph.gameObject);
            Destroy(gameObject);
        }

        public VisualTaskParameters DownloadParams () =>
            new VisualTaskParameters
                (_players.Select(x => new PlayerInfo(x.GetType(), (PlayerParameters) CustomizableExtension.CallDownloadParams(x))).ToArray(),
                 _graphs.Select(x => new GraphInfo(x.GetType(), (GraphParameters) CustomizableExtension.CallDownloadParams(x))).ToArray());

        public abstract List<Verdict> GetResult ();

        public abstract void InitTask ();

        public void SetupParams (VisualTaskParameters parameters)
        {
            foreach (var (graphType, graphParameters) in parameters.GraphsParameters)
            {
                var graph = new GameObject("Graph").AddComponent(graphType);
                _graphs.Add((AbstractGraph) graph);
                CustomizableExtension.CallSetUpParams(graph, graphParameters);
            }

            foreach (var (playerType, playerParameters) in parameters.PlayersParameters)
            {
                var player = new GameObject("Player").AddComponent(playerType);
                _players.Add((AbstractPlayer) player);
                CustomizableExtension.CallSetUpParams(player, playerParameters);
            }
        }
    }


    public class Verdict
    {
        public string Description { get; set; }
        public VerdictStatus Status { get; set; }

        public Verdict (string description, VerdictStatus status) => (Description, Status) = (description, status);

        public override string ToString () => $"{Description} Status:{Status}";
    }

    [Serializable]
    public class VisualTaskParameters : AbstractCustomizableParameter
    {
        public GraphInfo[] GraphsParameters { get; }
        public PlayerInfo[] PlayersParameters { get; }

        public VisualTaskParameters (PlayerInfo[] playersParameters,
                                        GraphInfo[] graphsParameters)
        {
            PlayersParameters = playersParameters ?? throw new ArgumentNullException(nameof(playersParameters));
            GraphsParameters = graphsParameters ?? throw new ArgumentNullException(nameof(graphsParameters));

            foreach (var (playerType, _) in playersParameters)
            {
                if (!playerType.IsSubclassOf(typeof(AbstractPlayer)))
                    throw new WrongTypeInCustomizableParameterException(typeof(AbstractPlayer), playerType);
            }

            foreach (var (graphType, _) in graphsParameters)
            {
                if (!graphType.IsSubclassOf(typeof(AbstractGraph)))
                    throw new WrongTypeInCustomizableParameterException(typeof(AbstractGraph), graphType);
            }
        }
    }

    public enum VerdictStatus
    {
        Correct = 0,
        Incorrect = 1,
        Undefined = 2,
    }
}
```


-------------------------------

Updated on 18 February 2021 at 16:24:40 RTZ 9 (����)
