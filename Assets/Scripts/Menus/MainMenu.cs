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

    [SerializeField] private Button quizButton;
    [SerializeField] private GameObject quizMenu;

    [SerializeField] private Button timedQuizButton;
    [SerializeField] private GameObject timedQuizMenu;

    public override void Open()
    {
        base.Open();
    }

    public override void Returned()
    {
        base.Returned();
    }

    public override void Close()
    {
        base.Close();
    }

    void OnEnable()
    {
        exploreButton.onClick.AddListener(OnExploreClicked);
        accountButton.onClick.AddListener(OnAccountClicked);
        quizButton.onClick.AddListener(OnQuizClicked);
        timedQuizButton.onClick.AddListener(OnTimedQuizClicked);

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
        MainCanvas.Instance.OpenMenu(quizMenu, Vector3.down);
    }

    private void OnTimedQuizClicked()
    {
        MainCanvas.Instance.OpenMenu(timedQuizMenu, Vector3.down);
    }

    private void OnDisable()
    {
        exploreButton.onClick.RemoveAllListeners();
        accountButton.onClick.RemoveAllListeners();
        quizButton.onClick.RemoveAllListeners();
        timedQuizButton.onClick.RemoveAllListeners();
    }
}