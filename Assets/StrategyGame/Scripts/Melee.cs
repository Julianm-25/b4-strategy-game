using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Melee Character
    /// </summary>
    public class Melee : Character
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
            hp = 8;
            ap = 1;
            maxActionDistance = 1;
            maxMoveDistance = 1;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}