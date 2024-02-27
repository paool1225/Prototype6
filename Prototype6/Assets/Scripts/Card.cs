using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public int cardId;
    public int cardPowerLevel;
    private GameManager gameManager;
    private GameTimer gameTimer;
    public bool hasBeenUsed = false;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void PlayCard(int nodeToUpdate) // play the card
    {
        hasBeenUsed = true; // effectively put into used pile
        gameManager.goodDeckSize--;
        gameManager.badDeckSize--;

        switch(cardId)
        {
            case 0: // good
                PlayCardPushBack(nodeToUpdate);
                break;
            case 1:
                PlayCardElectrify(nodeToUpdate);
                break;
            case 2:
                PlayCardElectrifyAll();
                break;
            case 3:
                PlayCardFreeze(nodeToUpdate);
                break;
            case 4:
                PlayCardSnip(nodeToUpdate);
                break;
            case 5: // bad
                PlayCardIncreaseSpeed(nodeToUpdate);
                break;
            case 6:
                PlayCardDoubleDamage(nodeToUpdate);
                break;
            case 7:
                PlayCardAdd5Seconds();
                break;
        }

        audioSource.Play();
    }

    public void PlayCardPushBack(int nodeToUpdate)
    {
        //Debug.Log("playing card: push back");

        // check for enemies on node where player dropped card
        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log("Enemies to push back: " + enemiesOnNode.Count.ToString());

        if (enemiesOnNode.Count > 0)
        {
            for(int i = 0; i < enemiesOnNode.Count; i++)
            {
                enemiesOnNode[i].GetComponent<EnemyMovement>().PushBack(); // for now, pushes back all enemies on node to starting position
            }
        }
    }
    public void PlayCardElectrify(int nodeToUpdate)
    {
        //Debug.Log("playing card: electrify");

        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log($"Electrifying {enemiesOnNode.Count} enemies on node {nodeToUpdate + 1}.");

        for (int i = enemiesOnNode.Count - 1; i >= 0; i--)
        {
            // Call the new DestroyEnemy method
            DestroyImmediate(enemiesOnNode[i]); // destroy enemy
            gameManager.numberOfActiveEnemies--;
        }
        enemiesOnNode.Clear(); // remove from enemy list on node
    }


    public void PlayCardElectrifyAll()
    {
        //Debug.Log("playing card: electrify all");


        // Iterate through all nodes
        for (int nodeIndex = 0; nodeIndex < gameManager.enemies.Length; nodeIndex++)
        {
            List<GameObject> enemiesOnNode = gameManager.enemies[nodeIndex];

            // Iterate backwards through the list to safely remove elements while iterating
            for (int i = enemiesOnNode.Count - 1; i >= 0; i--)
            {
                // Assuming Destroy(gameObject) is sufficient for cleanup, otherwise call a specific cleanup method
                DestroyImmediate(enemiesOnNode[i].gameObject);

                // Optionally remove the enemy from the list, if you handle this in the OnDestroy method of EnemyMovement, this is not needed
                enemiesOnNode.RemoveAt(i);

                gameManager.numberOfActiveEnemies--;
            }
        }

        //Debug.Log("All enemies have been electrified.");

    }


    public void PlayCardFreeze(int nodeToUpdate)
    {
        //Debug.Log("Playing card: freeze");

        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log($"Freezing {enemiesOnNode.Count} enemies on node {nodeToUpdate + 1}.");

        float freezeDuration = 5f; // Example duration, adjust as needed

        foreach (var enemy in enemiesOnNode)
        {
            enemy.GetComponent<EnemyMovement>().Freeze(freezeDuration);
        }
    }


    public void PlayCardSnip(int nodeToUpdate)
    {
        //Debug.Log("playing card: snip");
        GameObject node = gameManager.nodes[nodeToUpdate];
        if (node) // if the node is still alive
        {
            DestroyImmediate(node); // destroy node, spawner, and collider -> so no trigger when card is dropped on spire
            gameManager.nodes[nodeToUpdate] = null;
        }

        // check to see if last node snipped, then game end
        bool allRemoved = true;
        for (int i = 0; i < gameManager.nodes.Length; i++)
        {   
            if (gameManager.nodes[i] != null)
            {
                allRemoved = false;
            }
        }

        if (allRemoved)
        {
            //Debug.Log("Won from snipping all nodes");
            SceneManager.LoadScene("GameWinScene"); // Load the Game Win scene
        }

        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log("Enemies to remove: " + enemiesOnNode.Count.ToString());
        if (enemiesOnNode.Count > 0)
        {
            for (int i = 0; i < enemiesOnNode.Count; i++)
            {
                DestroyImmediate(enemiesOnNode[i]); // Destroy enemies 
                gameManager.numberOfActiveEnemies--;
             }
        }
        enemiesOnNode.Clear();

    }

    public void PlayCardIncreaseSpeed(int nodeToUpdate)
    {
        //Debug.Log("playing card: increase enemy speed");

        // check for enemies on node where player dropped card
        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log("Enemies to increase speed: " + enemiesOnNode.Count.ToString());

        if (enemiesOnNode.Count > 0)
        {
            for (int i = 0; i < enemiesOnNode.Count; i++)
            {
                enemiesOnNode[i].GetComponent<EnemyMovement>().IncreaseSpeed(2); // increase speed by input factor 
            }
        }
    }

    public void PlayCardDoubleDamage(int nodeToUpdate)
    {
        //Debug.Log("playing card: double damage");
        // check for enemies on node where player dropped card
        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log("Enemies to add double damage to: " + enemiesOnNode.Count.ToString());

        if (enemiesOnNode.Count > 0)
        {
            for (int i = 0; i < enemiesOnNode.Count; i++)
            {
                enemiesOnNode[i].GetComponent<EnemyMovement>().DoubleDamge(); // increase speed by input factor 
                //Debug.Log("new damage on enemy-" + i.ToString() + enemiesOnNode[i].GetComponent<EnemyMovement>().damage.ToString());
            }
        }
    }

    public void PlayCardAdd5Seconds()
    {
        //Debug.Log("playing card: add 5 seconds");
        gameTimer.AddTimeToTimer(5f);
    }
}
