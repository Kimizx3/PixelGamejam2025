using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")] 
    public List<GameObject> enemyPrefab;
    public float spawnInterval = 1f;
    public float spawnDistanceFromCamera = 2f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
    }

    private void Update()
    {
        spawnInterval = Mathf.Max(0.2f, spawnInterval - Time.deltaTime * 0.01f);
    }

    private void SpawnEnemy()
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

        GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Count)];
        Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
    }
}
