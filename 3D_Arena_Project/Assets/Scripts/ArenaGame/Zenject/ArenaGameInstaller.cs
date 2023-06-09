using ArenaGame.Enemy.BlueEnemy;
using ArenaGame.Enemy.RedEnemy;
using ArenaGame.Game.Signals;
using ArenaGame.Player.PlayerMovement;
using ArenaGame.Player.PlayerShooting;
using ArenaGame.Player.PlayerStats;
using ArenaGame.Player.PlayerStats.Signals;
using ArenaGame.Player.PlayerTeleporting;
using ArenaGame.Player.PlayerTeleporting.Signals;
using ArenaGame.UI.MainMenu;
using ArenaGame.UI.GameSceneUI;
using UniRx;
using UserInputs;
using UserInputs.Signals;
using UserInputs.UI.Signals;
using Zenject;

namespace ArenaGame.Zenject
{
    public class ArenaGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<TransientDisposables>().AsSingle();

            Container.Bind<CompositeDisposable>().AsTransient();

            InstallSignalsBindings();
            InstallServicesBindings();
            InstallMediatorsBindings();
        }

        private void InstallSignalsBindings()
        {
            Container.DeclareSignal<DamageMadeSignal>();
            Container.DeclareSignal<ExtraPointsReceivedSignal>();
            Container.DeclareSignal<PlayerTeleportedSignal>();
            Container.DeclareSignal<GameOverSignal>();

            // UserInputs
            Container.DeclareSignal<MoveSignal>();
            Container.DeclareSignal<LookAroundSignal>();
            Container.DeclareSignal<FireSignal>();

            // UI
            Container.DeclareSignal<StartGameRequestedSignal>();
            Container.DeclareSignal<OpenMainMenuRequestedSignal>();
            Container.DeclareSignal<OpenStatisticsRequestedSignal>();
            Container.DeclareSignal<ResetStatisticsRequestedSignal>();
        }

        private void InstallServicesBindings()
        {
            Container.BindInterfacesAndSelfTo<UserControls>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputsHandler>().AsSingle();
        }

        private void InstallMediatorsBindings()
        {
            Container.BindMediator<MainMenuMediator>();
            Container.BindMediator<GameSceneUIMediator>();
            Container.BindMediator<PlayerMovementMediator>();
            Container.BindMediator<PlayerShootingMediator>();
            Container.BindMediator<PlayerStatsMediator>();
            Container.BindMediator<PlayerTeleportingMediator>();
            Container.BindMediator<BlueEnemyMediator>();
            Container.BindMediator<RedEnemyMediator>();
        }
    }
}