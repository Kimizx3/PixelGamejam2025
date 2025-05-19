using UnityEngine;


[CreateAssetMenu(fileName = "Bullet_", menuName = "Bullet_Data")]
public class SO_BulletType : ScriptableObject
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 5f;
    public int damage = 1;
    public bool isPiercing = false;

    [Header("Effects")] 
    public float stunDuration = 3f;
}
