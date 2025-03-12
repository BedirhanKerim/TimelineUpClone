using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelStartPanel, levelFailPanel, levelCompletePanel;
    void Start()
    {
        GameEventManager.Instance.OnLevelStart += LevelStart;
        GameEventManager.Instance.OnLevelComplete += LevelComplete;
        GameEventManager.Instance.OnLevelFail += LevelFail;
        GameEventManager.Instance.OnLevelRestart += LevelRestart;

    }

    private void LevelStart()
    {
        levelStartPanel.SetActive(false);
    }

    private void LevelFail()
    {
        levelFailPanel.SetActive(true);
    }

    private void LevelComplete()
    {
        levelCompletePanel.SetActive(true);

    }
    private void LevelRestart()
    {
        levelStartPanel.SetActive(true);
        levelFailPanel.SetActive(false);
        levelCompletePanel.SetActive(false);



    }

    public void RestartGameButton()
    {
        GameEventManager.Instance.LevelRestart();
    }
}