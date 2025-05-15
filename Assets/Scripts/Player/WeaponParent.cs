using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [SerializeField] private SO_BulletType bulletType;
    [SerializeField] private Transform firePoint;
    private Animator _animator;
    private bool _attackLock;
    
    public SpriteRenderer playerRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public float delay = 0.3f;
    
    public bool IsAttacking { get; private set; }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
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
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }

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

    public void OnShootPerformed()
    {
        if (_attackLock)
        {
            return;
        }
        _animator.SetTrigger("Attack");
        IsAttacking = true;
        _attackLock = true;
        
        
        Vector2 shootDirection = (PointerPosition - (Vector2)firePoint.position).normalized;
        GameObject bullets = 
            Instantiate(bulletType.bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullets.GetComponent<Rigidbody2D>();
        StartCoroutine(DelayAttack());

        if (rb != null)
        {
            rb.velocity = shootDirection * bulletType.bulletSpeed;
        }
        Destroy(bullets, bulletType.bulletLifeTime);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        _attackLock = false;
    }
}
