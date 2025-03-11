using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] private List<Transform> soldiers = new List<Transform>(); // Askerleri tutan liste
    [SerializeField] private GameObject[] soldierPrefabs = new GameObject[3]; // 3 farklı asker prefabı
    [SerializeField] private Transform crowdMainObj;
    [SerializeField] private Vector3[] soldierLocalPositions = new Vector3[36]; // 3 farklı asker prefabı

    private void Start()
    {
        for (int i = 0; i < crowdMainObj.childCount; i++)
        {
            soldierLocalPositions[i]= crowdMainObj.GetChild(i).localPosition;

        }
    }

    public void SpawnSoldier(int spawnCount, int soldierLevel = 1)
    {
        if (soldierLevel < 1 || soldierLevel > soldierPrefabs.Length)
        {
            Debug.LogWarning("Geçersiz soldierLevel: " + soldierLevel);
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newSoldier = Instantiate(soldierPrefabs[soldierLevel - 1], transform.position, Quaternion.identity);
            soldiers.Add(newSoldier.transform);
            newSoldier.transform.parent = crowdMainObj;
        }

        for (int i = 0; i < soldiers.Count; i++)
        {
            soldiers[i].localPosition = soldierLocalPositions[i];
        }
    }

    private void DestroySoldier(Transform soldier)
    {
        if (soldiers.Contains(soldier))
        {
            soldiers.Remove(soldier);
            Destroy(soldier.gameObject); // Askeri sahneden kaldır
        }
    }

    public void Deneme()
    {
        SpawnSoldier(5, 1);
    }
    
}
