using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy_Data")]
public class SO_EnemyType : ScriptableObject
{
    [Header("Basic Settings")]
    public float moveSpeed = 3f;
    public int maxHealth = 4;
    public string nameTag;

    [Header("Combat Settings")] 
    public int damage = 1;
    public float detectRange = 10f;
    public float attackRange = 2f;
    public LayerMask targetLayer;
    public float detectionDelay = 0.5f;
    public float attackInterval = 2f;
    public Rigidbody2D playerRB;
    
    [Header("Drop Settings")]
    public float dropChance = 0.5f;
    public int exp = 1;
    public GameObject shard;

    [Header("Animation Setting")]
    public float deathAnimPlayTime = 1f;
}
