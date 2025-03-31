using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

    [SerializeField] private GameObject pythagoreanExplanation;

    [SerializeField] private TMP_Dropdown countryDropdown;

    [SerializeField] private bool isOnboarding = false;

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

        if (resetButton)
            resetButton.onClick.AddListener(OnResetClicked);

        var countryEnumValues = Enum.GetValues(typeof(Country)).Cast<Country>();
        var options = new List<TMP_Dropdown.OptionData>();
        foreach (var country in countryEnumValues){
            var description = GetEnumDescription(country);
            options.Add(new TMP_Dropdown.OptionData(description));
        }

        countryDropdown.AddOptions(options);
        countryDropdown.onValueChanged.AddListener(OnCountrySelected);

        countryDropdown.value = (int) AccountManager.Instance.UserCountry.Value;

        AccountManager.Instance.UserCountry.OnValueChanged += (s, a) =>
        {
            countryDropdown.value = (int) AccountManager.Instance.UserCountry.Value;
        };
    }

    private void OnCountrySelected(int arg0)
    {
        AccountManager.Instance.SetUserCountry((Country) arg0);
    }

    private string GetEnumDescription(Country country)
    {
        var field = country.GetType().GetField(country.ToString());
        var attribute = (DescriptionAttribute) Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute != null ? attribute.Description : country.ToString();
    }

    private void OnResetClicked()
    {
        GameObject confirmation = Instantiate(resetConfirmation, transform);
        ConfirmationPopup confirmationPopup = confirmation.GetComponent<ConfirmationPopup>();
        confirmationPopup.OnConfirm += () =>
        {
            AccountManager.Instance.ResetAccount();

            PFPButton[] buttons = FindObjectsOfType<PFPButton>();
            foreach (PFPButton pfpButton in buttons){
                pfpButton.UpdateStatus();
            }
        };
    }

    private void OnDisplayNameChanged(string newName)
    {
        AccountManager.Instance.SetDisplayName(newName);
    }

    private void OnBackClicked()
    {
        if (isOnboarding){
            MainCanvas.Instance.OpenMenu(pythagoreanExplanation, Vector3.right);
        }
        else{
            MainCanvas.Instance.CloseMenu();
        }
    }

    public override void Close()
    {
        base.Close();
        backButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
    }
}