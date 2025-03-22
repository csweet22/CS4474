using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private int xp;
    
    public void CompleteMinigame(bool isCorrect)
    {
        if (isCorrect)
        {
            Managers.ProgressionManager.Instance.AddXp(xp);
        }
        Managers.QuizManager.Instance.LoadNextMinigame();
    }
}
