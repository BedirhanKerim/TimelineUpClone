using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float _defaultBulletFireRate=1;

    private float _bulletRange=1;
    private float _bulletFireRate=1;
    private int _damage=1;

    [SerializeField] private int soldierLevel;
    [SerializeField] private Transform spawnLocation;

    private float _fireTimer = 0f; // Atış zamanlayıcısı

    private void Start()
    {
        _bulletFireRate = _defaultBulletFireRate;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime; // Geçen zamanı artır

        if (_fireTimer >= _bulletFireRate) // Fire rate süresi dolduysa
        {
            FireBullet();
            _fireTimer = 0f; // Zamanlayıcıyı sıfırla
        }
    }
    private void FireBullet()
    {
        GameManager.Instance.bulletManager.SpawnBullet(spawnLocation,soldierLevel,_damage,_bulletRange);
    }

    public int GetSoldierLevel()
    {
        return soldierLevel;
    }

    public void UpgradeFireRate(float value)
    {
        _bulletFireRate -= _bulletFireRate*value / 100;
    }

    private void OnEnable()
    {
        GameEventManager.Instance.OnFireRateUpgraded += UpgradeFireRate;
    }

    private void OnDisable()
    {
//        GameEventManager.Instance.OnFireRateUpgraded -= UpgradeFireRate;
    }

    private void OnTriggerEnter(Collider other)
  {
      IInteractable interactable = other.GetComponent<IInteractable>();
    
      if (interactable != null)
      {
          interactable.Interact();
      }
  }
}
