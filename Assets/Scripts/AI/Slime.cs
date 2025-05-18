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

    [SerializeField] private float sightRadius = 100f;

    private Rigidbody2D rb;
    private Vector2 wanderTarget;
    private Animator _animator;

    private Transform player;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        aiComponent = GetComponent<AIComponent>();
    }

    private void Update()
    {
        HandleStates();
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
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
            MoveTowards(player.transform.position);
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
}
