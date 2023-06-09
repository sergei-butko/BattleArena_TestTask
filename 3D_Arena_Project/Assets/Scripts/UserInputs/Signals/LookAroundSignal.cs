using ArenaGame.Player.PlayerMovement;
using UnityEngine;

namespace UserInputs.Signals
{
    /// <summary>
    /// Is fired by <see cref="InputsHandler"/> after LookAround action performed.
    /// Is handled by <see cref="PlayerMovementMediator"/>.
    /// </summary>
    public class LookAroundSignal
    {
        public Vector2 lookDelta;

        public LookAroundSignal(Vector2 lookDelta)
        {
            this.lookDelta = lookDelta;
        }
    }
}