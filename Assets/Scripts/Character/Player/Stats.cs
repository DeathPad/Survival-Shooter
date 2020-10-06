using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class Stats
    {
        public float GetValue()
        {
            return _totalValue;
        }

        public void AddModifier(StatsModifier statsModifier)
        {
            _statsModifiers.Add(statsModifier);
            _totalValue += statsModifier.Value;
        }

        public void RemoveModifier(StatsModifier statsModifier)
        {
            _statsModifiers.Remove(statsModifier);
            _totalValue -= statsModifier.Value;
        }

        private List<StatsModifier> _statsModifiers = new List<StatsModifier>();
        private float _totalValue;
    }
}