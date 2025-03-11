using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float _bulletRange=1;
    private float _bulletFireRate=1;
    private int _damage=1;

    [SerializeField] private int soldierLevel;
    [SerializeField] private Transform spawnLocation;

    private float _fireTimer = 0f; // Atış zamanlayıcısı

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
  private void OnTriggerEnter(Collider other)
  {
      IInteractable interactable = other.GetComponent<IInteractable>();
    
      if (interactable != null)
      {
          interactable.Interact();
      }
  }
}
