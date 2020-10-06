using UnityEngine;

namespace Shooter.Scene
{
    public class SceneController : MonoBehaviour
    {
        public void Start()
        {
            _gameTrackerController = new GameTrackerController();

            cameraFollow.Initialize(_gameTrackerController);
            scoreUI.Initialize(_gameTrackerController);
            gameOverUI.Initialize(_gameTrackerController);
            enemySpawner.Initialize(_gameTrackerController);
            player.Initialize(_gameTrackerController);
            picker.Initialize(player);

            _gameTrackerController.StateEvent?.Invoke(GameStateEnum.Play);
        }

        [SerializeField] private CameraFollow cameraFollow = default;
        [SerializeField] private ScoreUI scoreUI = default;
        [SerializeField] private GameOverUI gameOverUI = default;
        [SerializeField] private EnemySpawner enemySpawner = default;
        [SerializeField] private Player player = default;
        [SerializeField] private PickerPowerUps picker = default;

        private GameTrackerController _gameTrackerController;
    }
}