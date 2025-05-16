using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform PlayerTransform { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                PlayerTransform = playerObject.transform;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
