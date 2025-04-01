using UnityEngine;
using UnityEngine.UI;

public class QuizSelectMenu : ACMenu
{
    [SerializeField] private Button backButton;

    [SerializeField] private Button quizButton;
    [SerializeField] private GameObject quizMenu;

    [SerializeField] private Button timedQuizButton;
    [SerializeField] private GameObject timedQuizMenu;

    void OnEnable()
    {
        backButton.interactable = true;
        backButton.onClick.AddListener(OnBackClicked);
        quizButton.onClick.AddListener(OnQuizClicked);
        timedQuizButton.onClick.AddListener(OnTimedQuizClicked);
    }
    
    private void OnBackClicked()
    {
        backButton.interactable = false;
        MainCanvas.Instance.CloseMenu();
    }

    private void OnQuizClicked()
    {
        MainCanvas.Instance.OpenMenu(quizMenu, Vector3.down);
    }

    private void OnTimedQuizClicked()
    {
        MainCanvas.Instance.OpenMenu(timedQuizMenu, Vector3.down);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        quizButton.onClick.RemoveAllListeners();
        timedQuizButton.onClick.RemoveAllListeners();
    }
}