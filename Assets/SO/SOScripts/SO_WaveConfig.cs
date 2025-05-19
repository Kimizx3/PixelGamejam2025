using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Game/WaveConfig")]
public class SO_WaveConfig : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawn
    {
        public string enemyTag;
        public int amount;
        public float spawnInterval;
    }
    
    
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public float startTime; // When the wave starts (in seconds)
        public float duration; // How long this wave lasts
        public List<EnemySpawn> enemySpawns;
    }

    

    public List<Wave> Waves;
}
