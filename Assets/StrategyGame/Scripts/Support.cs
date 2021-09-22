using System;
using UnityEngine;

namespace StrategyGame.Scripts
{
    /// <summary>
    /// Support Character
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
            currentHealth = 3;
            actionPoints = 1;
            attackRange = 1;
            attack = 1;
            maxMovepoints = 3;
            currentMovepoints = maxMovepoints;
        }

        public override void DoAttackActionXZ(Character target)
        {
            target.DecreaseHP();
        }
    }
}