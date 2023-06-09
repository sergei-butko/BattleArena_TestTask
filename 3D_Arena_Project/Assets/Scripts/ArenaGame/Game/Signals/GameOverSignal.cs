using ArenaGame.Player.PlayerStats;
using ArenaGame.UI.GameSceneUI;

namespace ArenaGame.Game.Signals
{
    /// <summary>
    /// Is fired by <see cref="PlayerStatsMediator"/> after player died.
    /// Is handled by <see cref="GameSceneUIMediator"/>.
    /// </summary>
    public class GameOverSignal
    {
        public int killedEnemies;

        public GameOverSignal(int killedEnemies)
        {
            this.killedEnemies = killedEnemies;
        }
    }
}