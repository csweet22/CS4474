using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultipleChoiceData", menuName = "MinigameData/MultipleChoiceData", order = 1)]
public class MultipleChoiceData : ScriptableObject
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
}
