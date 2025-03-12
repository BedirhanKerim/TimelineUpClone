using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : Singleton<GameEventManager>
{
    public event UnityAction<float> OnFireRateUpgraded;
    public event UnityAction<int> OnCoinEarned;
    public event UnityAction<float> OnCubeExpEarned;

    public void FireRateUpgrade(float value)
    {
        OnFireRateUpgraded?.Invoke(value);
    }

    public  void CoinEarned(int value)
    {
        OnCoinEarned?.Invoke(value);
    }
    public  void CubeExpEarned(float value)
    {
        OnCubeExpEarned?.Invoke(value);
    }
}
