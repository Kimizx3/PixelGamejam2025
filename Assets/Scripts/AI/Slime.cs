using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum AIState
{
    Chase,
    Die
}
public class Slime : MonoBehaviour
{
    [Header("AI Setting")] 
    public AIState currentState = AIState.Chase;
    private AIComponent aiComponent;

    [Header("Reference Type")] 
    public SO_ShardType shardType;
    public SO_EnemyType enemyType;

    [Header("Detection Settings")] 
    public Transform player;

    [SerializeField] private float sightRadius = 100f;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 initPosition;
    private Vector2 wanderTarget;
    private bool isWandering = false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        aiComponent = GetComponent<AIComponent>();
        initPosition = transform.position;
        player = GameManager.Instance.PlayerTransform;
    }

    private void Update()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        switch (currentState)
        {
            case AIState.Chase:
                DetectPlayer();
                ChasePlayer();
                break;
            case AIState.Die:
                aiComponent.Die();
                break;
        }
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

    private void DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= sightRadius)
        {
            currentState = AIState.Chase;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initPosition, 0.3f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wanderTarget, 0.3f);
    }
}
