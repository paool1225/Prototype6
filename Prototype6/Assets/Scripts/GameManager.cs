using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Card> deckGood = new();
    public List<Card> deckBad = new();
    public Transform[] cardSlots;
    public Transform[] initialSlots;
    [SerializeField] CardHolder cardHolderLeft;
    [SerializeField] CardHolder cardHolderRight;
    [SerializeField] GameObject GoodDeck;
    [SerializeField] GameObject BadDeck;

    [SerializeField] public GameObject[] nodes;
    public List<GameObject>[] enemies = new List<GameObject>[8];
    public int numberOfActiveEnemies = 0;

    private GameTimer timer;
    private PlayerHealth playerHealth;

    public int goodDeckSize = 10;
    public int badDeckSize = 6;

    private void Start()
    {
        timer = FindObjectOfType<GameTimer>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        for (int i = 0; i < enemies.Length; i++) // instantiate Lists with array
        {
            enemies[i] = new List<GameObject>();
        }

        Drawcard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Drawcard()
    {
        if(goodDeckSize <= 5) // reset cards back into unused piles
        {
            for(int i = 0; i < deckGood.Count; i++) 
            {
                deckGood[i].hasBeenUsed = false;
            }
            goodDeckSize = 18;
        }
        if(badDeckSize <= 2)
        {
            for (int i = 0; i < deckBad.Count; i++)
            {
                deckBad[i].hasBeenUsed = false;
            }
            badDeckSize = 10;
        }

        int p1, p2;
        Card randGood1, randGood2, randBad1, randBad2;
        if(playerHealth.health < 4)
        {
            // let high cards come in
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];

            while (randGood1.hasBeenUsed)
            {
                p1 = Random.Range(0, deckGood.Count); // pull random card
                randGood1 = deckGood[p1];
            }

            p2 = Random.Range(0, deckGood.Count); // pull random card
            randGood2 = deckGood[p2];
            while (p2 == p1 || randGood2.hasBeenUsed) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);
                randGood2 = deckGood[p2];
            }
        }
        else if (numberOfActiveEnemies >= 4)
        {
            Debug.Log("HIGH ENEMIES: " + numberOfActiveEnemies);
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];

            while (randGood1.hasBeenUsed)
            {
                p1 = Random.Range(0, deckGood.Count); // pull random card
                randGood1 = deckGood[p1];
            }

            p2 = Random.Range(0, deckGood.Count); // pull random card
            randGood2 = deckGood[p2];
            while (p2 == p1 || randGood2.hasBeenUsed) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);
                randGood2 = deckGood[p2];
            }
        }
        // if timer is < x amt of time, then introduce higher power cards
        else if (timer.gameDuration >= 40f || numberOfActiveEnemies < 3)
        {
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];
            while (deckGood[p1].cardPowerLevel > 2 || randGood1.hasBeenUsed) // while it pulls a higher power level
            {
                p1 = Random.Range(0, deckGood.Count);
                randGood1 = deckGood[p1];
            }

            p2 = Random.Range(0, deckGood.Count); // pull random card
            randGood2 = deckGood[p2];
            while (deckGood[p2].cardPowerLevel > 2 || p2 == p1 || randGood2.hasBeenUsed) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);
                randGood2 = deckGood[p2];
            }
        }
        else // less than 70 secs left
        {
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];

            while (randGood1.hasBeenUsed)
            {
                p1 = Random.Range(0, deckGood.Count); // pull random card
                randGood1 = deckGood[p1];
            }

            p2 = Random.Range(0, deckGood.Count); // pull random card
            randGood2 = deckGood[p2];

            while (p2 == p1 || randGood2.hasBeenUsed) // while they are the exact same card
            {
                p2 = Random.Range(0, deckGood.Count);
                randGood2 = deckGood[p2];

            }
        }


        p1 = Random.Range(0, deckBad.Count); // pull random card
        randBad1 = deckBad[p1];
        while (randBad1.hasBeenUsed)
        {
            p1 = Random.Range(0, deckBad.Count); // pull random card
            randBad1 = deckBad[p1];
        }

        p2 = Random.Range(0, deckBad.Count); // pull random card
        randBad2 = deckBad[p2];

        while (p2 == p1 || randBad2.hasBeenUsed) // while they are the exact same card
        {
            p2 = Random.Range(0, deckBad.Count);
            randBad2 = deckBad[p2];
        }

        // Place cards in right position
        randGood1.transform.position = cardSlots[0].position;
        randBad1.transform.position = cardSlots[1].position;

        randGood2.transform.position = cardSlots[2].position;
        randBad2.transform.position = cardSlots[3].position;

        // Put cards into card holders
        cardHolderLeft.good = randGood1; cardHolderLeft.bad = randBad1;
        cardHolderRight.good = randGood2; cardHolderRight.bad = randBad2;

        Debug.Log("rg: " + randGood1.name + " rb: " + randBad1.name);
        Debug.Log("lg: " + randGood2.name + " lb: " + randBad2.name);
    }

    public void PlayCards(Card good, Card bad, int nodeToUpdate)
    {
        good.PlayCard(nodeToUpdate);
        bad.PlayCard(nodeToUpdate);

        // Debug.Log("Clearing old hand...");

        // Clear drawn cards: set cards as children to decks
        cardHolderLeft.good.transform.position = initialSlots[0].position;
        cardHolderLeft.bad.transform.position = initialSlots[1].position;
        cardHolderRight.good.transform.position = initialSlots[0].position;
        cardHolderRight.bad.transform.position = initialSlots[1].position;
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies[enemy.GetComponent<EnemyMovement>().enemySpawnerParent].Add(enemy);
    }
}