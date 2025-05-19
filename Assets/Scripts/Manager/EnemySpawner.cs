using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")] 
    public SO_WaveConfig waveConfig;
    public float spawnDistanceFromCamera = 2f;

    public static EnemySpawner Instance { get; private set; }
    private float elapsedTime = 0f;
    private bool isSpawning = true;
    
    [Header("Spawn Boundaries")]
    private Camera mainCamera;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            EnemyPool.Instance.SetPlayerTransform(playerObject.transform);
        }

        if (waveConfig == null)
        {
            isSpawning = false;
            return;
        }

        StartCoroutine(SpawnManager());
    }

    private IEnumerator SpawnManager()
    {
        while (isSpawning)
        {
            elapsedTime += Time.deltaTime;

            foreach (var wave in waveConfig.Waves)
            {
                if (elapsedTime >= wave.startTime && elapsedTime < wave.startTime + wave.duration)
                {
                    foreach (var enemySpawn in wave.enemySpawns)
                    {
                        StartCoroutine(SpawnEnemy(enemySpawn));
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator SpawnEnemy(SO_WaveConfig.EnemySpawn enemySpawn)
    {
        for (int i = 0; i < enemySpawn.amount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            EnemyPool.Instance.SpawnFromPool(enemySpawn.enemyTag, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawn.spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 cameraPos = mainCamera.transform.position;
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float leftBound = cameraPos.x - width / 2 - spawnDistanceFromCamera;
        float rightBound = cameraPos.x + width / 2 + spawnDistanceFromCamera;
        float topBound = cameraPos.y + height / 2 + spawnDistanceFromCamera;
        float bottomBound = cameraPos.y - height / 2 - spawnDistanceFromCamera;

        Vector3 spawnPosition = Vector3.zero;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector3(leftBound, Random.Range(bottomBound, topBound), 0), // Left
            1 => new Vector3(rightBound, Random.Range(bottomBound, topBound), 0), // Right
            2 => new Vector3( Random.Range(leftBound, rightBound), topBound, 0), // Top
            _ => new Vector3(Random.Range(leftBound, rightBound), bottomBound, 0) // Bottom
        };
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }
}
