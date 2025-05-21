using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public SO_EnemyType enemy;
    
    public float lifeTime = 5f;
    public PlayerHealth _playerHealth;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Bullet hit player. Damage: {enemy.damage}");
            _playerHealth.TakeDamage(enemy.damage);
            Destroy(gameObject);
        }
    }
}
