using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardListGood = new List<Card>();
    public static List<Card> cardListBad = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() // load card types
    {
        cardListGood.Add(new Card(0, "Push Back", 1));
        cardListGood.Add(new Card(1, "Electrify", 2));
        cardListGood.Add(new Card(2, "Electrify All", 3));
        cardListGood.Add(new Card(3, "Freeze", 1));
        cardListGood.Add(new Card(4, "Snip", 2));

        cardListBad.Add(new Card(0, "Increase Speed", 2));
        cardListBad.Add(new Card(0, "Double Damage", 3));
        cardListBad.Add(new Card(0, "Add 5 secs to Timer", 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
