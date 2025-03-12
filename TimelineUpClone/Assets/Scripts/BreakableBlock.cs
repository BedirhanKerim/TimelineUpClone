using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakableBlock : MonoBehaviour,IDamagable,IInteractable
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private TextMeshProUGUI hpText;

    private void Start()
    {
        _currentHp = maxHp;
        hpText.text = _currentHp.ToString();
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp<=0)
        {
            Destroy();
        }
        hpText.text = _currentHp.ToString();

    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void Interact(Soldier soldier)
    {
        GameManager.Instance.crowdManager.DestroySoldier(soldier);
    }
}
