using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackAnimHandler : MonoBehaviour
{
    public event Action Attack;
    public void AnimEventAttack()
    {
        Attack?.Invoke();
        // Debug.Log("Animation Event Triggered: ");
    }

}
