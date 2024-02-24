using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{

    public Card good, bad;

    private GameManager gameManager;
    private bool isDragging;
    private Vector3 originalCardHolderPosition;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Spire"))
        {
            Debug.Log("Card holder collided with spire");
            gameManager.PlayCards(good, bad);
            gameManager.Drawcard();
        }
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
