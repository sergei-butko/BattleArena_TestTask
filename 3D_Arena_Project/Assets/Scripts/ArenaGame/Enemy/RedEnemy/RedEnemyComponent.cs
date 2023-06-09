using System.Collections;
using ArenaGame.Enemy.BaseEnemy;
using UnityEngine;

namespace ArenaGame.Enemy.RedEnemy
{
    public class RedEnemyComponent : BaseEnemyComponent<RedEnemyModel, RedEnemyMediator>
    {
        private const float UpFlightMaxHeight = 1.5f;

        private bool _shouldFlyUp;
        private bool _shouldAttackPlayer;

        public override RedEnemyModel GetModel()
        {
            return new RedEnemyModel
            {
                type = type,
                health = health,
                force = force,
                cost = cost,
            };
        }

        protected override void Awake()
        {
            type = EnemyType.Red;
            base.Awake();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.CompareTag("Player"))
            {
                Mediator.MakeDamage(force);
                Destroy(gameObject);
            }
        }

        protected override void AttackPlayer()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                PlayerTransform.position,
                Time.deltaTime);
        }

        protected override IEnumerator EnemyAfterGroundedBehaviourCoroutine()
        {
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 after grounded

            _shouldFlyUp = true; // Start flying up

            yield return null;
        }

        protected override void Update()
        {
            base.Update();

            if (_shouldFlyUp)
            {
                FlyUp();
            }

            if (_shouldAttackPlayer)
            {
                AttackPlayer();
            }
        }

        private void FlyUp()
        {
            transform.Translate(Vector3.up * Time.deltaTime);

            if (transform.position.y >= UpFlightMaxHeight)
            {
                _shouldFlyUp = false;
                StartCoroutine(EnemyOnFlyingFinishedCoroutine());
            }
        }

        private IEnumerator EnemyOnFlyingFinishedCoroutine()
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second after reached top position

            _shouldAttackPlayer = true; // Start attacking player

            yield return null;
        }
    }
}