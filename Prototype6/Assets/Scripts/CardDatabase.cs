using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<Card> cardListGood = new();
    public List<Card> cardListBad = new();


    private void Awake() // load card types
    {
        //Card newCard = new(0, "Push Back", 1);
        //cardListGood.Add(newCard);
        //newCard = new(1, "Electrify", 2);
        //cardListGood.Add(newCard);
        //newCard = new(2, "Electrify All", 3);
        //cardListGood.Add(newCard);
        //newCard = new(3, "Freeze", 1);
        //cardListGood.Add(newCard);
        //newCard = new(4, "Snip", 2);
        //cardListGood.Add(newCard);

        //newCard = new(0, "Increase Speed", 2);
        //cardListBad.Add(newCard);
        //newCard = new(1, "Double Damage", 3);
        //cardListBad.Add(newCard);
        //newCard = new(2, "Add 5 secs to Timer", 3);
        //cardListBad.Add(newCard);
    }
}
