using ArenaGame.Player.BasePlayer;
using UniRx;
using UserInputs.Signals;

namespace ArenaGame.Player.PlayerShooting
{
    public class PlayerShootingMediator : BasePlayerMediator<PlayerShootingModel>
    {
        protected override void Subscribe()
        {
            SignalBus
                .GetStream<FireSignal>()
                .Subscribe(OnFire)
                .AddTo(Disposables);
        }

        private void OnFire(FireSignal signal)
        {
            Model.isToFire = true;
            Model.isUlta = signal.isUlta;

            Component.OnUpdate(Model);
        }
    }
}