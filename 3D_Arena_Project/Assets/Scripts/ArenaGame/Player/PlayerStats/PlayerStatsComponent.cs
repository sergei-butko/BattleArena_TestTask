using System;
using ArenaGame.Player.BasePlayer;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace ArenaGame.Player.PlayerStats
{
    public class PlayerStatsComponent : BasePlayerComponent<PlayerStatsModel, PlayerStatsMediator>
    {
        [SerializeField]
        private Image healthProgressBar;

        [SerializeField]
        private Image forceProgressBar;

        [SerializeField]
        private Image ultaProgressBar;

        [SerializeField]
        private TextMeshProUGUI killedEnemiesText;

        [SerializeField]
        private int maxStatsValue = 100;

        [SerializeField, InspectorReadOnly]
        private int health = 100;

        [SerializeField, InspectorReadOnly]
        private int force = 50;

        [SerializeField, InspectorReadOnly]
        private int killedEnemiesAmount;

        private const string KilledEnemiesText = "Total enemies killed: ";

        private Button _ultraButton;
        private OnScreenButton _ultraOnScreenButton;

        private const float ProgressBarDivider = 100f;

        public override PlayerStatsModel GetModel()
        {
            return new PlayerStatsModel
            {
                maxStatsValue = maxStatsValue,
                health = health,
                force = force,
                killedEnemiesAmount = killedEnemiesAmount,
            };
        }

        public override void OnUpdate(PlayerStatsModel model)
        {
            health = model.health;
            force = model.force;
            killedEnemiesAmount = model.killedEnemiesAmount;

            killedEnemiesText.text = KilledEnemiesText + killedEnemiesAmount;

            UpdateStatsProgressBars();

            if (health == 0)
            {
                PerformFailure();
            }
        }

        protected override void Awake()
        {
            if (!ultaProgressBar.TryGetComponent(out _ultraButton))
            {
                throw new NullReferenceException(
                    $"Component of type {typeof(Button)} is not set for {ultaProgressBar.transform.name}");
            }

            if (!ultaProgressBar.TryGetComponent(out _ultraOnScreenButton))
            {
                throw new NullReferenceException(
                    $"Component of type {typeof(OnScreenButton)} is not set for {ultaProgressBar.transform.name}");
            }

            UpdateUltaButtonsActiveState(targetActiveState: false);

            UpdateStatsProgressBars();
        }

        private void Update()
        {
            if (!_ultraButton.interactable && (int) ultaProgressBar.fillAmount == 1)
            {
                UpdateUltaButtonsActiveState(targetActiveState: true);
            }
        }

        private void PerformFailure()
        {
            Mediator.PerformFailure(killedEnemiesAmount);
        }

        private void UpdateStatsProgressBars()
        {
            healthProgressBar.fillAmount = health / ProgressBarDivider;
            forceProgressBar.fillAmount = force / ProgressBarDivider;
            ultaProgressBar.fillAmount = forceProgressBar.fillAmount;

            if (force == 0)
            {
                UpdateUltaButtonsActiveState(targetActiveState: false);
            }
        }

        private void UpdateUltaButtonsActiveState(bool targetActiveState)
        {
            _ultraButton.interactable = targetActiveState;
            _ultraOnScreenButton.enabled = targetActiveState;
        }
    }
}