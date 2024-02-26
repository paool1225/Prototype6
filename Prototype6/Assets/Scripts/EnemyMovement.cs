using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private Transform playerTransform;
    private Vector2 startingPosition;

    public int enemySpawnerParent;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Ensure player has the "Player" tag
        startingPosition = transform.position;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void PushBack() // for push back card
    {
        transform.position = startingPosition;
    }
}