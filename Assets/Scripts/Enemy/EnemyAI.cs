using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public SO_EnemyType _enemyType;
    public SO_GoldType goldData;

    public int heath = 4;

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
}
