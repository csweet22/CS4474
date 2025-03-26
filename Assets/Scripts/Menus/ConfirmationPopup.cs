using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] private Button confirm;
    [SerializeField] private Button no;

    public event Action OnConfirm;
    public event Action OnNo;
    
    private void OnEnable()
    {
        confirm.onClick.AddListener(Confirm);
        no.onClick.AddListener(No);
    }

    private void No()
    {
        OnNo?.Invoke();
        Destroy(gameObject);
    }

    private void Confirm()
    {
        OnConfirm?.Invoke();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        confirm.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }
}
