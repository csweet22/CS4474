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
    
    protected void SetButtonColor(RectTransform button, Color c)
    {
        button.GetComponent<Image>().color = c;
    }

    protected void SetButtonColor(RectTransform button, Color c, float delay)
    {
        StartCoroutine(SetButtonColorWithDelay(button, c, delay));
    }

    private IEnumerator SetButtonColorWithDelay(RectTransform button, Color c, float delay)
    {
        Image img = button.GetComponent<Image>();
        Color originalColor = img.color;
        img.color = c;

        yield return new WaitForSeconds(delay);

        if (img)
            img.color = originalColor;
    }
}
