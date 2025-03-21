using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchingData", menuName = "MinigameData/MatchingData", order = 2)]
public class MatchingData : ScriptableObject
{
    public string[] leftItems;
    public string[] rightItems;
}
