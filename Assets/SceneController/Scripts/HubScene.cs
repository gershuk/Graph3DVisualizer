using System.Collections.Generic;

using Graph3DVisualizer.PlayerInputControls;

using UnityEngine;

namespace Graph3DVisualizer.SceneController
{
    public class HubScene : AbstractSceneController
    {
        public override void InitTask ()
        {
            Players = new List<AbstractPlayer>();
            var player = Instantiate(Resources.Load<GameObject>(_playerPrefabPath)).GetComponent<FlyPlayer>();
            player.SetupParams(new PlayerParameters(new Vector3(0, 0, -50)));
            Players.Add(player);
        }
    }
}