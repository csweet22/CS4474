using System.Collections;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private int xp;
    
    public void CompleteMinigame(bool isCorrect, float delay = 0f)
    {
        StartCoroutine(CompleteWithDelay(isCorrect, delay));
    }

    private IEnumerator CompleteWithDelay(bool isCorrect, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (isCorrect)
        {
            Managers.ProgressionManager.Instance.AddXp(xp);
        }
        Managers.QuizManager.Instance.LoadNextMinigame();
    }
}
