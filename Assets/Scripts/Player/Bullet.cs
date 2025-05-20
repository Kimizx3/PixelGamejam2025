using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Reference Type")]
    public Action<GameObject> OnHitEnemy;

    // Bullet Properties
    private int _damage;
    private bool _isPiercing;
    private float _bulletSpeed;
    private float _bulletLifeTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int damage, bool isPiercing, float bulletSpeed, float bulletLifeTime)
    {
        _damage = damage;
        _isPiercing = isPiercing;
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;
        
        Destroy(gameObject, _bulletLifeTime);
    }

    public void LaunchBullet(Vector2 direction, Bullet bullet)
    {
        if (rb != null)
        {
            //Debug.Log($"[LaunchBullet] Applying velocity: {direction * _bulletSpeed}");
            rb.velocity = direction * _bulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("FinalBoss"))
        {
            OnHitEnemy?.Invoke(collision.gameObject);
            AIComponent aiComponent = collision.GetComponent<AIComponent>();
            Zombie zombie = collision.GetComponent<Zombie>();
            ZombieFlag flag = collision.GetComponent<ZombieFlag>();
            FinalBoss finalBoss = collision.GetComponent<FinalBoss>();
            if (zombie != null)
            {
                zombie.TakeDamage(_damage);
            }
            if (finalBoss != null)
            {
                finalBoss.TakeDamage(_damage);
            }
            if (flag != null)
            {
                flag.TakeDamage(_damage);
            }
            if (aiComponent != null)
            {
                aiComponent.TakeDamage(_damage);
                //Debug.Log($"Bullet hit {collision.name} and dealt {_damage} damage.");
            }
            if (!_isPiercing)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Collision"))
        {
            Destroy(gameObject);
        }
    }
}
