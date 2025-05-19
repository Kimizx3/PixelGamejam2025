using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillUI : MonoBehaviour
{
    public void TriggerSkillSelection()
    {
        //List<SkillOption> availableSkills = SkillDatabase.GetRandomSkills(3);
    }
}

[System.Serializable]
public class SkillOption
{
    public string skillName;
    public Action ActiveSkill;
}
