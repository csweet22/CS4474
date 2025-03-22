using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMenu : ACMenu
{
    [SerializeField] private Button backButton;

    public override void Open()
    {
        base.Open();
        Managers.QuizManager.Instance.StartQuiz(transform);
    }
    
    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        Managers.QuizManager.Instance.EndQuiz();
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }
}
