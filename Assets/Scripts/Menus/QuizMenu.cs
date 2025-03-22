using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMenu : ACMenu
{
    public override void Open()
    {
        base.Open();
        Managers.QuizManager.Instance.StartQuiz(transform);
    }
}
