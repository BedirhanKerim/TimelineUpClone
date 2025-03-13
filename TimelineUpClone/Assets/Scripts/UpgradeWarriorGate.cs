using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWarriorGate : GateBase, IInteractable, IDamagable
{
    public void Interact(Soldier soldier = null)
    {
        if (_bIsTaken) return;

        _bIsTaken = true;
        GameEventManager.Instance.UpgradeWarriors(valueCount);
        CloseAnimation();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageEffect();
        valueCount = Mathf.Clamp(valueCount + 1, minValue, maxValue);
        valueCountText.text = (valueCount >= 0) ? $"+{valueCount}" : valueCount.ToString();

        if (valueCount >= 0 && !bIsGreen)
        {
            greenPlane.SetActive(true);
            redPlane.SetActive(false);
            bIsGreen = true;
        }
    }
}
