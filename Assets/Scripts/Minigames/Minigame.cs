using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private int xp;

    public void CompleteMinigame(bool isCorrect)
    {
        if (isCorrect)
        {
            Managers.ProgressionManager.Instance.AddXp(xp);
            Debug.Log("Correct answer received!");
        }
        else
            Debug.Log("Incorrect answer received!");

        QuizManager.Instance.LoadNextMinigame();
    }
}
