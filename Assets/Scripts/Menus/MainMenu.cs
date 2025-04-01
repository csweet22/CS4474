using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : ACMenu
{
    [SerializeField] private Button exploreButton;

    [FormerlySerializedAs("explorerMenu")] [SerializeField]
    private GameObject exploreMenu;

    [SerializeField] private Button accountButton;
    [SerializeField] private GameObject accountMenu;

    [FormerlySerializedAs("quizButton")] [SerializeField] private Button quizSelectButton;
    [FormerlySerializedAs("quizMenu")] [SerializeField] private GameObject quizSelectMenu;
    
    void OnEnable()
    {
        exploreButton.onClick.AddListener(OnExploreClicked);
        accountButton.onClick.AddListener(OnAccountClicked);
        quizSelectButton.onClick.AddListener(OnQuizClicked);

        accountButton.gameObject.GetComponentInChildren<RawImage>().texture =
            AccountManager.Instance.ProfilePicture.Value;

        AccountManager.Instance.ProfilePicture.OnValueChanged += (texture2D, texture2D1) =>
        {
            accountButton.gameObject.GetComponentInChildren<RawImage>().texture =
                AccountManager.Instance.ProfilePicture.Value;
        };
    }

    private void OnAccountClicked()
    {
        MainCanvas.Instance.OpenMenu(accountMenu, Vector3.left);
    }

    private void OnExploreClicked()
    {
        MainCanvas.Instance.OpenMenu(exploreMenu, Vector3.up);
    }

    private void OnQuizClicked()
    {
        MainCanvas.Instance.OpenMenu(quizSelectMenu, Vector3.down);
    }

    private void OnDisable()
    {
        exploreButton.onClick.RemoveAllListeners();
        accountButton.onClick.RemoveAllListeners();
        quizSelectButton.onClick.RemoveAllListeners();
    }
}