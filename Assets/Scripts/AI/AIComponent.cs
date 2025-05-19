using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIComponent : MonoBehaviour
{
    [Header("Reference Types")]
    public SO_EnemyType enemyType;

    
    private Animator _animator;
    private BaseEnemy _enemy;
    private CapsuleCollider2D _capsuleCollider2D;
    private int stunDuration;
    private int currentHealth;
    private bool isStunning = false;
    private bool isDead = false;

    private void Awake()
    {
        _enemy = GetComponent<BaseEnemy>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        currentHealth = enemyType.maxHealth;
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
    
    public void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die");
        Instantiate(enemyType.shard, transform.position, Quaternion.identity);
        _enemy.DisableCollision(_capsuleCollider2D);
        
        StartCoroutine(PlayDeathAnim());
    }

    IEnumerator PlayDeathAnim()
    {
        _enemy.GetRigidBody().velocity = Vector2.zero;
        yield return new WaitForSeconds(enemyType.deathAnimPlayTime);
        string enemyTag = gameObject.tag;
        EnemyPool.Instance.ReturnToPool(gameObject, enemyTag);
        gameObject.SetActive(false);
        isDead = false;
    }
    
    
    // public void Stun(float duration)
    // {
    //     if (!isStunning)
    //     {
    //         isStunning = true;
    //         // Play stun coroutine
    //         StartCoroutine(StunEffect(stunDuration));
    //     }
    // }
    //
    // IEnumerator StunEffect(float duration)
    // {
    //     float elapsedTime = 0f;
    //     while (elapsedTime < duration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //     isStunning = false;
    // }

    public void ResetState()
    {
        _enemy.ChangeState(AIState.Chase);
        currentHealth = enemyType.maxHealth;
        isDead = false;
        _enemy.EnableCollision(_capsuleCollider2D);
        _animator.ResetTrigger("Die");
        //_animator.Play("Slime_Idle");
    }
}
