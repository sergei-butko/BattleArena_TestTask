using ArenaGame.Player.PlayerMovement;
using UnityEngine;

namespace UserInputs.Signals
{
    /// <summary>
    /// Is fired by <see cref="InputsHandler"/> after LookAround action performed.
    /// Is handled by <see cref="PlayerMovementMediator"/>.
    /// </summary>
    public class MoveSignal
    {
        public Vector2 moveDelta;

        public MoveSignal(Vector2 moveDelta)
        {
            this.moveDelta = moveDelta;
        }
    }
}