using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class UpgradeMenu
{
    public string upgradeName;
    public Action ApplyUpgrade;

    public UpgradeMenu(string name, Action upgradeAction)
    {
        upgradeName = name;
        ApplyUpgrade = upgradeAction;
    }
}
