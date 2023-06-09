using ArenaGame.Enemy;
using ArenaGame.Player.PlayerStats;
using ArenaGame.Player.PlayerStats.Signals;
using UnityEngine;
using Zenject;

namespace ArenaGame.Bullet
{
    public class PlayerBullet : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        [SerializeField, Range(0.6f, 3f)]
        private float maxRicochetedDistance = 1.5f;

        private const int ArenaRadius = 5;

        private const int HalfDivider = 2;
        private const int TenthDivider = 10;

        private Transform _enemiesManagerTransform;
        private Transform _ricochetAim;

        private bool _wasRicocheted;
        private bool _isRicocheting;

        public void AfterShootBehaviour(Transform killedEnemy)
        {
            var playerModel = GetComponentInParent<PlayerStatsComponent>().GetModel();

            #region RepeatedShoot

            if (_isRicocheting) // If repeated shoot
            {
                ExtraPointsReceivedSignal extraPointsReceivedSignal;

                if (Random.Range(1, 2) % 2 == 0)
                {
                    // Not sure whether I should add half of maximum or current health
                    extraPointsReceivedSignal = new ExtraPointsReceivedSignal(
                        StatsType.Health,
                        playerModel.maxStatsValue / HalfDivider
                    );
                }
                else
                {
                    // Not sure what does it mean "a little force", so decided to add 10%
                    extraPointsReceivedSignal = new ExtraPointsReceivedSignal(
                        StatsType.Force,
                        playerModel.maxStatsValue / TenthDivider
                    );
                }

                _signalBus.Fire(extraPointsReceivedSignal);

                _isRicocheting = false;
                Destroy(gameObject);
                return;
            }

            #endregion

            #region RicochetProbability

            var ricochetProbability = playerModel.maxStatsValue - playerModel.health;

            if (ricochetProbability <= 0) // If probability equals or less than zero
            {
                Destroy(gameObject);
                return;
            }

            var random = Random.Range(0, playerModel.maxStatsValue);

            if (random > ricochetProbability) // If random out of probable region
            {
                Destroy(gameObject);
                return;
            }

            #endregion

            #region NearestEnemy
            
            foreach (Transform enemy in _enemiesManagerTransform)
            {
                if (enemy == killedEnemy) return;

                var distanceToEnemy = (enemy.position - transform.position).magnitude;

                if (distanceToEnemy <= maxRicochetedDistance)
                {
                    _isRicocheting = true;

                    _ricochetAim = enemy;
                    return;
                }
            }

            #endregion
        }

        private void Awake()
        {
            _enemiesManagerTransform = FindObjectOfType<EnemySpawnManager>().transform;
        }

        private void Update()
        {
            if (_isRicocheting)
            {
                MoveBulletToNewAim();
            }
            else
            {
                MoveBulletForward();
            }

            if (transform.position.x > ArenaRadius || transform.position.z > ArenaRadius)
            {
                Destroy(gameObject);
            }
        }

        private void MoveBulletForward()
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }

        private void MoveBulletToNewAim()
        {
            transform.position = Vector3.MoveTowards(transform.position, _ricochetAim.position, Time.deltaTime);
        }
    }
}