using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")] 
    public List<string> enemyTags;
    public float spawnInterval = 1f;
    public float spawnDistanceFromCamera = 2f;
    public static EnemySpawner Instance { get; private set; }
    
    private Camera mainCamera;
    private bool isSpawning = true;

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

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            EnemyPool.Instance.SetPlayerTransform(playerObject.transform);
        }
        
        mainCamera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    private void Update()
    {
        if (isSpawning)
        {
            spawnInterval = Mathf.Max(0.2f, spawnInterval - Time.deltaTime * 0.01f);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (!isSpawning)
            {
                yield break;
            }
            
            Vector3 cameraPos = mainCamera.transform.position;
            float height = 2f * mainCamera.orthographicSize;
            float width = height * mainCamera.aspect;

            float leftBound = cameraPos.x - width / 2 - spawnDistanceFromCamera;
            float rightBound = cameraPos.x + width / 2 + spawnDistanceFromCamera;
            float topBound = cameraPos.y + height / 2 + spawnDistanceFromCamera;
            float bottomBound = cameraPos.y - height / 2 - spawnDistanceFromCamera;

            Vector3 spawnPosition = Vector3.zero;
            int edge = Random.Range(0, 4);

            switch (edge)
            {
                case 0: // Left
                    spawnPosition = new Vector3(leftBound, Random.Range(bottomBound, topBound), 0);
                    break;
                case 1: // Right
                    spawnPosition = new Vector3(rightBound, Random.Range(bottomBound, topBound), 0);
                    break;
                case 2: // Top
                    spawnPosition = new Vector3(Random.Range(leftBound, rightBound), topBound, 0);
                    break;
                case 3: // Bottom
                    spawnPosition = new Vector3(Random.Range(leftBound, rightBound), bottomBound,0);
                    break;
            }

            string randomTag = enemyTags[Random.Range(0, enemyTags.Count)];
            EnemyPool.Instance.SpawnFromPool(randomTag, spawnPosition, Quaternion.identity);

            
            //EnemyPool.Instance.SpawnFromPool(randomTag, spawnPosition, Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemy());
    }
}
