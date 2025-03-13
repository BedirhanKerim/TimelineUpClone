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
    private bool _bIsBouncing = false;

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

        TakeDamageEffect();
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
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f) 
            .SetEase(Ease.InBack)
            .OnComplete(() => 
            {
                gameObject.SetActive(false); 
            });
        for (int i = 0; i < 3; i++)
        {
            GameEventManager.Instance.CoinSpawned(transform, 1, null);
        }
    }
    public void TakeDamageEffect()
    {
        if (_bIsBouncing)
        {
            return;
        }

        _bIsBouncing = true;
        transform.DOScale(2.5f, 0.1f) 
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(2f, 0.1f) 
                    .SetEase(Ease.InQuad).OnComplete(() => { _bIsBouncing = false; });
            });
    }
    public void Interact(Soldier soldier)
    {
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
