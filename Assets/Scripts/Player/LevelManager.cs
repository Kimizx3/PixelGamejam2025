using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager PlayerLevelManager { get; private set; }
    
    private int experienceCount;

    private void Awake()
    {
        if (PlayerLevelManager == null)
        {
            PlayerLevelManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddExperienceToUI(int amount)
    {
        experienceCount += amount;
    }

    public int GetExperience()
    {
        return experienceCount;
    }
}
