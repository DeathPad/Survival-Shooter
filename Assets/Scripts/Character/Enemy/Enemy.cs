using Shooter.Scene;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public sealed class Enemy : Character
    {
        public int AttackDamage => attackDamage;
        public float AttackInterval => attackInterval;

        public int Score => scoreValue;

        public void Initialize(GameTrackerController gameTrackerController, Player player)
        {
            _player = player;
            Initialize(gameTrackerController); //Initialize base
        }

        protected override void OnInitialized()
        {
            navigationComponent.Initialize(this);
            physicsComponent.Initialize(this, _player);
            animationComponent.Initialize(this);
            attackComponent.Initialize(this, _player);
        }

        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            hitParticle.transform.position = hitPoint;
            TakeDamage(damage);
        }

        protected override void OnComponentUpdate()
        {
            navigationComponent.OnUpdate(_player.transform);
            physicsComponent.OnUpdate(out _isInAttackRange); //physics first
            attackComponent.OnUpdate(_isInAttackRange);
        }

        protected override void OnHurt(int damage)
        {
            hitParticle.Play();
        }

        protected override void OnDeath()
        {
            navigationComponent.OnDeath();
            animationComponent.OnDeath();
            physicsComponent.OnDeath(2f);

            bool _isDrop = Random.Range(0, 1f) <= .1f;
            if(_isDrop)
            {
                int _randomIndex = Random.Range(0, drops.Count);
                Instantiate(drops[_randomIndex], transform.position, Quaternion.identity);
            }
        }

        protected override void OnGameStateIsPlayerLose()
        {
            animationComponent.OnPlayerLose();
        }

        [Header("Enemy's Components")]
        [SerializeField] private EnemyNavigation navigationComponent = default;
        [SerializeField] private EnemyAnimation animationComponent = default;
        [SerializeField] private EnemyPhysics physicsComponent = default;
        [SerializeField] private EnemyAttack attackComponent = default;

        [Space]
        [SerializeField] private ParticleSystem hitParticle = default; //physics? animation?

        [Space]
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackInterval = .5f;
        [SerializeField] private int scoreValue = 1;

        [Space]
        [SerializeField] private List<GameObject> drops = default;
        private Player _player;

        private bool _isInAttackRange = false;
    }
}