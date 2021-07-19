using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrencyKeeper : MonoBehaviour
{
    //variables declared for use during the course of the class
    [Header("Balance Settings")]
    [SerializeField] private int startingBalance = 200;
    [SerializeField] private int currentBalance;

    //cached references
    [SerializeField] TMP_Text scoreText;

    //getters and setters
    public int CurrentBalance { get { return currentBalance; } }

    //first function that gets called
    private void Awake()
    {
        SetBalance();
    }

    //called once per frame
    private void Update()
    {
        scoreText.text = currentBalance.ToString();
    }

    //Sets the starting balance of the player
    private void SetBalance()
    {
        currentBalance = startingBalance;
    }

    //adds the value of currentbalance to see how much the player has
    public void AddBalanceValue(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    //deducts the from the currentbalance
    public void DeductBalanceValue(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if (currentBalance < 0)
        {
            int index = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(index);
        }
    }

}
