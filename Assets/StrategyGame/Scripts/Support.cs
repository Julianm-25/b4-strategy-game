using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Melee Character
    /// </summary>
    public class Support : Character
    {
        private void Start()
        {
            Initialise();
        }

        /// <summary>
        /// initialise the player stats
        /// </summary>
        public void Initialise()
        {
            hp = 3;
            ap = 1;
            maxActionDistance = 1;
            maxMoveDistance = 3;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}