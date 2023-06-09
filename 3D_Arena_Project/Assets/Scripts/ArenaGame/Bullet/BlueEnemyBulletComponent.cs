using System;
using ArenaGame.Player.PlayerStats;
using ArenaGame.Player.PlayerStats.Signals;
using ArenaGame.Player.PlayerTeleporting.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace ArenaGame.Bullet
{
    public class BlueEnemyBullet : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private int bulletForceSize;

        private readonly CompositeDisposable _disposables = new();

        private SignalBus _signalBus;

        private Transform _playerTransform;
        private Vector3 _targetPosition = Vector3.zero;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus
                .GetStream<PlayerTeleportedSignal>()
                .Subscribe(OnPlayerTeleported)
                .AddTo(_disposables);
        }

        private void OnPlayerTeleported(PlayerTeleportedSignal signal)
        {
            _targetPosition = signal.beforeTeleportingPosition;
        }

        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            var target = _targetPosition == Vector3.zero
                ? _playerTransform.position
                : _targetPosition;

            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                Time.deltaTime);

            if ((transform.position - _targetPosition).magnitude <= 0.01f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var damageMadeSignal = new DamageMadeSignal(StatsType.Force, bulletForceSize);
                _signalBus.Fire(damageMadeSignal);
            }

            Destroy(gameObject);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}