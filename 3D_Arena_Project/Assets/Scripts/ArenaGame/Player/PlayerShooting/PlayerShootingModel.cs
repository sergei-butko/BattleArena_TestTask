using System;
using ArenaGame.Player.BasePlayer;

namespace ArenaGame.Player.PlayerShooting
{
    [Serializable]
    public class PlayerShootingModel : BasePlayerModel
    {
        public bool isToFire;
        public bool isUlta;
    }
}