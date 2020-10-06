using UnityEngine;

namespace Shooter
{
    public sealed class EnemyAnimation : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
        }

        public void OnDeath()
        {
            animator.SetTrigger("Dead");
        }

        public void OnPlayerLose()
        {
            animator.SetTrigger("PlayerDead");
        }

        [SerializeField] private Animator animator = default;

#pragma warning disable IDE0052 // Remove unread private members
        private Character _character;
#pragma warning restore IDE0052 // Remove unread private members
    }
}