using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : Singleton<GameEventManager>
{
    public event UnityAction<float> OnFireRateUpgraded;

    public void FireRateUpgrade(float value)
    {
        OnFireRateUpgraded?.Invoke(value);
    }
}
