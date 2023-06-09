using System.Collections;
using ArenaGame.Enemy.BaseEnemy;
using UnityEngine;
using Zenject;

namespace ArenaGame.Enemy.BlueEnemy
{
    public class BlueEnemyComponent : BaseEnemyComponent<BlueEnemyModel, BlueEnemyMediator>
    {
        [SerializeField]
        private Transform enemyBulletPrefab;

        [SerializeField]
        private Transform enemyBulletSpawnPosition;

        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public override BlueEnemyModel GetModel()
        {
            return new BlueEnemyModel
            {
                type = type,
                health = health,
                force = force,
                cost = cost,
            };
        }

        protected override void Awake()
        {
            type = EnemyType.Blue;
            base.Awake();
        }

        protected override void AttackPlayer()
        {
            _container.InstantiatePrefab(
                enemyBulletPrefab,
                enemyBulletSpawnPosition.position,
                Quaternion.identity,
                enemyBulletSpawnPosition
            );
        }

        protected override IEnumerator EnemyAfterGroundedBehaviourCoroutine()
        {
            StartCoroutine(EnemyAttackCoroutine());

            yield return null;
        }


        private IEnumerator EnemyAttackCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(3); // Wait for 3 second before shooting
                AttackPlayer();
                yield return null;
            }
        }
    }
}