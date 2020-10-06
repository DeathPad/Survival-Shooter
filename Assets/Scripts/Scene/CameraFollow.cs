using Shooter.Scene;
using UnityEngine;

namespace Shooter.Scene
{
    public sealed class CameraFollow : MonoBehaviour
    {
        public void Initialize(GameTrackerController gameTracker)
        {
            _gameTrackerController = gameTracker;
            _gameTrackerController.StateEvent -= GameStateChangedEvent;
            _gameTrackerController.StateEvent += GameStateChangedEvent;

            _offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            if (_gameState is GameStateEnum.Play)
            {
                _targetCameraPosition = target.position + _offset;
                transform.position = Vector3.Lerp(transform.position, _targetCameraPosition, smoothing * Time.fixedDeltaTime);
            }
        }

        private void GameStateChangedEvent(GameStateEnum gameStateEnum)
        {
            _gameState = gameStateEnum;
        }

        private void OnDestroy()
        {
            _gameTrackerController.StateEvent -= GameStateChangedEvent;
        }

        [SerializeField] private Transform target = default;
        [SerializeField] private float smoothing = 5f;

        private GameTrackerController _gameTrackerController;
        private GameStateEnum _gameState = GameStateEnum.Idle;

        private Vector3 _targetCameraPosition;
        private Vector3 _offset;
    }
}