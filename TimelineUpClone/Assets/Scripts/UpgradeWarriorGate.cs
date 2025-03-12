using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWarriorGate : GateBase,IInteractable,IDamagable
{
    public void Interact(Soldier soldier=null)
    {
        if (_bIsTaken)
        {
            return;
        }

        _bIsTaken = true;
        Debug.Log("Kapıyla etkileşime geçildi, savaşçı Upgrade!");
        GameManager.Instance.crowdManager.UpgradeWarriors(valueCount);
       // Destroy(this.gameObject);
        transform.gameObject.SetActive(false);

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
