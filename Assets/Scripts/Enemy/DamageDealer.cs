using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    // TODO: Tag dependency
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log($"Dealt damage to player: {damageAmount}");
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}
