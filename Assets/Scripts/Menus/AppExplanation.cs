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

    public override void Open()
    {
        base.Open();
        continueButton.onClick.AddListener(OnContinueClick);
    }

    private void OnContinueClick()
    {
        MainCanvas.Instance.OpenMenu(mainMenu, Vector3.right);
    }

    public override void Close()
    {
        base.Close();
        continueButton.onClick.RemoveAllListeners();
    }
}