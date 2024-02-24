using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public string cardName;
    public int strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card()
    {

    }

    public Card(int Id, string CardName, int Strength)
    {
        id = Id;
        cardName = CardName;
        strength = Strength;
    }

}
