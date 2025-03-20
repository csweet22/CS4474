using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenu : ACMenu
{
    [SerializeField] private Button backButton;
    
    public override void Open()
    {
        base.Open();
        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        MainCanvas.Instance.CloseMenu();
    }

    public override void Close()
    {
        base.Close();
        backButton.onClick.RemoveAllListeners();
    }
}
