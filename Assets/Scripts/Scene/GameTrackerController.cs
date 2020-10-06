using Shooter.Event;

namespace Shooter.Scene
{
    public class GameTrackerController : IScoreController
    {
        public GameStateEvent StateEvent;

        public event UpdateScoreEvent UpdateScoreEvent;

        public GameStateEnum CurrentState { get; private set; }

        public GameTrackerController()
        {
            StateEvent -= GameStateChangedEvent;
            StateEvent += GameStateChangedEvent;
        }

        public void CharacterDead(Character character)
        {
            if (character.GetType() == typeof(Player))
            {
                StateEvent?.Invoke(GameStateEnum.PlayerLose);
                return;
            }

            _currentScore += (character as Enemy).Score;
            UpdateScoreEvent?.Invoke(_currentScore);
        }

        private void GameStateChangedEvent(GameStateEnum gameState)
        {
            CurrentState = gameState;
            switch (gameState)
            {
                case GameStateEnum.Idle:
                    break;

                case GameStateEnum.Play:
                    _currentScore = 0;
                    UpdateScoreEvent?.Invoke(_currentScore);
                    break;

            }
        }

        ~GameTrackerController()
        {
            StateEvent -= GameStateChangedEvent;
        }

        private int _currentScore;
    }
}