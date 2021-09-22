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
            currentHealth = 8;
            actionPoints = 1;
            attackRange = 1;
            attack = 1;
            maxMovepoints = 1;
            currentMovepoints = maxMovepoints;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}