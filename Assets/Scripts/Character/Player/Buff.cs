using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public abstract class Buff
    {
        public float Duration { get; private set; }

        public Buff(float duration)
        {
            Duration = duration;
        }

        public abstract void OnApply(Character character);
        public abstract void UnApply(Character character);
    }
}