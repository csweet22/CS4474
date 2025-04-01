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

    private void OnEnable()
    {
        continueButton.onClick.AddListener(OnContinueClick);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveAllListeners();
    }

    private void OnContinueClick()
    {
        continueButton.interactable = false;
        MainCanvas.Instance.OpenMenu(appExplanation, Vector3.right);
    }
}