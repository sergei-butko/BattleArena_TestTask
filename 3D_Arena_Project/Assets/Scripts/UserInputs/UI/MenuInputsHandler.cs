using UnityEngine;
using UserInputs.UI.Signals;
using Zenject;

namespace UserInputs.UI
{
    public class MenuInputsHandler : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        public void StartGame()
        {
            _signalBus.Fire(new StartGameRequestedSignal());
        }

        public void OpenMainMenu()
        {
            _signalBus.Fire(new OpenMainMenuRequestedSignal());
        }

        public void OpenStatistics()
        {
            _signalBus.Fire(new OpenStatisticsRequestedSignal());
        }

        public void ResetStatistics()
        {
            _signalBus.Fire(new ResetStatisticsRequestedSignal());
        }
    }
}