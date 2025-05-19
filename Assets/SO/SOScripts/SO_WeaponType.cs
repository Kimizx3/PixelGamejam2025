using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon_", menuName = "Weapon_Data")]
public class SO_WeaponType : ScriptableObject
{
    public int baseDamageAmount = 1;
    public float fireRate = 0.8f;
    public float bulletSize = 10f;
    public float reloadTime = 1f;
    public int projectileCount = 1;
    public int ammoCount = 1;

    public SO_WeaponType Clone()
    {
        return Instantiate(this);
    }
}

