using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    public int cardId;
    public Vector3 ogPosition;
    private GameManager gameManager;
    private GameTimer gameTimer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameTimer = FindObjectOfType<GameTimer>();
        ogPosition = transform.position;
    }

    public void PlayCard(int nodeToUpdate) // play the card
    {
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

    }

    public void PlayCardElectrifyAll()
    {
        //Debug.Log("playing card: electrify all");
    }

    public void PlayCardFreeze(int nodeToUpdate)
    {
        //Debug.Log("playing card: freeze");

    }

    public void PlayCardSnip(int nodeToUpdate)
    {
        Debug.Log("playing card: snip");
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
            Debug.Log("Won from snipping all nodes");
            SceneManager.LoadScene("GameWinScene"); // Load the Game Win scene
        }

        List<GameObject> enemiesOnNode = gameManager.enemies[nodeToUpdate];
        //Debug.Log("Enemies to remove: " + enemiesOnNode.Count.ToString());
        if (enemiesOnNode.Count > 0)
        {
            for (int i = 0; i < enemiesOnNode.Count; i++)
            {
                DestroyImmediate(enemiesOnNode[i]); // Destroy enemies 
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
