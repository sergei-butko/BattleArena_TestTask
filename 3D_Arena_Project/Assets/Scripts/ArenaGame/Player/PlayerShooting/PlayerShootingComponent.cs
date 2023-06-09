using ArenaGame.Player.BasePlayer;
using UnityEngine;
using Zenject;

namespace ArenaGame.Player.PlayerShooting
{
    public class PlayerShootingComponent : BasePlayerComponent<PlayerShootingModel, PlayerShootingMediator>
    {
        [SerializeField]
        private Transform bulletPrefab;

        [SerializeField]
        private Transform bulletSpawnTransform;

        private DiContainer _container;
        private LayerMask _targetObjectLayerMask;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public override PlayerShootingModel GetModel()
        {
            return new PlayerShootingModel { };
        }

        public override void OnUpdate(PlayerShootingModel model)
        {
            if (model.isToFire)
            {
                PerformFire(model.isUlta);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _targetObjectLayerMask = LayerMask.GetMask("Default");
        }

        private void PerformFire(bool isUlta)
        {
            if (isUlta)
            {
                foreach (Transform enemy in EnemiesManagerTransform)
                {
                    Destroy(enemy.gameObject);
                }
            }
            else
            {
                var mouseWorldPosition = Vector3.zero;
                var screenCenterPoint = new Vector3(Screen.width / 2f, Screen.height / 2f);
                var ray = Camera.main.ScreenPointToRay(screenCenterPoint);

                if (Physics.Raycast(ray, out var raycastHit, 100f, _targetObjectLayerMask))
                {
                    mouseWorldPosition = raycastHit.point;
                }

                var bulletSpawnPosition = bulletSpawnTransform.position;
                var aimDirection = (mouseWorldPosition - bulletSpawnPosition).normalized;

                _container.InstantiatePrefab(
                    bulletPrefab,
                    bulletSpawnPosition,
                    Quaternion.LookRotation(aimDirection, Vector3.up),
                    transform
                );
            }
        }
    }
}