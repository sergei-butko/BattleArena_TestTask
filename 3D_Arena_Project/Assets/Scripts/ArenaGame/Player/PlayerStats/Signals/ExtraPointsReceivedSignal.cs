using ArenaGame.Enemy.BaseEnemy;

namespace ArenaGame.Player.PlayerStats.Signals
{
    /// <summary>
    /// Is fired by <see cref="BaseEnemyMediator{TModel}"/> after player bullet collided with enemy.
    /// Is handled by <see cref="PlayerStatsMediator"/>.
    /// </summary>
    public class ExtraPointsReceivedSignal
    {
        public StatsType type;
        public int pointsAmount;
        public bool isForKilledEnemy;

        public ExtraPointsReceivedSignal(StatsType type, int pointsAmount, bool isForKilledEnemy = false)
        {
            this.type = type;
            this.pointsAmount = pointsAmount;
            this.isForKilledEnemy = isForKilledEnemy;
        }
    }
}