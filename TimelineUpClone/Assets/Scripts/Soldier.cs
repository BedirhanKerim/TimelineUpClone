using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float _defaultBulletFireRate = 1f;

    private float _bulletRange = .75f;
    private float _bulletFireRate = 1;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int soldierLevel;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private AttackAnimHandler attackAnimHandler;
    [SerializeField] private Animator animator;
    private void Start()
    {
        _bulletFireRate = _defaultBulletFireRate;
        attackAnimHandler.Attack += FireBullet;
    }


    private void FireBullet()
    {
        GameEventManager.Instance.SpawnBullet(spawnLocation, soldierLevel, _damage, _bulletRange);
    }

    public int GetSoldierLevel()
    {
        return soldierLevel;
    }

    public void UpgradeFireRate(float value)
    {
        _bulletFireRate += 0.01f * value;
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
        animator.SetBool("IsGameStarted", true);
    }
}