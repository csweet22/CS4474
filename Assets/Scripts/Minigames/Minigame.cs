using System.Collections;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private int xp;
    private float delay = 0.5f;
    
    public void CompleteMinigame(bool isCorrect)
    {
        StartCoroutine(CompleteWithDelay(isCorrect));
    }

    private IEnumerator CompleteWithDelay(bool isCorrect)
    {
        yield return new WaitForSeconds(delay);
        
        if (isCorrect)
        {
            Managers.ProgressionManager.Instance.AddXp(xp);
        }
        Managers.QuizManager.Instance.LoadNextMinigame();
    }
}
