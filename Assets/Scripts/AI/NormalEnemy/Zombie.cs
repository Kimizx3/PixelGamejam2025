using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private AIState currentState = AIState.Chase;

    public SO_EnemyType enemyType;
    private AIComponent _aiComponent;
    private float sightRadius = 100f;
    private Rigidbody2D rb;
    private Transform player;
    private bool isDead = false;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;
    private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _aiComponent = GetComponent<AIComponent>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        currentHealth = enemyType.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        HandleState();
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

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }

    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
        _aiComponent.ResetState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case AIState.Chase:
                DetectPlayer();
                ChasePlayer();
                break;
            case AIState.Die:
                Die();
                break;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 dir = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            rb.velocity = dir * enemyType.moveSpeed;
        }
    }

    private void DetectPlayer()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= sightRadius)
        {
            ChangeState(AIState.Chase);
        }
    }

    private void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die");
        StartCoroutine(PlayDeathAnim());
    }

    private IEnumerator PlayDeathAnim()
    {
        rb.velocity = Vector2.zero;
        _capsuleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
}
