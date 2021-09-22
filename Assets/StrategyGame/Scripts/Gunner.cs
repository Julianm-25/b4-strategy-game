using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Gunner Character
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
            currentHealth = 5;
            actionPoints = 1;
            attackRange = 2;
            attack = 1;
            maxMovepoints = 2;
            currentMovepoints = maxMovepoints;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}