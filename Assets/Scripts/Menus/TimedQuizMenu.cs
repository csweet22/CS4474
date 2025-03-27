using Scripts.Utilities;
using UnityEngine;

public class TimedQuizMenu : QuizMenu
{
    public override void Open()
    {
        base.Open();

        if (TryGetComponent(out CountdownTimer timer))
        {
            timer.OnTimeOut += Managers.QuizManager.Instance.EndQuiz;
        }
        else
            Debug.LogError("Missing CountdownTimer component!");
    }
}
