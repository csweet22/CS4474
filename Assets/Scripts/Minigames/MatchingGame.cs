using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingGame : Minigame
{
    [SerializeField] private string pathToData = "Matching/";
    [SerializeField] private RectTransform buttonPrefab;
    [SerializeField] private RectTransform leftContainer;
    [SerializeField] private RectTransform rightContainer;

    private MatchingData _data;
    private bool answerIsCorrect = true;

    private readonly Dictionary<string, string> pairs = new();
    private RectTransform selectedLeft = null;
    private RectTransform selectedRight = null;

    private void Start()
    {
        MatchingData[] possibleQuestions = Resources.LoadAll<MatchingData>(pathToData);
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
        if (_data.leftItems.Length != _data.rightItems.Length)
        {
            Debug.LogError("Left and right items are not the same length!");
            return;
        }

        RectTransform[] buttonsLeft = new RectTransform[_data.leftItems.Length];
        RectTransform[] buttonsRight = new RectTransform[_data.rightItems.Length];

        // Setup buttons
        for (int i = 0; i < _data.leftItems.Length; i++)
        {
            string left = _data.leftItems[i];
            RectTransform buttonLeft = Instantiate(buttonPrefab, transform);
            buttonLeft.GetComponentInChildren<TextMeshProUGUI>().text = left;
            buttonLeft.GetComponent<Button>().onClick.AddListener(() => SelectButton(buttonLeft, false));


            string right = _data.rightItems[i];
            RectTransform buttonRight = Instantiate(buttonPrefab, transform);
            buttonRight.GetComponentInChildren<TextMeshProUGUI>().text = right;
            buttonRight.GetComponent<Button>().onClick.AddListener(() => SelectButton(buttonRight, true));

            buttonsLeft[i] = buttonLeft;
            buttonsRight[i] = buttonRight;

            // Add pairs to dict
            pairs.Add(left, right);
        }

        // Shuffle buttons and correctly add to scene
        StaticHelpers.ShuffleArray(ref buttonsLeft);
        StaticHelpers.ShuffleArray(ref buttonsRight);

        for (int i = 0; i < buttonsLeft.Length; i++)
        {
            buttonsLeft[i].SetParent(leftContainer);
        }
        for (int i = 0; i < buttonsRight.Length; i++)
        {
            buttonsRight[i].SetParent(rightContainer);
        }
    }

    private void SelectButton(RectTransform button, bool right)
    {
        // Right side button selected
        if (right)
        {
            if (selectedRight)
                selectedRight.GetComponent<CustomButton>().ResetColor();

            selectedRight = button;
            selectedRight.GetComponent<CustomButton>().SetColor(Color.green);
        }
        // Left side button selected
        else
        {
            if (selectedLeft)
                selectedLeft.GetComponent<CustomButton>().ResetColor();

            selectedLeft = button;
            selectedLeft.GetComponent<CustomButton>().SetColor(Color.green);
        }

        CheckPair();
    }

    private void CheckPair()
    {
        if (selectedLeft == null || selectedRight == null)
            return;

        string leftValue = selectedLeft.GetComponentInChildren<TextMeshProUGUI>().text;
        string rightValue = selectedRight.GetComponentInChildren<TextMeshProUGUI>().text;

        // Get correct pair from dictionary and test button values
        if (pairs.TryGetValue(leftValue, out string correctRight))
        {
            if (rightValue == correctRight)
            {
                pairs.Remove(leftValue);

                selectedLeft.GetComponent<CustomButton>().SetColor(Color.green, 0.5f, true);
                selectedRight.GetComponent<CustomButton>().SetColor(Color.green, 0.5f, true);
            }
            else
            {
                answerIsCorrect = false;

                selectedLeft.GetComponent<CustomButton>().SetColor(Color.red, 0.5f);
                selectedRight.GetComponent<CustomButton>().SetColor(Color.red, 0.5f);
            }

            selectedLeft = selectedRight = null;
        }
        else
            Debug.LogError("Could not find pair in dictionary!");

        if (pairs.Count == 0)
            CompleteMinigame(answerIsCorrect, 0.5f);
    }
}
