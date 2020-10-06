using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shooter
{
    public sealed class Player : Character
    {
        public int AttackDamage => attackDamage;
        public float AttackInterval => attackInterval;

        public void RestartLevel() //From death animation, unity
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        public void Regenerate(int amount)
        {
            CurrentHealth += amount;
            uiComponent.OnRegenerate();
        }

        protected override void OnInitialized()
        {
            inputComponent.Initialize(this);
            physicsComponent.Initialize(this);
            animationComponent.Initialize(this);
            firearmComponent.Initialize(this);
            audioComponent.Initialize(this);
            uiComponent.Initialize(this);

            buffsComponent.Initialize(this);
        }

        protected override void OnHurt(int damage)
        {
            uiComponent.OnHurt();
        }

        protected override void OnDeath()
        {
            animationComponent.OnDeath();
        }

        protected override void OnComponentUpdate()
        {
            animationComponent.OnUpdate(movement);
        }

        protected override void OnComponentFixedUpdate()
        {
            inputComponent.OnUpdate(ref movement, out rotation, out _isShoot);
            physicsComponent.OnUpdate(movement, rotation);
            firearmComponent.OnUpdate(_isShoot);
            audioComponent.OnUpdate(_isShoot);
        }

        [Header("Player's Component")]
        [SerializeField] private PlayerInput inputComponent = default;

        [SerializeField] private PlayerPhysics physicsComponent = default;
        [SerializeField] private PlayerAnimation animationComponent = default;
        [SerializeField] private PlayerUI uiComponent = default;
        [SerializeField] private PlayerSound audioComponent = default;
        [SerializeField] private PlayerFirearm firearmComponent = default;
        [SerializeField] private PlayerBuffs buffsComponent = default;

        [Space]
        [SerializeField] private int attackDamage = 20;

        [SerializeField] private float attackInterval = .15f;

        private bool _isShoot;
    }
}