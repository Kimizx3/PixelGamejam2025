using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player_", menuName = "Player_Data")]
public class SO_PlayerType : ScriptableObject
{
    [Header("Basic Setting")] 
    public int maxHealth = 4;
    public float moveSpeed = 2f;

    [Header("Stats Setting")] 
    public float invincibilityDuration = 0.5f;
    
    
}
