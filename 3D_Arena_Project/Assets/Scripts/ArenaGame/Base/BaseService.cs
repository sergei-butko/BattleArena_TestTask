using System;
using UniRx;

namespace ArenaGame.Base
{
    public abstract class BaseService : IDisposable
    {
        protected CompositeDisposable Disposables { get; } = new();

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}