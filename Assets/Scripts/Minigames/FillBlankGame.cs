using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBlankGame : Minigame
{
    [SerializeField] private string pathToData = "FillBlank/";

    [SerializeField] private TextMeshProUGUI valueA;
    [SerializeField] private TextMeshProUGUI valueB;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;

    private FillBlankData _data;
    private bool answerIsCorrect = true;

    private void Start()
    {
        FillBlankData[] possibleQuestions = Resources.LoadAll<FillBlankData>(pathToData);
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
        valueA.text = $"<color=red>a = {_data.a}</color>";
        valueB.text = $"<color=green>b = {_data.b}</color>";
        submitButton.onClick.AddListener(() => SubmitAnswer(inputField.text));

        inputField.onValidateInput += (string text, int index, char addedChar) =>
        { 
            if (!int.TryParse(addedChar.ToString(), out _))
                return '\0';
            return addedChar;
        };
    }

    private void SubmitAnswer(string text)
    {
        if (int.TryParse(text, out int intValue))
        {
            CustomButton cb = submitButton.GetComponent<CustomButton>();

            if (intValue == _data.c)
            {
                cb.SetColor(Color.green);
                CompleteMinigame(answerIsCorrect, 0.5f);
            }
            else
            {
                cb.SetColor(Color.red);
                answerIsCorrect = false;
            }
        }
    }
}
