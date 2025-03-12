using Scripts.Utilities;
using UnityEngine;

public class QuizManager : Singleton<QuizManager>
{
    [SerializeField] private int numGames;
    [SerializeField] private Minigame[] gamePrefabs;

    private Minigame[] _gameSequence;
    private int _currentGameIndex = 0;

    public Minigame CurrentGame { get; private set; } = null;

    private void Start()
    {
        _gameSequence = new Minigame[numGames];
        for (int i = 0; i < numGames; i++)
        {
            _gameSequence[i] = gamePrefabs[Random.Range(0, gamePrefabs.Length)];
        }

        LoadNextMinigame();
    }

    public void LoadNextMinigame()
    {
        if (CurrentGame)
        {
            Destroy(CurrentGame.gameObject);
        }

        if (_currentGameIndex < numGames)
        {
            CurrentGame = Instantiate(_gameSequence[_currentGameIndex], transform);
            _currentGameIndex++;
        }
        else
        {
            EndQuiz();
        }
    }

    private void EndQuiz()
    {
        Debug.Log("Quiz complete!");
    }   
}
