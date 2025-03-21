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
        QuizManager.Instance.LoadNextMinigame();
    }
    
    protected void SetButtonColor(RectTransform button, Color c)
    {
        button.GetComponent<Image>().color = c;
    }
}
