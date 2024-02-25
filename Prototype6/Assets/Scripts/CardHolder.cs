using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHolder : MonoBehaviour
{
    public Card good, bad;

    private GameManager gameManager;
    private bool isDragging;
    private Vector3 originalCardHolderPosition;
    private Collider2D collisionObject;

    private bool triggered = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        originalCardHolderPosition = gameObject.transform.position; // getting o.g. position of card holder to return to after card usage
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }
    public void OnMouseUp()
    {
        isDragging = false;
        gameObject.transform.position = originalCardHolderPosition; // set card holder back to o.g. position
        if (triggered)
        {
            Debug.Log("Card holder collided with: " + collisionObject.tag);
            gameManager.PlayCards(good, bad);
            gameManager.Drawcard();
            triggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        collisionObject = collision;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
        
    }
}
