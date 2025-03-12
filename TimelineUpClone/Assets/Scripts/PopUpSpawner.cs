using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private DamagePopUp popUpPrefab;

    private void Start()
    {
        GameEventManager.Instance.OnSpawnPopUp+=SpawnPopUp;
    }

    private void SpawnPopUp(Vector3 spawnLocation,int value)
    {
        DamagePopUp popUp= LeanPool.Spawn(popUpPrefab);
        popUp.transform.position = spawnLocation;
        popUp.SetPopUp(value);
    }
}
