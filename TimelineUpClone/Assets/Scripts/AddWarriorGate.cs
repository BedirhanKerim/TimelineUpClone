using System;
using TMPro;
using UnityEngine;

public class AddWarriorGate : GateBase, IInteractable, IDamagable
{
    public void Interact(Soldier soldier = null)
    {
        if (_bIsTaken) return;

        _bIsTaken = true;
        GameEventManager.Instance.SpawnWarriors(valueCount, 1);
        CloseAnimation();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageEffect();
        
        valueCount = Mathf.Clamp(valueCount + 1, minValue, maxValue);
        valueCountText.text = valueCount >= 0 ? $"+{valueCount}" : valueCount.ToString();
        
        if (valueCount >= 0 && !bIsGreen)
        {
            greenPlane.SetActive(true);
            redPlane.SetActive(false);
            bIsGreen = true;
        }
    }
}