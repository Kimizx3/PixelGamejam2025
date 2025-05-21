using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public int maxActive;
    }

    [Header("Object Pools")]
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, int> activeCount;
    // private Dictionary<string, Pool> poolLookup = new();
    
    private BaseEnemy _baseEnemy;

    private Transform playerTranform;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        activeCount = new Dictionary<string, int>();

        InitializePools();
    }

    #region Initialization

    private void InitializePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
            activeCount.Add(pool.tag, 0);
        }
    }

    #endregion

    #region Spawn Logic

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Pool poolConfig = pools.Find(p => p.tag == tag);
            if (activeCount[tag] < poolConfig.maxActive)
            {
                GameObject obj = Instantiate(poolConfig.prefab);
                obj.SetActive(false);
                poolDictionary[tag].Enqueue(obj);
            }
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        objectToSpawn.SetActive(true);

        BaseEnemy baseEnemy = objectToSpawn.GetComponent<BaseEnemy>();

        if (baseEnemy != null && playerTranform != null)
        {
            baseEnemy.Initialize(playerTranform);
        }

        activeCount[tag]++;
        return objectToSpawn;
    }

    #endregion

    #region Return Logic

    public void ReturnToPool(GameObject objectToReturn, string tag)
    {
        if (activeCount.ContainsKey(tag))
        {
            activeCount[tag] = Mathf.Max(0, activeCount[tag] - 1);
            objectToReturn.SetActive(false);
            
            poolDictionary[tag].Enqueue(objectToReturn);
            
            //Debug.Log($"[EnemyPool] return {tag} to pool. Active: {activeCount[tag]}");
        }
    }

    #endregion

    public void SetPlayerTransform(Transform player)
    {
        playerTranform = player;
    }
}
