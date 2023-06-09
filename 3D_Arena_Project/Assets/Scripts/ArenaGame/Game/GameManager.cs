using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaGame.Game
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            LoadSceneAdditive(ScenesNames.MainMenu);
        }

        public void StartGame()
        {
            SceneManager.UnloadSceneAsync(ScenesNames.MainMenu.ToString())
                .completed += _ => LoadSceneAdditive(ScenesNames.GameScene);
        }

        public void RestartGame()
        {
            SceneManager.UnloadSceneAsync(ScenesNames.GameScene.ToString())
                .completed += _ => LoadSceneAdditive(ScenesNames.GameScene);
        }

        public void OpenMainMenu()
        {
            SceneManager.UnloadSceneAsync(ScenesNames.GameScene.ToString())
                .completed += _ => LoadSceneAdditive(ScenesNames.MainMenu);
        }

        private void LoadSceneAdditive(ScenesNames scene)
        {
            SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        }
    }
}