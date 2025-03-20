using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenu : ACMenu
{
    [SerializeField] private Button backButton;
    
    [SerializeField] private TMP_InputField displayNameField;
    
    public override void Open()
    {
        base.Open();
        
        displayNameField.text = AccountManager.Instance.DisplayName.Value;
        displayNameField.onValueChanged.AddListener(OnDisplayNameChanged);
        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnDisplayNameChanged(string newName)
    {
        AccountManager.Instance.SetDisplayName(newName);
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
