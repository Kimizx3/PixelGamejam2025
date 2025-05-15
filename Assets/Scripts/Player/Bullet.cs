using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SO_BulletType _bulletType;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(_bulletType.pistolBulletDamage);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Collision"))
        {
            Destroy(gameObject);
        }
        
    }
}
