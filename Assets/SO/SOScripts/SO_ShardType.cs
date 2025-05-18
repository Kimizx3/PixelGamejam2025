using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shard_", menuName = "Shard_Data")]
public class SO_ShardType : ScriptableObject
{
    public int shardAmount = 1;
    
    [Header("Gold Prefab Settings")]
    public GameObject shardPrefab;
}

