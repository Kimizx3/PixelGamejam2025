using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgrade : MonoBehaviour
{
    public float timeTrigger = 2f;
    public int bulletTrigger = 2;

    private float timeCounter = 0f;
    private int bulletCounter = 0;


    private void Update()
    {
        // Time-based trigger
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeTrigger)
        {
            timeCounter = 0f;
            ActivateTimeSkill();
        }
        
        // Bullet-based trigger (need to call this method when shooting)
    }

    public void CountBullet()
    {
        bulletCounter++;
        if (bulletCounter >= bulletTrigger)
        {
            bulletCounter = 0;
            ActivateBulletSkill();
        }
    }

    private void ActivateTimeSkill()
    {
        Debug.Log("Time-based skill activated!");
        // Put logic here...
    }

    private void ActivateBulletSkill()
    {
        Debug.Log("Bullet-based skill activated!");
        // Put logic here...
    }
}
