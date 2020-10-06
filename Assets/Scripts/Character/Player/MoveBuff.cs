using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public sealed class MoveBuff : Buff
    {
        public MoveBuff() : base(DURATION_IN_SECONDS)
        {
        }

        public override void OnApply(Character character)
        {
            character.MovementSpeed.AddModifier(_statModifier);
        }

        public override void UnApply(Character character)
        {
            character.MovementSpeed.RemoveModifier(_statModifier);
        }

        private const float DURATION_IN_SECONDS = 3f;
        private const float MOVEMENT_BONUS = 2f;

        private StatsModifier _statModifier = new StatsModifier("move_buff", MOVEMENT_BONUS);
    }
}