using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : Singleton<GameEventManager>
{
    public event UnityAction<float> OnFireRateUpgraded;
    public event UnityAction<int> OnCoinEarned;
    public event UnityAction<Transform, int, Action> OnSpawnedCoin;

    public event UnityAction<float> OnCubeExpEarned;
    public event UnityAction OnLevelFail;
    public event UnityAction OnLevelComplete;
    public event UnityAction OnLevelStart;
    public event UnityAction OnLevelRestart;
    public event UnityAction<Vector3,int> OnSpawnPopUp;

    public event UnityAction<Vector3> OnSpawnDamageParticle;
    
    public void FireRateUpgrade(float value)
    {
        OnFireRateUpgraded?.Invoke(value);
    }

    public void CoinEarned(int value)
    {
        OnCoinEarned?.Invoke(value);
    }
    public void CoinSpawned(Transform transform, int value, Action callback)
    {
        OnSpawnedCoin?.Invoke(transform,value,callback);
    }
    public void CubeExpEarned(float value)
    {
        OnCubeExpEarned?.Invoke(value);
    }

    public void LevelFail()
    {
        OnLevelFail?.Invoke();
    }

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void LevelStart()
    {
        OnLevelStart?.Invoke();
    }
    public void LevelRestart()
    {
        OnLevelRestart?.Invoke();
    }
    public void SpawnPopUp(Vector3 spawnLocation,int value)
    {
        OnSpawnPopUp?.Invoke(spawnLocation,value);
    }
    public void SpawnDamageParticle(Vector3 spawnLocation)
    {
        OnSpawnDamageParticle?.Invoke(spawnLocation);
    }
}