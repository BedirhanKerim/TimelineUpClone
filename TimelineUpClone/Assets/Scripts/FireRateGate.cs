using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateGate : GateBase,IInteractable,IDamagable
{
    public void Interact()
    {
        if (_bIsTaken)
        {
            return;
        }

        _bIsTaken = true;
        Debug.Log("Kapıyla etkileşime geçildi, savaşçı FireRate!");
        GameEventManager.Instance.FireRateUpgrade(valueCount);   
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
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
