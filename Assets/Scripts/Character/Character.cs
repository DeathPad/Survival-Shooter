using Shooter.Scene;
using UnityEngine;

namespace Shooter
{
    public abstract class Character : MonoBehaviour
    {
        public int CurrentHealth { get; protected set; }
        public Stats MovementSpeed => movementStat;

        /// <summary>
        /// Setup character for ready to use
        /// </summary>
        /// <param name="gameTracker"></param>
        public void Initialize(GameTrackerController gameTracker)
        {
            _gameTrackerController = gameTracker;
            _gameState = _gameTrackerController.CurrentState;
            _gameTrackerController.StateEvent -= OnGameStateChangedEvent;
            _gameTrackerController.StateEvent += OnGameStateChangedEvent;

            CurrentHealth = startingHealth;            
            movementStat.AddModifier(new StatsModifier("base_movement", movementSpeed));

            OnInitialized();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            soundComponent.OnHurt(); //Why not put this inside OnHurt()? Its possible for someone to override OnHurt and forgot about this line.
            OnHurt(damage);

            if(CurrentHealth <= 0 && 
                !_isDead)
            {
                _isDead = true;

                soundComponent.OnDeath(); //same reason with above
                OnDeath();

                _gameTrackerController.CharacterDead(this); //?? desperate line; Inform tracker
            }
        }
        
        /// <summary>
        /// Called after initialize completed.<br></br>
        /// Used to initialize character's components
        /// </summary>
        protected virtual void OnInitialized() { }
        protected virtual void OnComponentUpdate() { }
        protected virtual void OnComponentFixedUpdate() { }

        /// <summary>
        /// Called right after damage calculation
        /// </summary>
        protected virtual void OnHurt(int damage) { }
        protected virtual void OnDeath() { }

        protected virtual void OnGameStateIsIdle() { }
        protected virtual void OnGameStateIsPlay() { }
        protected virtual void OnGameStateIsPlayerLose() { }

        private void Update()
        {
            if(_gameState is GameStateEnum.Play && 
                !_isDead)
            {
                OnComponentUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (_gameState is GameStateEnum.Play && 
                !_isDead)
            {
                //OnCheckHealth?
                OnComponentFixedUpdate();
                //OnDeath?
            }
        }

        private void OnGameStateChangedEvent(GameStateEnum gameState)
        {
            _gameState = gameState;
            switch (_gameState)
            {
                case GameStateEnum.Idle:
                    OnGameStateIsIdle();
                    break;

                case GameStateEnum.Play:
                    OnGameStateIsPlay();
                    break;

                case GameStateEnum.PlayerLose:
                    OnGameStateIsPlayerLose();
                    break;
            }
        }

        private void OnDestroy()
        {
            _gameTrackerController.StateEvent -= OnGameStateChangedEvent;
        }

        protected Vector3 movement = Vector3.zero;
        protected Quaternion rotation = Quaternion.identity;

        [SerializeField] private int startingHealth = 100;
        [SerializeField] private float movementSpeed = 6;

        [Header("Components")]
        [SerializeField] private CharacterSound soundComponent = default;

        private GameTrackerController _gameTrackerController;
        private GameStateEnum _gameState;

        protected readonly Stats movementStat = new Stats();
        private bool _isDead = false;
    }
}