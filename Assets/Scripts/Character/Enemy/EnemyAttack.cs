using UnityEngine;

namespace Shooter
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        public void Initialize(Enemy self, Character target)
        {
            _self = self;
            _target = target;
        }

        public void OnUpdate(bool isInAttackRange)
        {
            _timer += Time.deltaTime;

            if (_timer >= _self.AttackInterval &&
                isInAttackRange)
            {
                _timer = 0f;
                _target.TakeDamage(_self.AttackDamage);
            }
        }

        private Enemy _self;
        private Character _target;

        private float _timer;
    }
}