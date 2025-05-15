using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy_Data")]
public class SO_EnemyType : ScriptableObject
{
    [Header("Basic Settings")]
    public float moveSpeed = 3;
    
    [Header("Drop Settings")]
    public float dropChance = 0.5f;
    public int minGold = 1;
    public int maxGold = 5;
}
