using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int cardId;
    [SerializeField] GameObject deck;
    public Vector3 ogPosition;

    private void Start()
    {
        transform.parent = deck.transform;
        ogPosition = transform.position;
    }

    public void PlayCard() // play the card
    {
        switch(cardId)
        {
            case 0: // good
                PlayCardPushBack();
                break;
            case 1:
                PlayCardElectrify();
                break;
            case 2:
                PlayCardElectrifyAll();
                break;
            case 3:
                PlayCardFreeze();
                break;
            case 4:
                PlayCardSnip();
                break;
            case 5: // bad
                PlayCardIncreaseSpeed();
                break;
            case 6:
                PlayCardDoubleDamage();
                break;
            case 7:
                PlayCardAdd5Seconds();
                break;
        }
    }

    public void PlayCardPushBack()
    {
        Debug.Log("playing card: push back");
    }
    public void PlayCardElectrify()
    {
        Debug.Log("playing card: electrify");

    }

    public void PlayCardElectrifyAll()
    {
        Debug.Log("playing card: electrify all");
    }

    public void PlayCardFreeze()
    {
        Debug.Log("playing card: freeze");

    }

    public void PlayCardSnip()
    {
        Debug.Log("playing card: snip");

    }

    public void PlayCardIncreaseSpeed()
    {
        Debug.Log("playing card: increase enemy speed");

    }

    public void PlayCardDoubleDamage()
    {
        Debug.Log("playing card: double damage");

    }

    public void PlayCardAdd5Seconds()
    {
        Debug.Log("playing card: add 5 seconds");

    }
}
