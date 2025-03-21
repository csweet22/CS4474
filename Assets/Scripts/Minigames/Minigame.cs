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
        }
        QuizManager.Instance.LoadNextMinigame();
    }
}
