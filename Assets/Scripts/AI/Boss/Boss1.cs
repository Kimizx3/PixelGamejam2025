using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss1 : MonoBehaviour, BaseEnemy
{
    [SerializeField] private AIState currentState = AIState.Chase;

    [Header("Reference Type")] 
    public SO_EnemyType enemyType;
    private AIComponent _aiComponent;
    public GameObject aimPoint;

    [Header("Detection Settings")] 
    //[SerializeField] private float sightRadius = 100f;

    [Header("Attack Setting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float predictionTime = 0.5f;

    private Rigidbody2D rb;
    private Transform player;
    private float lastAttackTime;

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
        enemyType.playerRB = playerTransform.GetComponent<Rigidbody2D>();
        
        ChangeState(AIState.Chase);
    }

    private void HandleStates()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        switch (currentState)
        {
            case AIState.Chase:
                if (distance <= enemyType.attackRange)
                {
                    ChangeState(AIState.Attack);
                }
                else
                {
                    ChasePlayer();
                }
                break;
            
            case AIState.Attack:
                if (distance > enemyType.attackRange)
                {
                    ChangeState(AIState.Chase);
                }
                else
                {
                    PerformAttack();
                }
                break;
            
            case AIState.Die:
                _aiComponent.Die();
                break;
        }
    }

    private void ChasePlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 targetPosition =
            Vector2.MoveTowards(transform.position, player.position, enemyType.moveSpeed * Time.deltaTime);
        rb.MovePosition(targetPosition);
    }

    public void PerformAttack()
    {
        if (Time.time >= lastAttackTime + enemyType.attackInterval)
        {
            lastAttackTime = Time.time;
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (player == null)
        {
            return;
        }
        
        Vector3 firePoint = aimPoint.transform.position;
        
        Vector3 predictionPosition =
            player.transform.position + (Vector3)(enemyType.playerRB.velocity * predictionTime);
        Vector2 direction = (predictionPosition - firePoint).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}
