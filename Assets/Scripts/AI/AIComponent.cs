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
    private Slime slime;
    private int stunDuration;
    private int currentHealth;
    private bool isStunning = false;
    private bool isDead = false;

    private void Awake()
    {
        slime = GetComponent<Slime>();
        _animator = GetComponent<Animator>();
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
        int goldAmount = Random.Range(enemyType.minGold, enemyType.maxGold + 1);
        StartCoroutine(PlayDeathAnim());
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Instantiate(slime.shardType.shardPrefab,
                transform.position + (Vector3)randomOffset, Quaternion.identity);
        }
    }

    IEnumerator PlayDeathAnim()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
    
    public void Stun(float duration)
    {
        if (!isStunning)
        {
            isStunning = true;
            // Play stun coroutine
            StartCoroutine(StunEffect(stunDuration));
        }
    }

    IEnumerator StunEffect(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isStunning = false;
    }
}
