using UnityEngine;

namespace Shooter
{
    public sealed class EnemyPhysics : MonoBehaviour
    {
        public void Initialize(Character self, Character target)
        {
            _self = self;
            _target = target;
        }

        public void OnUpdate(out bool isInAttackRange)
        {
            isInAttackRange = _isPlayerInAttackRange;
        }

        public void OnDeath(float destroyInSeconds)
        {
            enemyRigidbody.isKinematic = true;
            //circle collider? does we even need to set to true?

            Destroy(gameObject, destroyInSeconds);
        }

        public void StartSinking() //method that called within prefab animation, wp unity bad practice
        {
            InvokeRepeating("Sinking", 0, .1f); 
        }

#pragma warning disable IDE0051 // Remove unused private members
        private void Sinking()
#pragma warning restore IDE0051 // Remove unused private members
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_target != null &&
                other.gameObject == _target.gameObject)
            {
                _isPlayerInAttackRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_target != null &&
                other.gameObject == _target.gameObject)
            {
                _isPlayerInAttackRange = false;
            }
        }


        [SerializeField] private Rigidbody enemyRigidbody = default;
        [Tooltip("Represent of how fast the enemy fall through the floor after dead")] [SerializeField] private float sinkSpeed = 5f;

#pragma warning disable IDE0052 // Remove unread private members
        private Character _self;
#pragma warning restore IDE0052 // Remove unread private members

        private Character _target;

        private bool _isPlayerInAttackRange = false;
    }
}