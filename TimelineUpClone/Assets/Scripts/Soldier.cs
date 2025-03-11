using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
      IInteractable interactable = other.GetComponent<IInteractable>();
    
      if (interactable != null)
      {
          interactable.Interact();
      }
  }
}
