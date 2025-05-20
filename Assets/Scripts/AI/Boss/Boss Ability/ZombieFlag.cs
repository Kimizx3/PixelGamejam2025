using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFlag : MonoBehaviour
{
    public event Action OnFlagDestroyed; 
    
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxZombies = 10;
    [SerializeField] private int health = 50;

    private int currentZombies = 0;

    private void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        while (currentZombies < maxZombies)
        {
            GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            currentZombies++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            OnFlagDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
