using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour, BaseEnemy
{
    [SerializeField] private AIState currentState = AIState.Chase;
    
    [Header("Reference Type")] 
    public SO_EnemyType enemyType;
    private AIComponent _aiComponent;

    [Header("Detection Settings")] 
    [SerializeField] private float sightRadius = 100f;

    [Header("Attack Setting")]
    [SerializeField] private GameObject bullets;

    private Rigidbody2D rb;
    private Vector2 wanderTarget;
    private Transform player;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _aiComponent = GetComponent<AIComponent>();
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
        _aiComponent.ResetState();
    }

    private void HandleStates()
    {
        switch (currentState)
        {
            case AIState.Chase:
                DetectPlayer();
                ChasePlayer();
                break;
            case AIState.Attack:
                PerformAttack();
                break;
            case AIState.Die:
                _aiComponent.Die();
                break;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            MoveToWards(player.transform.position);
        }
    }

    private void MoveToWards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.velocity = direction * enemyType.moveSpeed;
    }

    private void DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= sightRadius)
        {
            if (distance <= enemyType.attackRange)
            {
                ChangeState(AIState.Attack);
            }
            ChangeState(AIState.Chase);
        }
    }
    
    public void PerformAttack()
    {
        
    }
}
