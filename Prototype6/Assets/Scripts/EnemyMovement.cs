using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.5f;
    private Transform playerTransform;
    private Vector2 startingPosition;

    public int enemySpawnerParent;
    public int damage = 1;

    private bool isFrozen = false;
    private float freezeDuration = 5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Ensure player has the "Player" tag
        startingPosition = transform.position;
    }

    void Update()
    {
        if (!isFrozen)
        {
            MoveTowardsPlayer();
        }
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
    public void Freeze(float duration)
    {
        if (!isFrozen) // Prevent re-freezing if already frozen
        {
            isFrozen = true;
            freezeDuration = duration;
            StartCoroutine(FreezeCoroutine());
        }
    }
    private IEnumerator FreezeCoroutine()
    {
        // Logic to pause enemy actions
        // For example, disable enemy's movement
        yield return new WaitForSeconds(freezeDuration);

        // Logic to resume enemy actions
        isFrozen = false;
        // For example, enable enemy's movement
    }

    private void Unfreeze()
    {
        isFrozen = false;
    }
}