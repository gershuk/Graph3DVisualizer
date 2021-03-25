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

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Graph3DVisualizer.Customizable;
using Graph3DVisualizer.Graph3D;
using Graph3DVisualizer.PlayerInputControls;

using UnityEngine;

using Yuzu;

namespace Graph3DVisualizer.GraphTasks
{
    /// <summary>
    /// Support class for graph serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public struct GraphInfo
    {
        public GraphParameters GraphParameters { get; set; }
        public Type GraphType { get; set; }

        public GraphInfo (Type graphType, GraphParameters graphParameters)
        {
            GraphType = graphType;
            GraphParameters = graphParameters;
        }

        public static implicit operator (Type graphType, GraphParameters graphParameters) (GraphInfo value) => (value.GraphType, value.GraphParameters);

        public static implicit operator GraphInfo ((Type graphType, GraphParameters graphParameters) value) => new GraphInfo(value.graphType, value.graphParameters);

        public void Deconstruct (out Type graphType, out GraphParameters graphParameters)
        {
            graphType = GraphType;
            graphParameters = GraphParameters;
        }

        public override bool Equals (object obj) => obj is GraphInfo other &&
                   EqualityComparer<Type>.Default.Equals(GraphType, other.GraphType) &&
                   EqualityComparer<GraphParameters>.Default.Equals(GraphParameters, other.GraphParameters);

        public override int GetHashCode ()
        {
            var hashCode = 459771302;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(GraphType);
            hashCode = hashCode * -1521134295 + EqualityComparer<GraphParameters>.Default.GetHashCode(GraphParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Support class for player serialization.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public struct PlayerInfo
    {
        public PlayerParameters PlayerParameters { get; set; }
        public Type PlayerType { get; set; }

        public PlayerInfo (Type playerType, PlayerParameters playerParameters)
        {
            PlayerType = playerType;
            PlayerParameters = playerParameters;
        }

        public static implicit operator (Type playerType, PlayerParameters playerParameters) (PlayerInfo value) => (value.PlayerType, value.PlayerParameters);

        public static implicit operator PlayerInfo ((Type playerType, PlayerParameters playerParameters) value) => new PlayerInfo(value.playerType, value.playerParameters);

        public void Deconstruct (out Type playerType, out PlayerParameters playerParameters)
        {
            playerType = PlayerType;
            playerParameters = PlayerParameters;
        }

        public override bool Equals (object obj) => obj is PlayerInfo other &&
                   EqualityComparer<Type>.Default.Equals(PlayerType, other.PlayerType) &&
                   EqualityComparer<PlayerParameters>.Default.Equals(PlayerParameters, other.PlayerParameters);

        public override int GetHashCode ()
        {
            var hashCode = -640872516;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(PlayerType);
            hashCode = hashCode * -1521134295 + EqualityComparer<PlayerParameters>.Default.GetHashCode(PlayerParameters);
            return hashCode;
        }
    }

    /// <summary>
    /// Class that describes a task for working with a graph in 3d
    /// </summary>
    [CustomizableGrandType(typeof(VisualTaskParameters))]
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

        public VisualTaskParameters DownloadParams (Dictionary<Guid, object> writeCache) =>
            new VisualTaskParameters
                (_players.Select(x => new PlayerInfo(x.GetType(), (PlayerParameters) CustomizableExtension.CallDownloadParams(x, writeCache))).ToArray(),
                 _graphs.Select(x => new GraphInfo(x.GetType(), (GraphParameters) CustomizableExtension.CallDownloadParams(x, writeCache))).ToArray());

        public abstract List<Verdict> GetResult ();

        public abstract void InitTask ();

        public virtual void SetupParams (VisualTaskParameters parameters)
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

    /// <summary>
    /// Class that describes the state of execution of some part of the task.
    /// </summary>
    public class Verdict
    {
        public string Description { get; set; }
        public VerdictStatus Status { get; set; }

        public Verdict (string description, VerdictStatus status) => (Description, Status) = (description, status);

        public override string ToString () => $"{Description} Status:{Status}";
    }

    /// <summary>
    /// Class that describes <see cref="AbstractVisualTask"/> parameters for <see cref="ICustomizable{TParams}"/>.
    /// </summary>
    [Serializable]
    [YuzuAll]
    public class VisualTaskParameters : AbstractCustomizableParameter
    {
        public GraphInfo[] GraphsParameters { get; protected set; }
        public PlayerInfo[] PlayersParameters { get; protected set; }

        public VisualTaskParameters (PlayerInfo[] playersParameters, GraphInfo[] graphsParameters, Guid? parameterId = default) : base(parameterId)
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