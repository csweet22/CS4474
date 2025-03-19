using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : ACMenu
{
    [SerializeField] private Button exploreButton;
    [SerializeField] private GameObject explorerMenu;


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
    }

    private void OnExploreClicked()
    {
        MainCanvas.Instance.OpenMenu(explorerMenu, Vector3.up, 1.0f);
    }

    private void OnDisable()
    {
        exploreButton.onClick.RemoveAllListeners();
    }
}