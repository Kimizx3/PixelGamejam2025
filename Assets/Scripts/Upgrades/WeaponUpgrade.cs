using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public SO_WeaponType weaponType;
    private int damage;
    private float fireRate;
    private float bulletSize;
    private float reloadTime;
    private int projectiles;

    private void Awake()
    {
        damage = weaponType.baseDamageAmount;
        fireRate = weaponType.fireRate;
        bulletSize = weaponType.bulletSize;
        reloadTime = weaponType.reloadTime;
        projectiles = weaponType.projectileCount;
    }
    
    
    public void UpgradeDamage(int amount)
    {
        damage += amount;
        Debug.Log($"Weapon upgraded: +{amount} Damage (Total: {damage})");
    }
    
    public void UpgradeFireRate(float amount)
    {
        fireRate *= amount;
        Debug.Log($"Weapon upgraded: +{amount} Fire Rate (Total: {fireRate})");
    }

    public void UpgradeBulletSize(float amount)
    {
        // TODO: Increase actual bullet size
        bulletSize += amount;
        Debug.Log($"Weapon upgraded: +{amount} Bullet Size (Total: {bulletSize})");
    }

    public void UpgradeReloadTime(float amount)
    {
        reloadTime *= amount;
        Debug.Log($"Weapon upgraded: +{amount} Reload Time (Total: {reloadTime})");
    }

    public void UpgradeProjectiles(int amount)
    {
        projectiles += amount;
        Debug.Log($"Weapon upgraded: +{amount} Projectiles (Total: {projectiles})");
    }
}
