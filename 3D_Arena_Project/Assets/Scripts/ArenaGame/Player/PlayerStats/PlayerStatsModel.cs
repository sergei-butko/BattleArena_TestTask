using System;
using ArenaGame.Player.BasePlayer;

namespace ArenaGame.Player.PlayerStats
{
    [Serializable]
    public class PlayerStatsModel : BasePlayerModel
    {
        public int maxStatsValue;
        public int health;
        public int force;
        public int killedEnemiesAmount;
    }
}