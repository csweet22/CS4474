using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class XPBar : ProgressBar
{

    [SerializeField] private TextMeshProUGUI xpText;

    protected override void Start()
    {
        base.Start();
        
        ProgressionManager.Instance.OnXpGained += UpdateBar;
        ProgressionManager.Instance.OnLevelUp += UpdateBar;

        UpdateBar(0, 0);
    }

    private void UpdateBar(int obj)
    {
        UpdateBar(0, 0);
    }

    private void UpdateBar(int oldValue, int newValue)
    {
        float originalValue = Progress.Value;
        max.Value = ProgressionManager.Instance.RequiredXp;
        Progress.Value = ProgressionManager.Instance.Xp;
        xpText.text = $"{Progress.Value} / {ProgressionManager.Instance.RequiredXp} XP";
        ProgressUpdate(originalValue, Progress.Value);
    }
}