using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy_Data")]
public class SO_EnemyType : ScriptableObject
{
    [Header("Basic Settings")]
    public float moveSpeed = 3f;
    
    [Header("Combat Settings")]
    public float detectRange = 10f;
    public float attackRange = 2f;
    public LayerMask targetLayer;
    public float detectionDelay = 0.5f;
    
    [Header("Drop Settings")]
    public float dropChance = 0.5f;
    public int minGold = 1;
    public int maxGold = 5;
}
