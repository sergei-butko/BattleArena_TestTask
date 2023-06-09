using ArenaGame.Bullet;
using UnityEngine;

namespace ArenaGame.Player.PlayerTeleporting.Signals
{
    /// <summary>
    /// Is fired by <see cref="PlayerTeleportingMediator"/> just before player performed teleporting.
    /// Is handled by <see cref="BlueEnemyBullet"/>.
    /// </summary>
    public class PlayerTeleportedSignal
    {
        public Vector3 beforeTeleportingPosition;

        public PlayerTeleportedSignal(Vector3 beforeTeleportingPosition)
        {
            this.beforeTeleportingPosition = beforeTeleportingPosition;
        }
    }
}