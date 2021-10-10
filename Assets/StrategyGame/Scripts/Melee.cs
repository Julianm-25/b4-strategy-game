using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Melee Character
    /// </summary>
    public class Melee : Character
    {
        private void Awake()
        {
            Initialise();
        }

        /// <summary>
        /// initialise the player stats
        /// </summary>
        public void Initialise()
        {
            currentHealth = 8;
            maxAP = 1;
            actionPoints = 1;
            attackRange = 1;
            attack = 3;
            maxMovepoints = 1;
            currentMovepoints = maxMovepoints;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}