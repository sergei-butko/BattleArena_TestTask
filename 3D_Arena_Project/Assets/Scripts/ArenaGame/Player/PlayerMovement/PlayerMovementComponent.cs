using ArenaGame.Player.BasePlayer;
using Cinemachine;
using UnityEngine;

namespace ArenaGame.Player.PlayerMovement
{
    public class PlayerMovementComponent : BasePlayerComponent<PlayerMovementModel, PlayerMovementMediator>
    {
        [SerializeField, Range(1f, 3f)]
        private float moveSmoothing = 2f;

        [SerializeField, Range(0.5f, 1f)]
        private float lookAroundSmoothing = 0.75f;

        private CinemachinePOV _cinemachinePov;

        private Vector2 _currentMoveDelta;
        private Vector2 _currentLookDelta;

        private CinemachinePOV CinemachinePov
        {
            get
            {
                if (_cinemachinePov == null)
                {
                    _cinemachinePov = GetComponentInChildren<CinemachineVirtualCamera>()
                        .GetCinemachineComponent<CinemachinePOV>();
                }

                return _cinemachinePov;
            }
        }

        public override PlayerMovementModel GetModel()
        {
            return new PlayerMovementModel
            {
                moveDelta = _currentMoveDelta,
                lookDelta = _currentLookDelta,
            };
        }

        public override void OnUpdate(PlayerMovementModel model)
        {
            _currentMoveDelta = model.moveDelta;
            _currentLookDelta = model.lookDelta;
        }

        private void Update()
        {
            if (_currentMoveDelta != Vector2.zero)
            {
                MovePlayer();
            }

            if (_currentLookDelta != Vector2.zero)
            {
                LookAround();
            }
        }

        private void LookAround()
        {
            var delta = _currentLookDelta * lookAroundSmoothing;

            CinemachinePov.m_HorizontalAxis.Value += delta.x;
            CinemachinePov.m_VerticalAxis.Value -= delta.y;
        }

        private void MovePlayer()
        {
            var yAxisPosition = transform.position.y;

            var direction = new Vector3(_currentMoveDelta.x, 0, _currentMoveDelta.y);
            var velocity = moveSmoothing * Time.deltaTime * direction;

            transform.Translate(velocity);

            var newPosition = transform.position;
            newPosition.y = yAxisPosition;

            transform.position = newPosition;
        }
    }
}