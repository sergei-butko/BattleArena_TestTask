using System;
using ArenaGame.Base;

namespace ArenaGame.Enemy.BaseEnemy
{
    [Serializable]
    public abstract class BaseEnemyModel : BaseModel
    {
        public EnemyType type;
        public int health;
        public int force;
        public int cost;
    }
}