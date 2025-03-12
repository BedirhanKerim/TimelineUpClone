using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddWarriorGate : GateBase,IInteractable,IDamagable
{



    public void Interact(Soldier soldier=null)
    {
        if (_bIsTaken)
        {
            return;
        }

        _bIsTaken = true;
//        Debug.Log("Kapıyla etkileşime geçildi, savaşçı eklendi!");
        //GameManager.Instance.crowdManager.SpawnSoldier(valueCount);
        GameEventManager.Instance.SpawnWarriors(valueCount,1);
       // Destroy(this.gameObject);
      // transform.gameObject.SetActive(false);
       CloseAnimation();
    }

    public void TakeDamage(int damage)
    {        TakeDamageEffect();

        valueCount ++;
        valueCount = Mathf.Clamp(valueCount, minValue, maxValue);
        if (valueCount<0)
        {
            
            valueCountText.text = valueCount.ToString();
        }
        else
        {
            valueCountText.text = "+" + valueCount;
            if (!bIsGreen)
            {
                greenPlane.SetActive(true);
                redPlane.SetActive(false);

                bIsGreen = true;
            }

        }
    }
    
}
