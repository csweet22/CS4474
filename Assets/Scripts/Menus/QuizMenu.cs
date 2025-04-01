using UnityEngine;
using UnityEngine.UI;

public class QuizMenu : ACMenu
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private GameObject hintText;

    [SerializeField] private GameObject exitConfirmation;

    public override void Open()
    {
        base.Open();
        Managers.QuizManager.Instance.StartQuiz(transform);
    }
    
    private void OnEnable()
    {
        backButton.interactable = true;
        backButton.onClick.AddListener(OnBackClicked);
        hintButton.onClick.AddListener(OnHintClicked);
    }

    private void OnBackClicked()
    {
        backButton.interactable = false;
        GameObject confirmation = Instantiate(exitConfirmation, transform);
        ConfirmationPopup confirmationPopup = confirmation.GetComponent<ConfirmationPopup>();
        confirmationPopup.OnConfirm += () => Managers.QuizManager.Instance.EndQuiz();
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
