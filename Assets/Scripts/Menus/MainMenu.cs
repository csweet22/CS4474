using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : ACMenu
{
    [SerializeField] private Button exploreButton;
    [FormerlySerializedAs("explorerMenu")] [SerializeField] private GameObject exploreMenu;
    
    [SerializeField] private Button accountButton;
    [SerializeField] private GameObject accountMenu;


    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    void OnEnable()
    {
        exploreButton.onClick.AddListener(OnExploreClicked);
        accountButton.onClick.AddListener(OnAccountClicked);
    }

    private void OnAccountClicked()
    {
        MainCanvas.Instance.OpenMenu(accountMenu, Vector3.left);
    }

    private void OnExploreClicked()
    {
        MainCanvas.Instance.OpenMenu(exploreMenu, Vector3.up);
    }

    private void OnDisable()
    {
        exploreButton.onClick.RemoveAllListeners();
        accountButton.onClick.RemoveAllListeners();
    }
}