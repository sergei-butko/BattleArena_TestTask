using ArenaGame.UI.BaseUI;
using TMPro;
using UnityEngine;

namespace ArenaGame.UI.MainMenu
{
    public class MainMenuComponent : BaseUiComponent<MainMenuModel, MainMenuMediator>
    {
        [SerializeField]
        private GameObject mainPanel;

        [SerializeField]
        private GameObject statsPanel;

        [SerializeField]
        private TextMeshProUGUI statsText;

        private const string StatsTextBase = "Max killed enemies: ";

        private bool _mainPanelIsActive = true;
        private bool _statsPanelIsActive;

        public override MainMenuModel GetModel()
        {
            return new MainMenuModel
            {
                mainPanelIsActive = _mainPanelIsActive,
                statsPanelIsActive = _statsPanelIsActive,
            };
        }

        public override void OnUpdate(MainMenuModel model)
        {
            _mainPanelIsActive = model.mainPanelIsActive;
            _statsPanelIsActive = model.statsPanelIsActive;

            UpdatePanelsActiveStates();
            UpdateStatsText();
        }

        private void Awake()
        {
            UpdatePanelsActiveStates();
        }

        private void UpdatePanelsActiveStates()
        {
            mainPanel.SetActive(_mainPanelIsActive);
            statsPanel.SetActive(_statsPanelIsActive);
        }

        private void UpdateStatsText()
        {
            statsText.text = StatsTextBase + PlayerPrefs.GetInt("KilledEnemies", 0);
        }
    }
}