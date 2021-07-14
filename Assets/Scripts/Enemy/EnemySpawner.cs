using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //variables declared
    [Header("Enemy Spawner Settings")]
    [SerializeField] private float timeToSpawn = 1f;
    [SerializeField] private int enemyPoolSize = 10;

    //cached references
    [Header("Enemies to respawn")]
    [SerializeField] private GameObject enemy;
    private GameObject[] enemyPool;

    private void Awake()
    {
        PopulatePool();
    }

    //populates the enemy pool and disables the enemies
    private void PopulatePool()
    {
        enemyPool = new GameObject[enemyPoolSize];

        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = Instantiate(enemy, transform);
            enemyPool[i].SetActive(false);
        }
    }

    // Called at the start of
    private void Start()
    {
           StartCoroutine(SpawnTimer());
    }

    //Timer for how long it takes before a new enemy spawns in
    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    //spawns in the enemy 
    private void SpawnEnemy()
    {
        for(int i = 0; i < enemyPool.Length; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }
}
