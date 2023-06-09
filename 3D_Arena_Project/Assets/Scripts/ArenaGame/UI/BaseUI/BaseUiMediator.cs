using ArenaGame.Base;
using Zenject;

namespace ArenaGame.UI.BaseUI
{
    public abstract class BaseUiMediator<TModel> : BaseMediator<TModel>
        where TModel : BaseUiModel
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