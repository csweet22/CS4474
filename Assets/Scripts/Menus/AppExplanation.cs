using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppExplanation : ACMenu
{
    [SerializeField] private Button continueButton;

    [SerializeField] private GameObject mainMenu;

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
        MainCanvas.Instance.OpenMenu(mainMenu, Vector3.right);
    }
}