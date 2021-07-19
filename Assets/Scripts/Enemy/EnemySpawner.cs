using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //variables declared
    [Header("Enemy Spawner Settings")]
    [Tooltip("Range between 1 and 7 seconds before next enemy spawns")]
    [SerializeField] [Range(1f,7f)] private float timeToSpawn = 1f;
    [SerializeField] [Range(1,20)] private int enemyPoolSize = 10;

    //cached references
    [Header("Enemies to respawn")]
    [SerializeField] private GameObject enemy;
    private GameObject[] enemyPool;

    //first function that is called as soon as the gameobject comes into play
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
