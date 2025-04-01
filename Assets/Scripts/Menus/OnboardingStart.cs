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

    private void OnSkipClick()
    {
        skipButton.interactable = false;
        MainCanvas.Instance.OpenMenu(pythagoreanExplanationMenu, Vector3.right);
    }

    private void OnProfileClick()
    {
        profileButton.interactable = false;
        MainCanvas.Instance.OpenMenu(accountMenu, Vector3.right);
    }

    private void OnEnable()
    {
        profileButton.onClick.AddListener(OnProfileClick);
        skipButton.onClick.AddListener(OnSkipClick);
    }

    private void OnDisable()
    {
        profileButton.onClick.RemoveAllListeners();
        skipButton.onClick.RemoveAllListeners();
    }
}