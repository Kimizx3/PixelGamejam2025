using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [Header("Reference Setting")]
    [SerializeField] private SO_BulletType bulletType;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SO_WeaponType _weaponType;
    
    [Header("Internal State")]
    private bool _attackLock = false;
    private float nextFireTime = 0f;
    private Animator _animator;
    private AudioSource shootSound;
    private AudioSource reloadSound;
    private PlayerHealth _playerHealth;

    private int currentAmmo;
    private bool isReloading = false;
    private float lastFireTime = 0f;
    
    public SpriteRenderer playerRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public bool IsAttacking { get; private set; }

    [Header("UI Setting")] 
    public TextMeshProUGUI ammoDisplay;
    
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerHealth = GetComponentInParent<PlayerHealth>();
        // Initialize the runtime instance
    }

    private void Start()
    {
        currentAmmo = _weaponType.ammoCount;
        UpdateUI();
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
        if (_playerHealth.isDead) { return; }
        float spreadAngle = 15f;
        float angleStep = WeaponUpgrade.Instance.Projectiles > 1 ? spreadAngle / (WeaponUpgrade.Instance.Projectiles - 1) : 0;
        float startingAngle = -spreadAngle / 2;

        if (isReloading) return;

        if (currentAmmo > 0)
        {
            AudioManager.Instance.PlaySound("fire");
            _animator.SetTrigger("Attack");
            IsAttacking = true;
            _attackLock = true;

            nextFireTime = Time.time + (1f / WeaponUpgrade.Instance.FireRate);
            lastFireTime = Time.time;
            currentAmmo--;

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
                //AudioManager.Instance.PlaySound("bulletDrop");
                UpdateUI();
            }
        }

        if (currentAmmo <= 0)
        {
            AudioManager.Instance.PlaySound("reload");
            StartCoroutine(Reload());
        }

        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(WeaponUpgrade.Instance.ReloadTime);
        _attackLock = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        //if (reloadSound != null) reloadSound.Play();
        yield return new WaitForSeconds(_weaponType.reloadTime);
        currentAmmo = _weaponType.ammoCount;
        isReloading = false;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (!isReloading)
        {
            ammoDisplay.text = $"Ammo: {currentAmmo} / {_weaponType.ammoCount}";
        }
    }
}
