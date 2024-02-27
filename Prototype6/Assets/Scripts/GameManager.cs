using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> deckGood = new();
    public List<Card> deckBad = new();
    public Transform[] cardSlots;
    [SerializeField] CardHolder cardHolderLeft;
    [SerializeField] CardHolder cardHolderRight;
    [SerializeField] GameObject GoodDeck;
    [SerializeField] GameObject BadDeck;

    [SerializeField] public GameObject[] nodes;
    public List<GameObject>[] enemies = new List<GameObject>[8];
    public int numberOfActiveEnemies = 0;

    private GameTimer timer;

    private void Start()
    {
        timer = FindObjectOfType<GameTimer>();

        for (int i = 0; i < enemies.Length; i++) // instantiate Lists with array
        {
            enemies[i] = new List<GameObject>();
        }

        Drawcard();
    }

    public void Drawcard()
    {
        int p1, p2;
        Card randGood1, randGood2, randBad1, randBad2;
        // if timer is < x amt of time, then introduce higher power cards
        if (timer.gameDuration >= 70f || numberOfActiveEnemies < 4)
        {
            p1 = Random.Range(0, deckGood.Count); // pull random card
            while (deckGood[p1].cardPowerLevel > 2) // while it pulls a higher power level
            {
                p1 = Random.Range(0, deckGood.Count);

            }
            randGood1 = deckGood[p1];

            p2 = Random.Range(0, deckGood.Count); // pull random card
            while (deckGood[p2].cardPowerLevel > 2 || p2 == p1) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);

            }
            randGood2 = deckGood[p2];
        }
        else if (numberOfActiveEnemies > 3)
        {
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];

            p2 = Random.Range(0, deckGood.Count); // pull random card
            while (p2 == p1) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);

            }
            randGood2 = deckGood[p2];
        }
        else // less than 70 secs left
        {
            p1 = Random.Range(0, deckGood.Count); // pull random card
            randGood1 = deckGood[p1];

            p2 = Random.Range(0, deckGood.Count); // pull random card
            while ( p2 == p1) // while it pulls a higher power level
            {
                p2 = Random.Range(0, deckGood.Count);

            }
            randGood2 = deckGood[p2];
        }
        p1 = Random.Range(0, deckBad.Count); // pull random card
        randBad1 = deckBad[p1];

        p2 = Random.Range(0, deckBad.Count); // pull random card
        while (p2 == p1) // while it pulls a higher power level
        {
            p2 = Random.Range(0, deckBad.Count);

        }
        randBad2 = deckBad[p2];

        // Place cards in right position
        randGood1.transform.position = cardSlots[0].position;
        randBad1.transform.position = cardSlots[1].position;

        randGood2.transform.position = cardSlots[2].position;
        randBad2.transform.position = cardSlots[3].position;

        // Put cards into card holders
        cardHolderLeft.good = randGood1; cardHolderLeft.bad = randBad1;
        cardHolderRight.good = randGood2; cardHolderRight.bad = randBad2;
    }

    public void PlayCards(Card good, Card bad, int nodeToUpdate)
    {
        good.PlayCard(nodeToUpdate);
        bad.PlayCard(nodeToUpdate);

       // Debug.Log("Clearing old hand...");

        // Clear drawn cards: set cards as children to decks
        cardHolderLeft.good.transform.position = cardHolderLeft.good.ogPosition;
        cardHolderLeft.good.transform.position = cardHolderLeft.good.ogPosition;
        cardHolderLeft.bad.transform.position = cardHolderLeft.bad.ogPosition;
        cardHolderRight.good.transform.position = cardHolderRight.good.ogPosition;
        cardHolderRight.bad.transform.position = cardHolderRight.bad.ogPosition;
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies[enemy.GetComponent<EnemyMovement>().enemySpawnerParent].Add(enemy);
    }
}