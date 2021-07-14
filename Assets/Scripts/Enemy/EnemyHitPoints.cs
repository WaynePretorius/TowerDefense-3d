using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPoints : MonoBehaviour
{
    //variables declared
    [Header("Enemy Hit Points Settings")]
    [SerializeField] private int maxHP = 50;
    [SerializeField] private int damage = 10;
    private int hitPoints = 50;

    private void OnEnable()
    {
        hitPoints = maxHP;
    }

    //when anything collides with the enemy
    private void OnParticleCollision(GameObject other)
    {
        DoDamage();
    }

    //do damage if the collision is true
    private void DoDamage()
    {
        hitPoints -= damage;
        CheckIfEnemyDies();
    }

    //check if the enemy is = or below 0 hp, and destroy if so
    private void CheckIfEnemyDies()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
