using ArenaGame.Base;
using ArenaGame.Player.PlayerStats;
using ArenaGame.Player.PlayerStats.Signals;
using Zenject;

namespace ArenaGame.Enemy.BaseEnemy
{
    public abstract class BaseEnemyMediator<TModel> : BaseMediator<TModel>
        where TModel : BaseEnemyModel
    {
        [Inject]
        protected SignalBus SignalBus;

        public void TriggerForcePointsReceived(int enemyCost)
        {
            var forcePointsReceivedSignal = new ExtraPointsReceivedSignal(
                StatsType.Force, enemyCost, isForKilledEnemy: true);
            SignalBus.Fire(forcePointsReceivedSignal);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Subscribe();
        }

        protected virtual void Subscribe()
        {
        }
    }
}