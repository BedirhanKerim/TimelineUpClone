using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _coinImageTransform;
    [SerializeField] private UICoin _uiCoinPrefab;
    [SerializeField] private Transform _parentUiCoin;

    private void Start()
    {
       // GameEventManager.Instance.OnCoinSpawn += SendUiCoin;
    }

    public void SendUiCoin(Transform worldPos, Action callback, int coinAmount)
    {
        var newUiCoin = Instantiate(_uiCoinPrefab);
        newUiCoin.transform.SetParent(_parentUiCoin);

        Vector3 spawnPos = GetScreenPos(worldPos.position);
        Vector3 randomPos = GetRandomScreenPos(worldPos.position);

        newUiCoin.Move(spawnPos, randomPos, _coinImageTransform.position, 0.5f, callback, coinAmount);

    }
    private Vector3 GetScreenPos(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        return screenPos;
    }
    private Vector3 GetRandomScreenPos(Vector3 worldPos)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector3 randomizedSpawnPos = new Vector3(
            spawnPos.x + UnityEngine.Random.Range(-250, 250),
            spawnPos.y + UnityEngine.Random.Range(-250, 250),
            spawnPos.z);
        return randomizedSpawnPos;
    }
}