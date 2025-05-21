using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private GameObject battlefieldPrefab;
    
    [SerializeField] private float activationTime = 302f;
    [SerializeField] private float size = 30f;

    private List<GameObject> barriers = new List<GameObject>();
    //private bool isBattlefieldActive = false;
    private Transform bossTransform;

    private void Start()
    {
        Invoke(nameof(ActivateBattlefield), activationTime);
    }

    private void ActivateBattlefield()
    {
        bossTransform = GameObject.FindGameObjectWithTag("FinalBoss")?.transform;

        if (bossTransform == null)
        {
            return;
        }

        Vector3 centerPosition = bossTransform.position;
        
        CreateBarrier(new Vector3(centerPosition.x, centerPosition.y + size / 2, 0), new Vector2(size, 0.5f));
        CreateBarrier(new Vector3(centerPosition.x, centerPosition.y - size / 2, 0), new Vector2(size, 0.5f));
        CreateBarrier(new Vector3(centerPosition.x - size / 2, centerPosition.y, 0), new Vector2(0.5f, size));
        CreateBarrier(new Vector3(centerPosition.x + size / 2,  centerPosition.y, 0), new Vector2(0.5f, size));
        
    }

    private void CreateBarrier(Vector3 position, Vector2 scale)
    {
        GameObject barrier = Instantiate(battlefieldPrefab, position, Quaternion.identity);
        barrier.transform.localScale = new Vector3(scale.x, scale.y, 1f);
        barriers.Add(barrier);
    }
}
