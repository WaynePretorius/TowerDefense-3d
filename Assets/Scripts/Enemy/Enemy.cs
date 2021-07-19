using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //variables declared for later use in the class
    [Header("Reward Settings")]
    [SerializeField] private int currencyGained = 50;
    [SerializeField] private int currencyLost = 100;

    //chached Reference
    private CurrencyKeeper currencyManager;

    //first function that gets called when the class is opened
    private void Awake()
    {
        AwakeReferenceFunctions();
    }

    //declares all the cached referneces to the class
    private void AwakeReferenceFunctions()
    {
        currencyManager = FindObjectOfType<CurrencyKeeper>();
    }

    //adds currency to the amount
    public void AddsCurrency()
    {
        currencyManager.AddBalanceValue(currencyGained);
    }

    //deducts currency to the amount
    public void DeductCurrency()
    {
        currencyManager.DeductBalanceValue(currencyLost);
    }
}
