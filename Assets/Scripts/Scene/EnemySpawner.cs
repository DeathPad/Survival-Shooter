using System.Collections;
using UnityEngine;

namespace Shooter.Scene
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        public void Initialize(GameTrackerController gameTrackerController)
        {
            _gameTrackerController = gameTrackerController;
            _gameTrackerController.StateEvent -= GameStateChangedEvent;
            _gameTrackerController.StateEvent += GameStateChangedEvent;

            _enemyCount = enemyArray.Length;
        }

        private void GameStateChangedEvent(GameStateEnum gameState)
        {
            _gameStateEnum = gameState;
            switch (_gameStateEnum)
            {
                case GameStateEnum.Play:
                    StartCoroutine(SpawnEnemyCoroutine());
                    break;

                case GameStateEnum.Paused:
                    StopCoroutine(SpawnEnemyCoroutine());
                    break;

                case GameStateEnum.Idle:
                case GameStateEnum.PlayerLose:
                    StopCoroutine(SpawnEnemyCoroutine());
                    _timer = 0f;
                    break;
            }
        }

        private IEnumerator SpawnEnemyCoroutine()
        {
            while (_gameStateEnum == GameStateEnum.Play)
            {
                _timer += Time.deltaTime;
                if (_timer >= SPAWN_TIME)
                {
                    _timer = 0f;

                    int _spawnPointIndex = Random.Range(0, spawnPoints.Length);
                    int _spawnEnemy = Random.Range(0, _enemyCount);

                    Enemy _enemy = Instantiate(enemyArray[_spawnEnemy], spawnPoints[_spawnPointIndex].position, Quaternion.identity);
                    _enemy.Initialize(_gameTrackerController, player);
                }

                yield return null;
            }
        }

        private void OnDestroy()
        {
            _gameTrackerController.StateEvent -= GameStateChangedEvent;
        }

        [SerializeField] private Transform[] spawnPoints = default;
        [SerializeField] private Enemy[] enemyArray = default;
        [SerializeField] private Player player = default;

        private GameTrackerController _gameTrackerController;
        private GameStateEnum _gameStateEnum = GameStateEnum.Idle;

        private const float SPAWN_TIME = 3f;
        private float _timer = 0f;

        private int _enemyCount;
    }
}