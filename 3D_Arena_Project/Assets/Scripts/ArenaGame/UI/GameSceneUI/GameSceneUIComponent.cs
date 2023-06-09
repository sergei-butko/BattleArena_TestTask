using ArenaGame.UI.BaseUI;
using UnityEngine;

namespace ArenaGame.UI.GameSceneUI
{
    public class GameSceneUIComponent : BaseUiComponent<GameSceneUIModel, GameSceneUIMediator>
    {
        [SerializeField]
        private Transform mainInterface;

        [SerializeField]
        private Transform pauseWindow;

        [SerializeField]
        private Transform failureWindow;

        private bool _mainInterfaceActiveState = true;
        private bool _pauseWindowActiveState;
        private bool _failureWindowActiveState;

        public override GameSceneUIModel GetModel()
        {
            return new GameSceneUIModel
            {
                mainInterfaceActiveState = _mainInterfaceActiveState,
                pauseWindowActiveState = _pauseWindowActiveState,
                failureWindowActiveState = _failureWindowActiveState,
            };
        }

        public override void OnUpdate(GameSceneUIModel model)
        {
            _mainInterfaceActiveState = model.mainInterfaceActiveState;
            _pauseWindowActiveState = model.pauseWindowActiveState;
            _failureWindowActiveState = model.failureWindowActiveState;

            UpdateMenusVisibility();
        }

        public void PauseGame()
        {
            _mainInterfaceActiveState = false;
            _pauseWindowActiveState = true;

            UpdateMenusVisibility();
        }

        public void ContinueGame()
        {
            Time.timeScale = 1; // Unfreeze actions in scene

            _mainInterfaceActiveState = true;
            _pauseWindowActiveState = false;

            UpdateMenusVisibility();
        }

        public void RestartGame()
        {
            _mainInterfaceActiveState = true;
            _pauseWindowActiveState = false;
            _failureWindowActiveState = false;

            UpdateMenusVisibility();

            GameManager.RestartGame();
        }

        public void OpenMainMenu()
        {
            GameManager.OpenMainMenu();
        }

        private void Awake()
        {
            Time.timeScale = 1; // Unfreeze actions in scene

            UpdateMenusVisibility();
        }

        private void UpdateMenusVisibility()
        {
            // If either pause or failure window is displayed than freeze actions in scene
            if (_pauseWindowActiveState || _failureWindowActiveState)
            {
                Time.timeScale = 0;
            }

            mainInterface.gameObject.SetActive(_mainInterfaceActiveState);
            pauseWindow.gameObject.SetActive(_pauseWindowActiveState);
            failureWindow.gameObject.SetActive(_failureWindowActiveState);
        }
    }
}