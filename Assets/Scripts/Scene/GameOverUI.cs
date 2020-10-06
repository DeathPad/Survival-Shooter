using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Scene
{
    public sealed class GameOverUI : MonoBehaviour
    { 
        public void Initialize(GameTrackerController gameTrackerController)
        {
            _gameTrackerController = gameTrackerController;
            _gameTrackerController.StateEvent -= GameStateChangedEvent;
            _gameTrackerController.StateEvent += GameStateChangedEvent;
        }

        private void GameStateChangedEvent(GameStateEnum gameState)
        {
            if(gameState == GameStateEnum.PlayerLose)
            {
                animator.SetTrigger("GameOver");
            }
        }

        private void OnDestroy()
        {
            _gameTrackerController.StateEvent += GameStateChangedEvent;
        }

        [SerializeField] private Animator animator = default;

        private GameTrackerController _gameTrackerController;
    }
}