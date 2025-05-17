using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Reference Type")]
    public SO_BulletType _bulletType;

    public Action<GameObject> OnHitEnemy;

    private void Start()
    {
        Destroy(gameObject, _bulletType.bulletLifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnHitEnemy?.Invoke(collision.gameObject);
            AIComponent aiComponent = collision.GetComponent<AIComponent>();
            if (aiComponent != null)
            {
                aiComponent.TakeDamage(_bulletType.damage);
                Debug.Log($"Bullet hit {collision.name} and dealt {_bulletType.damage} damage.");
            }
            if (_bulletType.isPiercing)
            {
                return;
            }
            else
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
