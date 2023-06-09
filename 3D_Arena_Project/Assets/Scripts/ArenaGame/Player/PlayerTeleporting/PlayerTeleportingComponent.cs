using ArenaGame.Player.BasePlayer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArenaGame.Player.PlayerTeleporting
{
    public class PlayerTeleportingComponent : BasePlayerComponent<PlayerTeleportingModel, PlayerTeleportingMediator>
    {
        private const float ArenaRadius = 4.75f;

        public override PlayerTeleportingModel GetModel()
        {
            return new PlayerTeleportingModel();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Arena"))
            {
                var playerTransform = transform.parent;

                Mediator.FirePlayerTeleported(playerTransform.position);

                var newPlayerPosition = GenerateFarthestPointInCircle();
                playerTransform.position = newPlayerPosition;
            }
        }

        private Vector3 GenerateFarthestPointInCircle()
        {
            var farthestPoint = Vector3.zero;
            var maxDistance = float.MinValue;

            const int maxAttempts = 50;

            for (var i = 0; i < maxAttempts; i++)
            {
                var randomPoint = Random.insideUnitCircle * ArenaRadius;
                var generatedPoint = new Vector3(randomPoint.x, transform.parent.position.y, randomPoint.y);

                var distance = GetTotalDistanceFromPoint(generatedPoint);

                if (!(distance > maxDistance)) continue;

                maxDistance = distance;
                farthestPoint = generatedPoint;
            }

            return farthestPoint;
        }

        private float GetTotalDistanceFromPoint(Vector3 point)
        {
            var totalDistance = 0f;

            foreach (Transform enemy in EnemiesManagerTransform)
            {
                var distance = Vector3.Distance(point, enemy.position);
                totalDistance += distance;
            }

            return totalDistance;
        }
    }
}