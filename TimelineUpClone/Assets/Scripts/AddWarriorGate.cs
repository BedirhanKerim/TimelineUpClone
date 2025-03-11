using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddWarriorGate : MonoBehaviour,IInteractable,IDamagable
{
    [SerializeField] private int spawnCount;
    private bool _bIsTaken = false;
    [SerializeField] private GameObject redPlane, greenPlane;
    [SerializeField] private TextMeshProUGUI valueCount;
    private bool bIsGreen = false;
    private void Start()
    {
        if (spawnCount<0)
        {
            valueCount.text = spawnCount.ToString();
            bIsGreen = false;
            greenPlane.SetActive(false);
            redPlane.SetActive(true);
        }
        else
        {
            valueCount.text = "+" + spawnCount;
            bIsGreen = true;
            greenPlane.SetActive(true);
            redPlane.SetActive(false);

        }
    }

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
        spawnCount ++;
        if (spawnCount<0)
        {
            valueCount.text = spawnCount.ToString();
        }
        else
        {
            valueCount.text = "+" + spawnCount;
            if (!bIsGreen)
            {
                greenPlane.SetActive(true);
                redPlane.SetActive(false);

                bIsGreen = true;
            }

        }
    }
    
}
