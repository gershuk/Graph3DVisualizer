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

using Grpah3DVisualizer.Customizable;
using Grpah3DVisualizer.Graph3D;
using Grpah3DVisualizer.PlayerInputControls;

using UnityEngine;

namespace Grpah3DVisualizer.GraphTasks
{
    public abstract class AbstractVisualTask : MonoBehaviour, ICustomizable<VisualTaskParameters>
    {
        protected List<AbstractGraph> _graphs = new List<AbstractGraph>();
        protected List<AbstractPlayer> _players = new List<AbstractPlayer>();

        public abstract IReadOnlyCollection<AbstractGraph> Graphs { get; protected set; }
        public abstract IReadOnlyCollection<AbstractPlayer> Players { get; protected set; }

        public void DestroyTask ()
        {
            foreach (var player in _players)
                Destroy(player.gameObject);
            foreach (var graph in _graphs)
                Destroy(graph.gameObject);
            Destroy(gameObject);
        }

        public VisualTaskParameters DownloadParams () =>
            new VisualTaskParameters
                (_players.Select(x => (x.GetType(), CustomizableExtension.CallDownloadParams<PlayerParameters>(x).ToArray())).ToArray(),
                 _graphs.Select(x => (x.GetType(), CustomizableExtension.CallDownloadParams<GraphParameters>(x).ToArray())).ToArray());

        public abstract List<Verdict> GetResult ();

        public abstract void InitTask ();

        public void SetupParams (VisualTaskParameters parameters)
        {
            foreach (var (graphType, graphParameters) in parameters.GraphsParameters)
            {
                var graph = new GameObject("Graph").AddComponent(graphType);
                CustomizableExtension.CallSetUpParams(graph, graphParameters);
            }

            foreach (var (playerType, playerParameters) in parameters.PlayersParameters)
            {
                var graph = new GameObject("Player").AddComponent(playerType);
                CustomizableExtension.CallSetUpParams(graph, playerParameters);
            }
        }

        public abstract void StartTask ();

        public abstract void StopTask ();
    }

    public class Verdict
    {
        public string Description { get; set; }
        public VerdictStatus Status { get; set; }

        public Verdict (string description, VerdictStatus status) => (Description, Status) = (description, status);

        public override string ToString () => $"{Description} Status:{Status}";
    }

    public class VisualTaskParameters : CustomizableParameter
    {
        public (Type graphType, GraphParameters[] graphParameters)[] GraphsParameters { get; }
        public (Type playerType, PlayerParameters[] playerParameters)[] PlayersParameters { get; }

        public VisualTaskParameters ((Type playerType, PlayerParameters[] playerParameters)[] playersParameters,
                                        (Type graphType, GraphParameters[] graphParameters)[] graphsParameters)
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