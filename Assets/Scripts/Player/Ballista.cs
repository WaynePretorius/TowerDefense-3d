using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{

    //chached objects
    [SerializeField] private Transform weapon;
    private Transform enemy;
    private GameObject parent;

    //first function when the class comes into polay
    private void Awake()
    {
        enemy = GameObject.FindObjectOfType<EnemyMover>().transform;
        parent = GameObject.FindGameObjectWithTag(Tags.TAGS_PARENT);
    }

    //update every frame
    private void Update()
    {
        AimTurret();
    }

    //look at the enemy
    private void AimTurret()
    {
        if(weapon == null)
        {
            Debug.LogWarning("No TurretTop assigned");
            return;
        }
        weapon.transform.LookAt(enemy.transform);
    }

}
