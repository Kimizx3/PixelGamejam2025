using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AIState
{
    Wander,
    Idle,
    Return,
    Chase,
    Lost,
    Die
}
public class Slime : MonoBehaviour
{
    [Header("AI Setting")] 
    public AIState currentState = AIState.Wander;
    public SO_GoldType goldData;
    public SO_EnemyType enemyType;

    [Header("Detection Settings")] 
    public Transform player;

    [SerializeField] private float sightRadius = 5f;
    [SerializeField] private float loseRadius = 7f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float wanderRadius = 3f;
    [SerializeField] private float wanderTime = 2f;
    [SerializeField] private float idleTime = 1f;
    [SerializeField] private int maxHealth = 4;

    private Rigidbody2D rb;
    private Vector2 initPosition;
    private Vector2 wanderTarget;
    private bool isWandering = false;
    private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        switch (currentState)
        {
            case AIState.Wander:
                if (!isWandering)
                {
                    StartCoroutine(Wander());
                }
                DetectPlayer();
                break;
            case AIState.Idle:
                rb.velocity = Vector2.zero;
                break;
            case AIState.Return:
                ReturnToInitialPosition();
                break;
            case AIState.Chase:
                ChasePlayer();
                LosePlayer();
                break;
            case AIState.Lost:
                LostBehavior();
                break;
            case AIState.Die:
                Die();
                break;
        }
    }

    private IEnumerator Wander()
    {
        isWandering = true;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        wanderTarget = (Vector2)transform.position + randomDirection * wanderRadius;

        float elapsedTime = 0f;

        while (elapsedTime < wanderTime && currentState == AIState.Wander)
        {
            MoveTowards(wanderTarget);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isWandering = false;
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            MoveTowards(player.position);
        }
    }

    private void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * enemyType.moveSpeed;
    }

    private void LostBehavior()
    {
        StartCoroutine(Idle());
        ReturnToInitialPosition();
        currentState = AIState.Lost;
    }

    private void ReturnToInitialPosition()
    {
        if (Vector2.Distance(transform.position, initPosition) > 0.1f)
        {
            MoveTowards(initPosition);
        }
        else
        {
            rb.velocity = Vector2.zero;
            currentState = AIState.Wander;
        }
    }

    private void DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= sightRadius)
        {
            currentState = AIState.Chase;
        }
    }

    private void LosePlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > loseRadius)
        {
            currentState = AIState.Lost;
        }
    }

    private IEnumerator Idle()
    {
        currentState = AIState.Idle;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(idleTime);

        currentState = AIState.Return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initPosition, 0.3f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wanderTarget, 0.3f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ResetAI()
    {
        
    }

    private void Die()
    {
        int goldAmount = Random.Range(enemyType.minGold, enemyType.maxGold + 1);
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Instantiate(goldData.basicGoldPrefab,
                transform.position + (Vector3)randomOffset, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
