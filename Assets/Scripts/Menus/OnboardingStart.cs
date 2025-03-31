using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingStart : ACMenu
{
    [SerializeField] private Button profileButton;
    [SerializeField] private Button skipButton;
    
    [SerializeField] private GameObject pythagoreanExplanationMenu;
    [SerializeField] private GameObject accountMenu;
    
    public override void Open()
    {
        base.Open();
        
        profileButton.onClick.AddListener(OnProfileClick);
        skipButton.onClick.AddListener(OnSkipClick);
    }

    private void OnSkipClick()
    {
        MainCanvas.Instance.OpenMenu(pythagoreanExplanationMenu, Vector3.right);
    }

    private void OnProfileClick()
    {
        MainCanvas.Instance.OpenMenu(accountMenu, Vector3.right);
    }

    public override void Close()
    {
        base.Close();
        profileButton.onClick.RemoveAllListeners();
        skipButton.onClick.RemoveAllListeners();
    }
}