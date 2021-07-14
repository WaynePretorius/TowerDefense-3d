using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //cached regerences for the towers
    [Header("Instantiate Towers")]
    [SerializeField] private GameObject playerTower;
    private GameObject parent;

    //states of the object
    [Header("Waypoint Settings")]
    [SerializeField] private bool canPlace;

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
    }

    //when the player clicks on the grid
    private void OnMouseDown()
    {
        PlaceTower();
    }

    private void PlaceTower()
    {
        if (canPlace)
        {
            canPlace = false;
            if (playerTower == null)
            {
                Debug.LogWarning("No Tower Prefab added.");
                return;
            }
            GameObject ballista = Instantiate(playerTower, transform.position, Quaternion.identity);
            ballista.transform.parent = parent.transform;
        }
    }
}
