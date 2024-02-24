using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> goodDeck = new List<Card>();
    public List<Card> badDeck = new List<Card>();

    int x,y;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        for(int i = 0; i < 40; i++)
        {
            x = Random.Range(0, 4);
            y = Random.Range(0, 2);
            goodDeck.Add(CardDatabase.cardListGood[x]);
            badDeck.Add(CardDatabase.cardListBad[y]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
