using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private Transform playerTransform;
    private Vector2 startingPosition;

    public int enemySpawnerParent;
    public int damage = 1;

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

    public void PushBack() // for push back enemy card
    {
        transform.position = startingPosition;
    }

    public void IncreaseSpeed(int factorToIncrease) // for increase enemy speed card
    {
        speed *= factorToIncrease;
    }

    public void DoubleDamge() // for double damage enemy card
    {
        damage *= 2;
    }
}