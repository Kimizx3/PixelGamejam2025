using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Slime : MonoBehaviour, BaseEnemy
{
    [Header("AI Setting")]
    [SerializeField] private AIState currentState = AIState.Chase;
    

    [Header("Reference Type")] 
    public SO_EnemyType enemyType;
    private AIComponent aiComponent;
    
    [Header("Detection Settings")] 
    [SerializeField] private float sightRadius = 100f;

    private Rigidbody2D rb;

    private Transform player;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        aiComponent = GetComponent<AIComponent>();
    }

    private void Update()
    {
        HandleStates();
    }

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }

    public void ChangeState(AIState newState)
    {
        currentState = newState;
    }

    public AIState GetCurrentState()
    {
        return currentState;
    }

    public void DisableCollision(CapsuleCollider2D collision)
    {
        collision.enabled = false;
    }

    public void EnableCollision(CapsuleCollider2D collision)
    {
        collision.enabled = true;
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
        aiComponent.ResetState();
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

    public void PerformAttack()
    {
        throw new NotImplementedException();
    }
}
