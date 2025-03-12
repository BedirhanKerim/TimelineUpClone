using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IInteractable
{
    public void Interact(Soldier soldier=null)
    {
        //GameManager.Instance.coinSpawner.SendUiCoin(transform,1);
        GameEventManager.Instance.CoinSpawned(transform, 1, null);
        Destroy(this.gameObject);
    }
}
