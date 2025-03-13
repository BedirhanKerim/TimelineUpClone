using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IInteractable
{
    private void Start()
    {
        GameEventManager.Instance.OnLevelRestart += Restart;
    }

    public void Interact(Soldier soldier=null)
    {
        //GameManager.Instance.coinSpawner.SendUiCoin(transform,1);
        GameEventManager.Instance.CoinSpawned(transform, 1, null);
        gameObject.SetActive(false);
        
        //Destroy(this.gameObject);
    }

    private void Restart()
    {
        gameObject.SetActive(true);

    }
}
