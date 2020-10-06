using UnityEngine;
using UnityEngine.AI;

namespace Shooter
{
    public class EnemyNavigation : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
            navigationAgent.speed = _character.MovementSpeed.GetValue();
        }

        public void OnUpdate(Transform target)
        {
            navigationAgent.SetDestination(target.position);
        }

        public void OnDeath()
        {
            navigationAgent.enabled = false;
        }

        [SerializeField] private NavMeshAgent navigationAgent = default;

        private Character _character;
    }
}