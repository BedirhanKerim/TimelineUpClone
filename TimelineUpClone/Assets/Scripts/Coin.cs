using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IInteractable
{
    public void Interact(Soldier soldier=null)
    {
        GameManager.Instance.coinSpawner.SendUiCoin(transform,null,1);
        Destroy(this.gameObject);
    }
}
