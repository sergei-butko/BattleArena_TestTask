using System;
using System.Collections;
using ArenaGame.Base;
using ArenaGame.Bullet;
using Attributes;
using UnityEngine;

namespace ArenaGame.Enemy.BaseEnemy
{
    public abstract class BaseEnemyComponent<TModel, TMediator> : BaseComponent<TModel, TMediator>
        where TModel : BaseEnemyModel
        where TMediator : BaseEnemyMediator<TModel>
    {
        // ----- May be better with ScriptableObjects, however not sure whether it is so. -----
        [SerializeField, InspectorReadOnly]
        protected EnemyType type;

        [SerializeField, InspectorReadOnly]
        protected int health;

        [SerializeField, InspectorReadOnly]
        protected int force;

        [SerializeField, InspectorReadOnly]
        protected int cost;
        // ------------------------------------------------------------------------------------

        protected Transform PlayerTransform;

        private bool _isGrounded;

        protected virtual void Awake()
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform;

            switch (type)
            {
                case EnemyType.Blue:
                {
                    health = 100;
                    force = 25;
                    cost = 50;
                    break;
                }
                case EnemyType.Red:
                {
                    health = 50;
                    force = 15;
                    cost = 15;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException($"Enemy of type {type} is unknown!");
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                Mediator.TriggerForcePointsReceived(cost);

                other.GetComponent<PlayerBullet>().AfterShootBehaviour(transform);
                Destroy(gameObject);
            }

            if (other.CompareTag("Arena"))
            {
                _isGrounded = true;
            }
        }

        protected abstract void AttackPlayer();

        protected abstract IEnumerator EnemyAfterGroundedBehaviourCoroutine();

        private void Start()
        {
            StartCoroutine(EnemyOnSpawnBehaviourCoroutine());
        }

        protected virtual void Update()
        {
            if (!_isGrounded)
            {
                MoveEnemyDown();
            }
        }

        private void MoveEnemyDown()
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        private IEnumerator EnemyOnSpawnBehaviourCoroutine()
        {
            while (true)
            {
                if (!_isGrounded)
                {
                    yield return null;
                }
                else
                {
                    StartCoroutine(EnemyAfterGroundedBehaviourCoroutine());
                    yield break;
                }
            }
        }
    }
}