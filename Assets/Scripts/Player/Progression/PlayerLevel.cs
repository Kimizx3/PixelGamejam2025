using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 1;
    private int currentExperience;
    public int experienceToNextLevel = 100;
    public Action<int> OnLevelUp;

    [FormerlySerializedAs("weaponUpgradeUI")] [SerializeField] private WeaponUpgradeUI weaponWeaponUpgradeUI;

    private void Awake()
    {
        OnLevelUp += HandleLevelUp;
    }


    public void AddExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            currentLevel++;
            experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.2f);
            
            OnLevelUp?.Invoke(currentLevel);

            if (currentLevel % 5 == 0)
            {
                // TriggerSkillSelection();
                Debug.Log("Skill selection time!");
            }
            else
            {
                // TriggerUpgradeSelection();
                weaponWeaponUpgradeUI.ShowUpgradeOptions();
                Debug.Log("Upgrade selection time!");
            }
        }
    }

    private void HandleLevelUp(int level)
    {
        Debug.Log($"Player leveled up! Current Level: {level}");
    }

    public int GetCurrentExperience()
    {
        return currentExperience;
    }
}
