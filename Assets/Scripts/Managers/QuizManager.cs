using Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        List<Minigame> possibleGames = new(gamePrefabs);

        if (numGames > possibleGames.Count)
            numGames = possibleGames.Count;

        _gameSequence = new Minigame[numGames];
        for (int i = 0; i < numGames; i++)
        {
            Minigame next = possibleGames[Random.Range(0, possibleGames.Count)];
            _gameSequence[i] = next;
            possibleGames.Remove(next);
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

    private void EndQuiz()
    {
        root = null;
        _gameSequence = null;
        _currentGameIndex = 0;

        MainCanvas.Instance.CloseMenu();
    }   
}
