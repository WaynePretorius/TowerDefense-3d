using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //cached regerences for the towers
    [Header("Instantiate Towers")]
    [SerializeField] private GameObject playerTower;
    [SerializeField] private Ballista towerCost;
    private GameObject parent;
    private CurrencyKeeper bank;


    //states of the object
    [Header("Waypoint Settings")]
    [SerializeField] private bool canPlace;
    [SerializeField] private bool haveEnoughMoney;

    //Encapsulation functions
    public bool CanPlace
    {
        get{return canPlace;}
    }

    //first function that gets called as soon as the script is in the game
    private void Awake()
    {
        AwakeFunctions();
    }

    //calls what will happen in the awake function
    private void AwakeFunctions()
    {
        parent = GameObject.FindGameObjectWithTag(Tags.TAGS_PARENT);
        bank = FindObjectOfType<CurrencyKeeper>();
    }

    //when the player clicks on the grid
    private void OnMouseDown()
    {
        PlaceTower();
    }

    //function that check if the tower can be placed
    private void PlaceTower()
    {
        CheckBalance();
        if (canPlace && haveEnoughMoney)
        {
            canPlace = false;
            GameObject ballista = Instantiate(playerTower, transform.position, Quaternion.identity);
            ballista.transform.parent = parent.transform;
            bank.DeductBalanceValue(towerCost.TowerCost);
        }
    }

    //function that checks if there is enough cash in the bank
    private void CheckBalance()
    {
        if(bank.CurrentBalance >= towerCost.TowerCost)
        {
            haveEnoughMoney = true;
        }
        else
        {
            haveEnoughMoney = false;
        }
    }
}
