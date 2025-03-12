using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BreakableBlock : MonoBehaviour,IDamagable,IInteractable
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private TextMeshProUGUI hpText;
    private bool _bIsAlive=true;
    private void Start()
    {
        _currentHp = maxHp;
        hpText.text = _currentHp.ToString();
        GameEventManager.Instance.OnLevelRestart += Restart;
    }

    public void TakeDamage(int damage)
    {
        if (!_bIsAlive)
        {
            return;
        }
        _currentHp -= damage;
        if (_currentHp<=0)
        {
            _currentHp = 0;
            _bIsAlive = false;
            Destroy();
        }
        hpText.text = _currentHp.ToString();

    }

    private void Destroy()
    {
        //Destroy(this.gameObject);
        transform.DOScale(Vector3.zero, 0.5f) // 0.5 saniyede sıfıra küçül
            .SetEase(Ease.InBack) // Geriye çekilerek küçülsün
            .OnComplete(() => 
            {
                gameObject.SetActive(false); // Objeyi kapat veya başka bir işlem yap
            });
        for (int i = 0; i < 3; i++)
        {
            GameEventManager.Instance.CoinSpawned(transform, 1, null);
        }
    }

    public void Interact(Soldier soldier)
    {
      //  GameManager.Instance.crowdManager.DestroySoldier(soldier,true);
        GameEventManager.Instance.DestroyWarriors(soldier,true);
    }

    private void Restart()
    {
        transform.localScale=Vector3.one*2;
        _bIsAlive = true;
        _currentHp = maxHp;
        hpText.text = _currentHp.ToString();
        transform.gameObject.SetActive(true);
    }
}
