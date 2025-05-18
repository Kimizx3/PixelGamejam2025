using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [Header("Reference Setting")]
    [SerializeField] private SO_BulletType bulletType;
    [SerializeField] private Transform firePoint;
    
    [Header("Internal State")]
    private bool _attackLock;
    private float nextFireTime = 0.1f;
    private Animator _animator;
    
    public SpriteRenderer playerRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public bool IsAttacking { get; private set; }
    
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        // Initialize the runtime instance
    }

    public void Update()
    {
        PositioningWeapon();
    }

    public void PositioningWeapon()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        scale.y = direction.x < 0 ? -1 : 1;

        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
        }
    }

    public void Fire()
    {
        float spreadAngle = 15f;
        float angleStep = WeaponUpgrade.Instance.Projectiles > 1 ? spreadAngle / (WeaponUpgrade.Instance.Projectiles - 1) : 0;
        float startingAngle = -spreadAngle / 2;
        
        if (_attackLock || Time.time < nextFireTime)
        {
            return;
        }
        
        _animator.SetTrigger("Attack");
        IsAttacking = true;
        _attackLock = true;
        
        nextFireTime = Time.time + (1f / WeaponUpgrade.Instance.FireRate);

        int middleIndex = WeaponUpgrade.Instance.Projectiles / 2;
        
        for (int i = 0; i < WeaponUpgrade.Instance.Projectiles; i++)
        {
            float angle;
            if (i == middleIndex)
            {
                angle = 0;
            }
            else
            {
                angle = startingAngle + (i * angleStep);
            }
            
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            
            Vector2 shootDirection = rotation * (PointerPosition - (Vector2)firePoint.position).normalized;
            GameObject bullets = 
                Instantiate(WeaponUpgrade.Instance.GetBulletType().bulletPrefab, firePoint.position, rotation);
            
            bullets.transform.localScale *= WeaponUpgrade.Instance.BulletSize;
            
            Bullet bullet = bullets.GetComponent<Bullet>();
            
            if (bullet != null)
            {
                bullet.Initialize(
                    WeaponUpgrade.Instance.Damage,
                    bulletType.isPiercing,
                    bulletType.bulletSpeed,
                    bulletType.bulletLifeTime);
            }
            bullet.LaunchBullet(shootDirection, bullet);
        }
        
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(WeaponUpgrade.Instance.ReloadTime);
        _attackLock = false;
    }
}
