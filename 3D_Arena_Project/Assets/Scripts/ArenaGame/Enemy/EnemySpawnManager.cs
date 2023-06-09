using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ArenaGame.Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField]
        private Transform blueEnemyPrefab;

        [SerializeField]
        private Transform redEnemyPrefab;

        private const int ArenaRadius = 5;

        private const float MinimalSpawnInterval = 2f;
        private const float SpawnIntervalReducer = 0.5f;

        private DiContainer _container;

        private float _spawnInterval = 5f;
        private int _spawnIteration;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _container = diContainer;
        }

        private void Start()
        {
            StartCoroutine(EnemiesSpawnCoroutine());
        }

        private IEnumerator EnemiesSpawnCoroutine()
        {
            while (true)
            {
                if (_spawnInterval <= MinimalSpawnInterval)
                {
                    yield return new WaitForSeconds(MinimalSpawnInterval);
                }
                else
                {
                    yield return new WaitForSeconds(_spawnInterval);
                    _spawnInterval -= SpawnIntervalReducer;
                }

                var enemyPrefab = _spawnIteration % 5 == 0 ? blueEnemyPrefab : redEnemyPrefab;

                var onArenaSpawnPosition = Random.insideUnitCircle * (ArenaRadius - enemyPrefab.localScale.x);

                var yAxisSpawnPosition = enemyPrefab.position.y;
                yAxisSpawnPosition += 1;

                var spawnPosition = new Vector3(onArenaSpawnPosition.x, yAxisSpawnPosition, onArenaSpawnPosition.y);

                _container.InstantiatePrefab(enemyPrefab, spawnPosition, Quaternion.identity, transform);

                _spawnIteration += 1;

                yield return null;
            }
        }
    }
}