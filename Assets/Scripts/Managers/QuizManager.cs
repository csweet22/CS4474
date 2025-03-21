using Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuizManager : PersistentSingleton<QuizManager>
{
    [SerializeField] private int numGames;
    [SerializeField] private List<Minigame> gamePrefabs;

    private Transform root;
    private Minigame[] _gameSequence;
    private int _currentGameIndex = 0;

    public Minigame CurrentGame { get; private set; } = null;

    public void StartQuiz(Transform root = null)
    {
        this.root = root ? root : transform;

        if (numGames > gamePrefabs.Count)
            numGames = gamePrefabs.Count;

        _gameSequence = new Minigame[numGames];
        for (int i = 0; i < numGames; i++)
        {
            Minigame next = gamePrefabs[Random.Range(0, gamePrefabs.Count)];
            _gameSequence[i] = next;
            gamePrefabs.Remove(next);
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
        Debug.Log("Quiz complete!");
    }   
}
