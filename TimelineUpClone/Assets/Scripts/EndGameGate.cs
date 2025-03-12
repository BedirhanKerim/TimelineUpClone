using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameGate : MonoBehaviour,IInteractable
{
    public void Interact(Soldier soldier)
    {
        GameEventManager.Instance.LevelComplete();
    
    }
}
