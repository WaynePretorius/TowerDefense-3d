using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    //variables declared
    [Header("Turret variable")]
    [SerializeField] private float turretRange = 15f;
    [SerializeField] private int towerCost = 100;

    //chached objects
    [Header("Gameobjects Needed")]
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectile;
    private Transform enemyPosition;

    //getters and setter
    public int TowerCost { get { return towerCost; } }

    //first function that caomes into play as soon as the class is activated
    private void Awake()
    {
        enemyPosition = FindObjectOfType<Enemy>().transform;
    }

    //update every frame
    private void Update()
    {
        FindClosestTarget();
        AimTurret();
    }

    //finds the closest target to the turret
    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        enemyPosition = closestTarget;
    }

    //look at the closest enemy and attack if they are in range
    private void AimTurret()
    {
        float maxDistance = Vector3.Distance(transform.position, enemyPosition.position);

        weapon.transform.LookAt(enemyPosition);

        if(maxDistance < turretRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    //enable or disable the turret attacking
    private void Attack(bool canAttack)
    {
        var emmissionState = projectile.emission;
        emmissionState.enabled = canAttack;
    }

}
