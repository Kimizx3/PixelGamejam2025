using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public SO_EnemyType _enemyType;
    public SO_GoldType goldData;
    public GameObject target;

    public int heath = 4;

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    public void TakeDamage(int damage)
    {
        heath -= damage;

        if (heath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int goldAmount = Random.Range(_enemyType.minGold, _enemyType.maxGold + 1);
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Instantiate(goldData.basicGoldPrefab, 
                transform.position + (Vector3)randomOffset, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (target.CompareTag("Player"))
        {
            
        }
    }

    private void Detection()
    {
        Collider2D collision =
            Physics2D.OverlapCircle((Vector2)transform.position, 
                _enemyType.detectRange, _enemyType.targetLayer);
        if (collision != null)
        {
            target = collision.gameObject;
        }
        else
        {
            target = null;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_enemyType.detectionDelay);
        Detection();
        StartCoroutine(DetectionCoroutine());
    }
}
