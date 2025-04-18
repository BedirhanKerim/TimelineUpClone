using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] private List<Soldier> soldiers = new List<Soldier>(); // Askerleri tutan liste
    [SerializeField] private GameObject[] soldierPrefabs = new GameObject[3]; // 3 farklı asker prefabı
    [SerializeField] private Transform crowdMainObj;
    [SerializeField] private Vector3[] soldierLocalPositions = new Vector3[36]; // 3 farklı asker prefabı
    private bool _bIsStarted = false;
    private void Start()
    {
        GameEventManager.Instance.OnLevelStart += GameStarted;
        GameEventManager.Instance.OnLevelRestart += GameRestart;
        GameEventManager.Instance.OnLevelComplete += GameEnd;
        GameEventManager.Instance.OnLevelFail += GameEnd;
        GameEventManager.Instance.OnUpgradeWarriors += UpgradeWarriors;
        GameEventManager.Instance.OnDestroyWarriors += DestroySoldier;
        GameEventManager.Instance.OnSpawnWarriors += SpawnSoldier;

        for (int i = 0; i < crowdMainObj.childCount; i++)
        {
            soldierLocalPositions[i]= crowdMainObj.GetChild(i).localPosition;

        }
        SpawnSoldier(1, 1);
    }

    public void SpawnSoldier(int spawnCount, int soldierLevel = 1)
    {
        if (soldierLevel < 1 || soldierLevel > soldierPrefabs.Length)
        {
            Debug.LogWarning("Geçersiz soldierLevel: " + soldierLevel);
            return;
        }

        if (spawnCount>0)
        {
            
       
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newSoldier = Instantiate(soldierPrefabs[soldierLevel - 1], transform.position, Quaternion.identity);
            var soldierInstance = newSoldier.transform.GetComponent<Soldier>();
            soldiers.Add(soldierInstance);
            newSoldier.transform.parent = crowdMainObj;
            if (_bIsStarted)
            {
                soldierInstance.LevelStart();
            }
        }

        for (int i = 0; i < soldiers.Count; i++)
        {
            if (i > 34)
            {
                soldiers[i].transform.localPosition = soldierLocalPositions[0];

            }
            else
            {
                soldiers[i].transform.localPosition = soldierLocalPositions[i];

            }
        }
        }
        else  if (spawnCount<0)
        {
            int removeCount = Mathf.Abs(spawnCount); 

            if (soldiers.Count == 0)
            {
                Debug.LogWarning("Asker bulunmuyor, silinecek bir şey yok!");
                return;
            }

            int soldiersToRemove = Mathf.Min(removeCount, soldiers.Count); 

            for (int i = 0; i < soldiersToRemove; i++)
            {
                var soldierToRemove = soldiers[soldiers.Count - 1]; 
                soldiers.RemoveAt(soldiers.Count - 1); 
                DestroySoldier(soldierToRemove); 
            }
            

        }
    }

    public void DestroySoldier(Soldier soldier,bool isDestroyProcess=false)
    {
        if (soldiers.Contains(soldier))
        {
            soldiers.Remove(soldier);
            Destroy(soldier.gameObject); 
        }
        if (soldiers.Count == 0&&isDestroyProcess)
        {
            GameEventManager.Instance.LevelFail();
            Debug.Log("123");
        }
    }

    public void UpgradeWarriors(int addUpgradeLevelValue)
    {
         List<int> tempSoldiers = new List<int>();
         for (int i = 0; i < soldiers.Count; i++)
         {
           var soldierLevel=  soldiers[i].GetSoldierLevel();
           soldierLevel += addUpgradeLevelValue;
           soldierLevel = Mathf.Clamp(soldierLevel, 1, 3);
           tempSoldiers.Add(soldierLevel);
         }
         while (soldiers.Count > 0) 
         {
             DestroySoldier(soldiers[0]); 
         }
         foreach (int soldierLevel in tempSoldiers)
         {
             SpawnSoldier(1,soldierLevel);
         }
    }

    private void GameStarted()
    {
        _bIsStarted = true;
    }
    private void GameEnd()
    {
        _bIsStarted = false;
    }
    private void GameRestart()
    { while (soldiers.Count > 0) 
        {
            DestroySoldier(soldiers[0]); 
        }
        DOVirtual.DelayedCall(.2f, () => 
        {        SpawnSoldier(1, 1);

        });
    }
}
