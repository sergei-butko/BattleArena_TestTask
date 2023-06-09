using System;
using ArenaGame.UI.BaseUI;

namespace ArenaGame.UI.MainMenu
{
    [Serializable]
    public class MainMenuModel : BaseUiModel
    {
        public bool mainPanelIsActive;
        public bool statsPanelIsActive;
    }
}