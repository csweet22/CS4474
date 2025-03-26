using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class PFPButton : MonoBehaviour
{
    private Button _button;

    public RawImage image;

    private void OnEnable()
    {
        _button = GetComponentInChildren<Button>();
        image = GetComponentInChildren<RawImage>();
        _button.onClick.AddListener(OnClick);

        UpdateStatus();
    }

    public void UpdateStatus()
    {
        image.color = AccountManager.Instance.ProfilePicture.Value == (Texture2D) image.texture
            ? Color.white
            : Color.gray;
    }

    private void OnClick()
    {
        PFPButton[] buttons = GameObject.FindObjectsOfType<PFPButton>();
        foreach (PFPButton pfpButton in buttons){
            pfpButton.image.color = Color.gray;
        }

        image.color = Color.white;
        AccountManager.Instance.SetProfilePicture((Texture2D) image.texture);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}