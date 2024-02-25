using JetBrains.Annotations;
using System.Collections;
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

    private void Start()
    {
        Drawcard();
    }

    public void Drawcard()
    {
        int p1 = Random.Range(0, deckGood.Count);
        // Pull random cards
        Card randGood1 = deckGood[p1];

        int p2 = Random.Range(0, deckGood.Count);
        while (p2 == p1)
        {
            p2 = Random.Range(0, deckGood.Count);
        }
        Card randGood2 = deckGood[p2];

        p1 = Random.Range(0, deckBad.Count);
        Card randBad1 = deckBad[p1];

        p2 = Random.Range(0, deckBad.Count);
        while (p2 == p1)
        {
            p2 = Random.Range(0, deckBad.Count);
        }
        Card randBad2 = deckBad[p2];

        // Place cards in right position
        randGood1.transform.position = cardSlots[0].position;
        randBad1.transform.position = cardSlots[1].position;

        randGood2.transform.position = cardSlots[2].position;
        randBad2.transform.position = cardSlots[3].position;

        // Put cards into card holders
        cardHolderLeft.good = randGood1; cardHolderLeft.bad = randBad1;
        cardHolderRight.good = randGood2; cardHolderRight.bad = randBad2;
    }

    public void PlayCards(Card good, Card bad)
    {
        Debug.Log("Playing good card: " + good.cardId.ToString());
        Debug.Log("Playing bad card: " + bad.cardId.ToString());

        good.PlayCard();
        bad.PlayCard();

        Debug.Log("Clearing old hand...");

        // Clear drawn cards: set cards as children to decks
        cardHolderLeft.good.transform.position = cardHolderLeft.good.ogPosition;
        cardHolderLeft.bad.transform.position = cardHolderLeft.bad.ogPosition;
        cardHolderRight.good.transform.position = cardHolderRight.good.ogPosition;
        cardHolderRight.bad.transform.position = cardHolderRight.bad.ogPosition;
    }
}
