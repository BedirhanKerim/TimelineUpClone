using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float _defaultBulletFireRate=1f;

    private float _bulletRange=1;
    private float _bulletFireRate=1;
    [SerializeField]private int _damage=1;

    [SerializeField] private int soldierLevel;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private AttackAnimHandler attackAnimHandler;
    [SerializeField] private Animator animator;
    private float _fireTimer = 0f; // Atış zamanlayıcısı

    private void Start()
    {
        _bulletFireRate = _defaultBulletFireRate;
        attackAnimHandler.Attack += FireBullet;

    }

    private void Update()
    {
     /*   _fireTimer += Time.deltaTime; // Geçen zamanı artır

        if (_fireTimer >= _bulletFireRate) // Fire rate süresi dolduysa
        {
            FireBullet();
            _fireTimer = 0f; // Zamanlayıcıyı sıfırla
        }*/
    }
    private void FireBullet()
    {
       // GameManager.Instance.bulletManager.SpawnBullet(spawnLocation,soldierLevel,_damage,_bulletRange);
        GameEventManager.Instance.SpawnBullet(spawnLocation,soldierLevel,_damage,_bulletRange);
    }

    public int GetSoldierLevel()
    {
        return soldierLevel;
    }

    public void UpgradeFireRate(float value)
    {
        _bulletFireRate += 0.01f*value;
        animator.SetFloat("ThrowSpeed", _bulletFireRate);        
    }

    private void OnEnable()
    {
        GameEventManager.Instance.OnFireRateUpgraded += UpgradeFireRate;
        GameEventManager.Instance.OnLevelStart += LevelStart;

    }

    private void OnDisable()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnFireRateUpgraded -= UpgradeFireRate;
            GameEventManager.Instance.OnLevelStart -= LevelStart;
        }
    }

    private void OnTriggerEnter(Collider other)
  {
      IInteractable interactable = other.GetComponent<IInteractable>();
    
      if (interactable != null)
      {
          interactable.Interact(this);
      }
  }

    public void LevelStart()
    {
        animator.SetBool("IsGameStarted",true);
    }
    
}
