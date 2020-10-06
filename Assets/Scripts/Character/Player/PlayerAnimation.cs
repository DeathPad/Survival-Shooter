using UnityEngine;

namespace Shooter
{
    /// <summary>
    /// Does animation need derived class? probably not
    /// </summary>
    public sealed class PlayerAnimation : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
        }

        public void OnUpdate(Vector3 movement)
        {
            _isWalking = movement.x != 0 || movement.z != 0;
            animator.SetBool("IsWalking", _isWalking);
        }

        public void OnDeath()
        {
            animator.SetTrigger("Die");
        }

#pragma warning disable IDE0052 // Remove unread private members
        private Character _character;
#pragma warning restore IDE0052 // Remove unread private members

        [SerializeField] private Animator animator = default;
        private bool _isWalking;
    }
}