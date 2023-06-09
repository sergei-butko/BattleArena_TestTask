using ArenaGame.Player.BasePlayer;
using UniRx;
using UserInputs.Signals;

namespace ArenaGame.Player.PlayerMovement
{
    public class PlayerMovementMediator : BasePlayerMediator<PlayerMovementModel>
    {
        protected override void Subscribe()
        {
            SignalBus
                .GetStream<MoveSignal>()
                .Subscribe(OnMove)
                .AddTo(Disposables);
            SignalBus
                .GetStream<LookAroundSignal>()
                .Subscribe(OnLookAround)
                .AddTo(Disposables);
        }

        private void OnMove(MoveSignal signal)
        {
            Model.moveDelta = signal.moveDelta;
            Component.OnUpdate(Model);
        }

        private void OnLookAround(LookAroundSignal signal)
        {
            Model.lookDelta = signal.lookDelta;
            Component.OnUpdate(Model);
        }
    }
}