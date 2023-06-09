using ArenaGame.Base;
using ArenaGame.Enemy;
using UnityEngine;

namespace ArenaGame.Player.BasePlayer
{
    public abstract class BasePlayerComponent<TModel, TMediator> : BaseComponent<TModel, TMediator>
        where TModel : BasePlayerModel
        where TMediator : BasePlayerMediator<TModel>
    {
        protected Transform EnemiesManagerTransform;

        protected virtual void Awake()
        {
            EnemiesManagerTransform = FindObjectOfType<EnemySpawnManager>().transform;
        }
    }
}