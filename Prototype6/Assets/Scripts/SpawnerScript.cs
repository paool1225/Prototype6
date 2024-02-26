using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in inspector

    private float nextSpawnTime;

    [SerializeField] int spawnerNumber;

    private GameManager gameManager;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Set an initial random spawn time to stagger the start times of each spawner
        nextSpawnTime = Time.time + Random.Range(7f, 15f);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            // After spawning, set the next spawn time to a new random value
            nextSpawnTime = Time.time + Random.Range(5f, 15f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        EnemyMovement em = enemy.GetComponent<EnemyMovement>();
        em.enemySpawnerParent = spawnerNumber;
        gameManager.AddEnemy(em);
    }
}