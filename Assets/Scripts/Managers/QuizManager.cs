using Scripts.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class QuizManager : PersistentSingleton<QuizManager>
    {
        [SerializeField] private int numGames;
        [SerializeField] private Minigame[] gamePrefabs;

        private Transform root;
        private Minigame[] _gameSequence;
        private int _currentGameIndex = 0;

        public Minigame CurrentGame { get; private set; } = null;

        public void StartQuiz(Transform root = null)
        {
            this.root = root ? root : transform;

            _gameSequence = new Minigame[numGames];
            for (int i = 0; i < numGames; i++)
            {
                _gameSequence[i] = gamePrefabs[Random.Range(0, gamePrefabs.Length)];
            }

            LoadNextMinigame();
        }

        public void LoadNextMinigame()
        {
            if (_currentGameIndex < numGames)
            {
                if (CurrentGame)
                {
                    Destroy(CurrentGame.gameObject);
                }

                CurrentGame = Instantiate(_gameSequence[_currentGameIndex], root);
                _currentGameIndex++;
            }
            else
            {
                EndQuiz();
            }
        }

        public void EndQuiz()
        {
            root = null;
            _gameSequence = null;
            _currentGameIndex = 0;

            MainCanvas.Instance.CloseMenu();
        }
    }
}
