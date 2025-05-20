using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI experienceUI;
    [SerializeField] private Slider experienceBarFill;
    [SerializeField] private PlayerLevel playerLevel;

    [Header("Time Debug")] [SerializeField]
    private TextMeshProUGUI timeUI;

    private void Start()
    {
        if (playerLevel != null)
        {
            playerLevel.OnLevelUp += UpdateUI;
        }

        experienceBarFill.maxValue = playerLevel.experienceToNextLevel;
        experienceBarFill.value = playerLevel.GetCurrentExperience();
    }

    private void Update()
    {
        UpDateTime();
        if (playerLevel != null)
        {
            experienceBarFill.value = playerLevel.GetCurrentExperience();
        }
    }

    private void OnPlayerLevelUp(int level)
    {
        experienceBarFill.maxValue = playerLevel.experienceToNextLevel;
        experienceBarFill.value = 0;
    }

    private void UpdateUI(int level)
    {
        experienceUI.text = $"Hero Level: {level}";
    }

    private void UpDateTime()
    {
        float time = Time.time;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeUI.text = $"Time: {minutes:D2}:{seconds:D2}";
    }
}
