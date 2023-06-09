using ArenaGame.Player.PlayerShooting;

namespace UserInputs.Signals
{
    /// <summary>
    /// Is fired by <see cref="InputsHandler"/> after Fire action performed.
    /// Is handled by <see cref="PlayerShootingMediator"/>.
    /// </summary>
    public class FireSignal
    {
        public bool isUlta;

        public FireSignal(bool isUlta)
        {
            this.isUlta = isUlta;
        }
    }
}