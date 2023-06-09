using ArenaGame.Base;
using ArenaGame.Game;
using UnityEngine;
using UnityEngine.U2D;

namespace ArenaGame.UI.BaseUI
{
    public abstract class BaseUiComponent<TModel, TMediator> : BaseComponent<TModel, TMediator>, IBaseUI
        where TModel : BaseUiModel
        where TMediator : BaseUiMediator<TModel>
    {
        [SerializeField]
        private SpriteAtlas spriteAtlas;

        private GameManager _gameManager;

        public SpriteAtlas SpriteAtlas => spriteAtlas;

        public GameManager GameManager
        {
            get
            {
                if (_gameManager == null)
                {
                    _gameManager = FindObjectOfType<GameManager>();
                }

                return _gameManager;
            }
        }
    }
}