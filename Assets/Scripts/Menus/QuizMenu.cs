using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMenu : ACMenu
{
    public override void Open()
    {
        base.Open();
        QuizManager.Instance.StartQuiz(transform);
    }
}
