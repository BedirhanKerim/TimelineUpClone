using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletPrefabs = new GameObject[3]; // 3 farklı asker prefabı

    public void SpawnBullet(Transform spawnPosition, int bulletLevel, int damage,float range)
    {
        if (bulletLevel < 0 || bulletLevel >= bulletPrefabs.Length)
        {
            Debug.LogWarning("Geçersiz bulletLevel: " + bulletLevel);
            return;
        }

        // Mermiyi oluştur
        GameObject instance = Instantiate(bulletPrefabs[bulletLevel], spawnPosition.position, Quaternion.identity);
     var bullet=   instance.GetComponent<Bullet>();
     bullet.SetBullet(damage,range);
    }
}
