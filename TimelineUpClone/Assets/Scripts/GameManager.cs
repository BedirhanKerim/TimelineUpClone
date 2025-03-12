using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
 public CrowdManager crowdManager;
 public BulletManager bulletManager;
 public CoinSpawner coinSpawner;
 private int _totalCoin=0;
 [SerializeField] private TextMeshProUGUI coinText;

 private void Start()
 {
     //playerpreften alınacak
     coinText.text = _totalCoin.ToString();
     GameEventManager.Instance.OnCoinEarned += CoinEared;
 }

 private void CoinEared(int value)
 {
     _totalCoin += value;
     coinText.text = _totalCoin.ToString();

 }
}
