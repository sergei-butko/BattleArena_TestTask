using ArenaGame.Game.Signals;
using ArenaGame.UI.BaseUI;
using UniRx;
using UnityEngine;

namespace ArenaGame.UI.GameSceneUI
{
    public class GameSceneUIMediator : BaseUiMediator<GameSceneUIModel>
    {
        protected override void Subscribe()
        {
            SignalBus
                .GetStream<GameOverSignal>()
                .Subscribe(OnGameOver)
                .AddTo(Disposables);
        }

        private void OnGameOver(GameOverSignal signal)
        {
            var currentKilledEnemiesMaxAmount = PlayerPrefs.GetInt("KilledEnemies", 0);

            if (signal.killedEnemies > currentKilledEnemiesMaxAmount)
            {
                PlayerPrefs.SetInt("KilledEnemies", signal.killedEnemies);
            }

            Model.mainInterfaceActiveState = false;
            Model.failureWindowActiveState = true;

            Component.OnUpdate(Model);
        }
    }
}