using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Melee Character
    /// </summary>
    public class Gunner : Character
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
            hp = 5;
            ap = 1;
            maxActionDistance = 2;
            maxMoveDistance = 2;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}