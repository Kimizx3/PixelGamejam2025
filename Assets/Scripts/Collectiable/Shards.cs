using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shards : MonoBehaviour
{
    public SO_ShardType shard;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.PlayerLevelManager.AddExperienceToUI(shard.shardAmount);
            FindObjectOfType<PlayerLevel>().AddExperience(shard.shardAmount);
            Destroy(gameObject);
        }
    }
}
