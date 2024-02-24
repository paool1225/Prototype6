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

        Debug.Log("Good cards pulled: " + randGood1.cardId.ToString() + " " + randGood2.cardId.ToString());

        p1 = Random.Range(0, deckBad.Count);
        Card randBad1 = deckBad[p1];

        p2 = Random.Range(0, deckBad.Count);
        while (p2 == p1)
        {
            p2 = Random.Range(0, deckBad.Count);
        }
        Card randBad2 = deckBad[p2];

        Debug.Log("Bad cards pulled: " + randBad1.cardId.ToString() + " " + randBad2.cardId.ToString());

        //// Set cards as children to card holders
        //randGood1.transform.parent = cardHolderLeft.gameObject.transform;
        //randBad1.transform.parent = cardHolderLeft.gameObject.transform;

        //randGood2.transform.parent = cardHolderRight.gameObject.transform;
        //randBad2.transform.parent = cardHolderRight.gameObject.transform;

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
        //cardHolderLeft.good.transform.parent = GoodDeck.transform;
        cardHolderLeft.good.transform.position = cardHolderLeft.good.ogPosition;
        
        //cardHolderLeft.bad.transform.parent = BadDeck.transform;
        cardHolderLeft.bad.transform.position = cardHolderLeft.bad.ogPosition;

       // cardHolderRight.good.transform.parent = GoodDeck.transform;
        cardHolderRight.good.transform.position = cardHolderRight.good.ogPosition;

        //cardHolderRight.bad.transform.parent = BadDeck.transform;
        cardHolderRight.bad.transform.position = cardHolderLeft.bad.ogPosition;
    }
}
