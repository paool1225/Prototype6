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
        nextSpawnTime = Time.time + Random.Range(2f, 30f);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            // After spawning, set the next spawn time to a new random value
            nextSpawnTime = Time.time + Random.Range(30f, 80f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        EnemyMovement em = enemy.GetComponent<EnemyMovement>();
        em.enemySpawnerParent = spawnerNumber; // tell enemy which node it originated from
        gameManager.AddEnemy(enemy); // add to list of all active enemies in GameManager
        gameManager.numberOfActiveEnemies++;
        Debug.Log(gameManager.numberOfActiveEnemies);

    }
}