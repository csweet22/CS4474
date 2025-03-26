using UnityEngine;
using UnityEngine.UI;

public class QuizMenu : ACMenu
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private GameObject hintText;

    public override void Open()
    {
        base.Open();
        Managers.QuizManager.Instance.StartQuiz(transform);
    }
    
    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackClicked);
        hintButton.onClick.AddListener(OnHintClicked);
    }

    private void OnBackClicked()
    {
        Managers.QuizManager.Instance.EndQuiz();
    }
    
    private void OnHintClicked()
    {
        hintText.SetActive(!hintText.activeSelf);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        hintButton.onClick.RemoveAllListeners();
    }
}
