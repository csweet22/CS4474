using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceGame : Minigame
{
    [SerializeField] private string pathToData = "MultipleChoice/";
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private RectTransform[] answerButtons;

    private MultipleChoiceData _data;

    private bool answerIsCorrect = true;

    private void Start()
    {
        MultipleChoiceData[] possibleQuestions = Resources.LoadAll<MultipleChoiceData>(pathToData);
        if (possibleQuestions.Length == 0)
        {
            Debug.LogError("No questions found in " + pathToData);
            return;
        }

        _data = possibleQuestions[Random.Range(0, possibleQuestions.Length)];
        Setup();
    }

    private void Setup()
    {
        questionText.text = _data.question;
        for (int i = 0; i < _data.answers.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = _data.answers[i];
            if (i == _data.correctAnswerIndex)
            {
                button.onClick.AddListener(() => SelectButton((RectTransform) button.transform, true));
            }
            else
            {
                button.GetComponent<Button>().onClick.AddListener(() => SelectButton((RectTransform)button.transform, false));
            }
        }
    }

    private void SelectButton(RectTransform button, bool correct)
    {
        if (correct)
        {
            SetButtonColor(button, Color.green);
            CompleteMinigame(answerIsCorrect);
        }
        else
        {
            SetButtonColor(button, Color.red);
            answerIsCorrect = false;
        }
    }
}
