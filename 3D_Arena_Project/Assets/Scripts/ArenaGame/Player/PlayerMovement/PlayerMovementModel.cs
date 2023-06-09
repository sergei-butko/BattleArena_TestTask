using System;
using ArenaGame.Player.BasePlayer;
using UnityEngine;

namespace ArenaGame.Player.PlayerMovement
{
    [Serializable]
    public class PlayerMovementModel : BasePlayerModel
    {
        public Vector2 moveDelta;
        public Vector2 lookDelta;
    }
}