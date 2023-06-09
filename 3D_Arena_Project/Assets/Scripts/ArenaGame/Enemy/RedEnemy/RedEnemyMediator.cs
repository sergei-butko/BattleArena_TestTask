using ArenaGame.Enemy.BaseEnemy;
using ArenaGame.Player.PlayerStats;
using ArenaGame.Player.PlayerStats.Signals;

namespace ArenaGame.Enemy.RedEnemy
{
    public class RedEnemyMediator : BaseEnemyMediator<RedEnemyModel>
    {
        public void MakeDamage(int force)
        {
            var damageMadeSignal = new DamageMadeSignal(StatsType.Health, force);
            SignalBus.Fire(damageMadeSignal);
        }
    }
}