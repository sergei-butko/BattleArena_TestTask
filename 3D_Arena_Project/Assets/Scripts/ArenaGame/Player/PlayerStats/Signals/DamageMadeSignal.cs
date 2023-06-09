using ArenaGame.Enemy.RedEnemy;

namespace ArenaGame.Player.PlayerStats.Signals
{
    /// <summary>
    /// Is fired by <see cref="RedEnemyMediator"/> after enemy collided with player.
    /// Is handled by <see cref="PlayerStatsMediator"/>.
    /// </summary>
    public class DamageMadeSignal
    {
        public StatsType type;
        public int damageAmount;

        public DamageMadeSignal(StatsType type, int damageAmount)
        {
            this.type = type;
            this.damageAmount = damageAmount;
        }
    }
}