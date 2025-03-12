using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletPrefabs = new GameObject[3]; // 3 farklı asker prefabı

    private void Start()
    {
        GameEventManager.Instance.OnSpawnBullet += SpawnBullet;
    }

    public void SpawnBullet(Transform spawnPosition, int bulletLevel, int damage,float range)
    {
        GameObject instance = LeanPool.Spawn(bulletPrefabs[bulletLevel - 1], spawnPosition.position, Quaternion.identity);

        var bullet = instance.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetBullet(damage, range);
        }
    }
}
