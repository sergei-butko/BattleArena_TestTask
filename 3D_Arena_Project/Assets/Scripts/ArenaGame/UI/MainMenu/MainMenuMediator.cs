using ArenaGame.UI.BaseUI;
using UniRx;
using UnityEngine;
using UserInputs.UI.Signals;

namespace ArenaGame.UI.MainMenu
{
    public class MainMenuMediator : BaseUiMediator<MainMenuModel>
    {
        protected override void Subscribe()
        {
            SignalBus
                .GetStream<StartGameRequestedSignal>()
                .Subscribe(OnStartGameRequested)
                .AddTo(Disposables);
            SignalBus
                .GetStream<OpenMainMenuRequestedSignal>()
                .Subscribe(OnOpenMainMenuRequested)
                .AddTo(Disposables);
            SignalBus
                .GetStream<OpenStatisticsRequestedSignal>()
                .Subscribe(OnOpenStatisticsRequested)
                .AddTo(Disposables);
            SignalBus
                .GetStream<ResetStatisticsRequestedSignal>()
                .Subscribe(OnResetStatisticsRequested)
                .AddTo(Disposables);
        }

        private void OnStartGameRequested(StartGameRequestedSignal signal)
        {
            ((MainMenuComponent) Component).GameManager.StartGame();
        }

        private void OnOpenMainMenuRequested(OpenMainMenuRequestedSignal signal)
        {
            Model.mainPanelIsActive = true;
            Model.statsPanelIsActive = false;

            Component.OnUpdate(Model);
        }

        private void OnOpenStatisticsRequested(OpenStatisticsRequestedSignal signal)
        {
            Model.statsPanelIsActive = true;
            Model.mainPanelIsActive = false;

            Component.OnUpdate(Model);
        }

        private void OnResetStatisticsRequested(ResetStatisticsRequestedSignal signal)
        {
            PlayerPrefs.SetInt("KilledEnemies", 0);
            Component.OnUpdate(Model);
        }
    }
}