using System;
using ArenaGame.UI.BaseUI;

namespace ArenaGame.UI.GameSceneUI
{
    [Serializable]
    public class GameSceneUIModel : BaseUiModel
    {
        public bool mainInterfaceActiveState;
        public bool pauseWindowActiveState;
        public bool failureWindowActiveState;
    }
}