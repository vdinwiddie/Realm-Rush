using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankUI : MonoBehaviour
{
    TMP_Text currentBalanceDisplay;
    Bank currentBalance;

    // Start is called before the first frame update
    void Start()
    {
        currentBalanceDisplay = GetComponent<TMP_Text>();
        currentBalance = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentBalanceString = currentBalance.CurrentBalance.ToString();
        currentBalanceDisplay.text = $"Gold: {currentBalanceString}";
    }
}
