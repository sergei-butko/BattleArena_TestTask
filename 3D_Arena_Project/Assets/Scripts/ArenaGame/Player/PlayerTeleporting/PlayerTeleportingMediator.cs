using ArenaGame.Player.BasePlayer;
using ArenaGame.Player.PlayerTeleporting.Signals;
using UnityEngine;

namespace ArenaGame.Player.PlayerTeleporting
{
    public class PlayerTeleportingMediator : BasePlayerMediator<PlayerTeleportingModel>
    {
        public void FirePlayerTeleported(Vector3 beforeTeleportingPosition)
        {
            var playerTeleportedSignal = new PlayerTeleportedSignal(beforeTeleportingPosition);
            SignalBus.Fire(playerTeleportedSignal);
        }
    }
}