using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenu : ACMenu
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button resetButton;
    
    [SerializeField] private TMP_InputField displayNameField;
    
    [SerializeField] private GameObject resetConfirmation;
    
    public override void Open()
    {
        base.Open();

        AccountManager.Instance.DisplayName.OnValueChanged += (s, s1) =>
        {
            displayNameField.text = AccountManager.Instance.DisplayName.Value;
        };
        
        displayNameField.text = AccountManager.Instance.DisplayName.Value;
        displayNameField.onValueChanged.AddListener(OnDisplayNameChanged);
        backButton.onClick.AddListener(OnBackClicked);
        
        resetButton.onClick.AddListener(OnResetClicked);
        
    }

    private void OnResetClicked()
    {
        GameObject confirmation = Instantiate(resetConfirmation, transform);
        ConfirmationPopup confirmationPopup = confirmation.GetComponent<ConfirmationPopup>();
        confirmationPopup.OnConfirm += () =>
        {
            AccountManager.Instance.ResetAccount();
        };
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
        resetButton.onClick.RemoveAllListeners();
    }
}
