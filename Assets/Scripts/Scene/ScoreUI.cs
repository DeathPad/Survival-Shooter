using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Scene
{
    public sealed class ScoreUI : MonoBehaviour
    {
        public void Initialize(IScoreController scoreController)
        {
            _scoreController = scoreController;
            _scoreController.UpdateScoreEvent -= OnScoreUpdated;
            _scoreController.UpdateScoreEvent += OnScoreUpdated;
        }

        private void OnScoreUpdated(int increaseTo)
        {
            scoreText.text = increaseTo.ToString();
        }

        private void OnDestroy()
        {
            _scoreController.UpdateScoreEvent -= OnScoreUpdated;
        }

        [SerializeField] private Text scoreText = default;

        private IScoreController _scoreController;
    }
}