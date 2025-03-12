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
       // GameManager.Instance.crowdManager.UpgradeWarriors(valueCount);
        GameEventManager.Instance.UpgradeWarriors(valueCount);
       // Destroy(this.gameObject);
       // transform.gameObject.SetActive(false);
        CloseAnimation();

    }

    public void TakeDamage(int damage)
    {
        TakeDamageEffect();
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
