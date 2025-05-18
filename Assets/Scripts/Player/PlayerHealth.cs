using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Reference Setting")] 
    public SO_PlayerType player;
    [SerializeField] private HealthUI healthUI;

    [Header("Player Stats")] 
    public SpriteRenderer playerSprite;
    
    // Private Section
    private int currentHealth;
    private bool isInvincible = false;
    

    private void Awake()
    {
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
        
        Debug.Log($"Player health : {currentHealth}");
        currentHealth -= amount;
        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth);
        }
        if (currentHealth <= 0)
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
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
        var spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }
    }

    private IEnumerator PlayDeathAnim()
    {
        yield return new WaitForSeconds(1);
    }
}
