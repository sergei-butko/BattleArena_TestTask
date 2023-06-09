using System;
using ArenaGame.Game.Signals;
using ArenaGame.Player.BasePlayer;
using ArenaGame.Player.PlayerStats.Signals;
using UniRx;
using UnityEngine;
using UserInputs.Signals;

namespace ArenaGame.Player.PlayerStats
{
    public class PlayerStatsMediator : BasePlayerMediator<PlayerStatsModel>
    {
        public void PerformFailure(int killedEnemiesAmount)
        {
            var gameOverSignal = new GameOverSignal(killedEnemiesAmount);
            SignalBus.Fire(gameOverSignal);
        }

        protected override void Subscribe()
        {
            SignalBus
                .GetStream<FireSignal>()
                .Subscribe(OnFire)
                .AddTo(Disposables);
            SignalBus
                .GetStream<DamageMadeSignal>()
                .Subscribe(OnDamageMade)
                .AddTo(Disposables);
            SignalBus
                .GetStream<ExtraPointsReceivedSignal>()
                .Subscribe(OnExtraPointsReceived)
                .AddTo(Disposables);
        }

        private void OnFire(FireSignal signal)
        {
            if (!signal.isUlta) return;

            Model.force = 0;
            Component.OnUpdate(Model);
        }

        private void OnDamageMade(DamageMadeSignal signal)
        {
            switch (signal.type)
            {
                case StatsType.Health:
                {
                    Model.health -= signal.damageAmount;
                    Model.health = Mathf.Clamp(Model.health, 0, Model.maxStatsValue);
                    break;
                }
                case StatsType.Force:
                {
                    Model.force -= signal.damageAmount;
                    Model.force = Mathf.Clamp(Model.force, 0, Model.maxStatsValue);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException($"Damage type {signal.type} is unknown!");
                }
            }

            Component.OnUpdate(Model);
        }

        private void OnExtraPointsReceived(ExtraPointsReceivedSignal signal)
        {
            switch (signal.type)
            {
                case StatsType.Force:
                {
                    Model.force += signal.pointsAmount;
                    Model.force = Mathf.Clamp(Model.force, 0, Model.maxStatsValue);
                    break;
                }
                case StatsType.Health:
                {
                    Model.health += signal.pointsAmount;
                    Model.health = Mathf.Clamp(Model.health, 0, Model.maxStatsValue);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException($"Stats of type {signal.type} is unknown!");
                }
            }

            if (signal.isForKilledEnemy)
            {
                Model.killedEnemiesAmount += 1;
            }

            Component.OnUpdate(Model);
        }
    }
}