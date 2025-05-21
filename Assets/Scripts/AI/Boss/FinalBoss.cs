using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [Header("Reference Settings")] 
    [SerializeField] private GameObject zombieFlagPrefab;
    [SerializeField] private GameObject shockwavePrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private SO_EnemyType enemyType;
    

    [Header("Ability Settings")] 
    [SerializeField] private float zombieFlagCooldown = 10f;
    [SerializeField] private float shockwaveCooldown = 15f;
    [SerializeField] private float bulletCircleCooldown = 8f;
    [SerializeField] private int bulletsPerCircle = 12;
    [SerializeField] private float bulletSpeed = 5f;

    private Transform player;
    private bool canShockwave = true;
    private bool canShootCircle = true;
    private Rigidbody2D rb;
    private int currentHealth;
    private bool isDead = false;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;

    private ZombieFlag _zombieFlag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentHealth = enemyType.maxHealth;
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(SummonZombieFlag());
    }

    private void Update()
    {
        if (canShockwave)
        {
            StartCoroutine(CastShockwave());
        }
        if (canShootCircle)
        {
            StartCoroutine(ShootBulletInCircle());
        }
    }

    private IEnumerator SummonZombieFlag()
    {
        while (true)
        {
            if (_zombieFlag == null)
            {
                //Debug.Log("[Final Boss] Summoning Zombie Flag");
                GameObject flag = 
                    Instantiate(zombieFlagPrefab, 
                        transform.position + Vector3.right * 8, 
                        Quaternion.identity);
                _zombieFlag = flag.GetComponent<ZombieFlag>();

                _zombieFlag.OnFlagDestroyed += HandleFlagDestroyed;
            }
            yield return new WaitForSeconds(zombieFlagCooldown);
        }
    }

    private void HandleFlagDestroyed()
    {
        _zombieFlag = null;
    }

    private IEnumerator CastShockwave()
    {
        canShockwave = false;
        //Debug.Log("[Final Boss] Summoning Shockwave");

        GameObject shockwave = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);

        Vector3 dir = (player.position - transform.position).normalized;
        shockwave.GetComponent<Rigidbody2D>().velocity = dir * 5f;
        
        _animator.SetTrigger("Attack");

        yield return new WaitForSeconds(shockwaveCooldown);
        canShockwave = true;
    }

    private IEnumerator ShootBulletInCircle()
    {
        canShootCircle = false;
        //Debug.Log("[Final Boss] Shooting bullets in circle");

        float angleStep = 360f / bulletsPerCircle;

        for (int i = 0; i < bulletsPerCircle; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpeed;
        }
        _animator.SetTrigger("Attack");

        yield return new WaitForSeconds(bulletCircleCooldown);

        canShootCircle = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die");
        StartCoroutine(PlayDeathAnim());
    }

    private IEnumerator PlayDeathAnim()
    {
        rb.velocity = Vector2.zero;
        _capsuleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        GameManager.Instance?.ShowVictoryScreen();
        EnemySpawner.Instance?.StopSpawning();
    }
}
