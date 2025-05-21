using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Reference Setting")] 
    public SO_PlayerType player;
    [SerializeField] private HealthUI healthUI;

    [Header("Player Stats")] 
    public SpriteRenderer playerSprite;
    
    // Private Section
    public int currentHealth;
    private bool isInvincible = false;
    [ReadOnly] public bool isDead;
    

    private void Awake()
    {
        isDead = false;
        currentHealth = player.maxHealth;
        if (healthUI != null)
        {
            healthUI.InitializeHearts(currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible)
        {
            return;
        }
        
        //Debug.Log($"Player health : {currentHealth}");
        if (!isDead)
        {
            currentHealth -= amount;
        }
        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth);
        }
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            StartCoroutine(ActivateInvincibility());
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;

        float flashTime = 0.1f;

        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(flashTime);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(flashTime);
        }

        yield return new WaitForSeconds(player.invincibilityDuration - (flashTime * 5));

        isInvincible = false;
    }
    
    private void Die()
    {
        isDead = true;
        
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
        EnemySpawner.Instance.StopSpawning();
    }

    private IEnumerator PlayDeathAnim()
    {
        yield return new WaitForSeconds(1);
    }
}
