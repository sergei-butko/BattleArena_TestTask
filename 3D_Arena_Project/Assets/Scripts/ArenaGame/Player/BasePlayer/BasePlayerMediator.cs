using ArenaGame.Base;
using Zenject;

namespace ArenaGame.Player.BasePlayer
{
    public abstract class BasePlayerMediator<TModel> : BaseMediator<TModel>
        where TModel : BasePlayerModel
    {
        [Inject]
        protected SignalBus SignalBus;

        protected override void OnInitialized()
        {
            Subscribe();
        }

        protected virtual void Subscribe()
        {
        }
    }
}