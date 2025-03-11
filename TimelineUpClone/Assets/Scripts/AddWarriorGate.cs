using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWarriorGate : MonoBehaviour,IInteractable,IDamagable
{
    [SerializeField] private int spawnCount;
    private bool _bIsTaken = false;
    public void Interact()
    {
        if (_bIsTaken)
        {
            return;
        }

        _bIsTaken = true;
        Debug.Log("Kapıyla etkileşime geçildi, savaşçı eklendi!");
        GameManager.Instance.crowdManager.SpawnSoldier(spawnCount);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        spawnCount += damage;
    }
    
}
