using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PythagoreanExplanation : ACMenu
{
    [SerializeField] private Button continueButton;

    [SerializeField] private GameObject appExplanation;

    public override void Open()
    {
        base.Open();
        continueButton.onClick.AddListener(OnContinueClick);
    }

    private void OnContinueClick()
    {
        MainCanvas.Instance.OpenMenu(appExplanation, Vector3.right);
    }

    public override void Close()
    {
        base.Close();
        continueButton.onClick.RemoveAllListeners();
    }
}