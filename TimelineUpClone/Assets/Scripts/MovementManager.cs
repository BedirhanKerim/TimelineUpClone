using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed = 10f; // İleri hareket hızı
    public float swerveSpeed = 5f; // Sağa-sola hareket hızı
    public float maxSwerveAmount = 2f; // Maksimum sağ-sol hareket mesafesi

    private float lastFrameFingerPositionX;
    private float moveFactorX;
    [SerializeField] private Transform crowdMainObjTransform;
    private bool _bIsStarted = false;
    private bool _bIsGameEnd = false;

    private void Start()
    {
        GameEventManager.Instance.OnLevelComplete += EndGame;
        GameEventManager.Instance.OnLevelFail += EndGame;
        GameEventManager.Instance.OnLevelRestart += RestartGame;

    }

    void Update()
    {
        // İleri hareket
        // Oyuncu dokunduğunda/mouse basıldığında
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFingerPositionX = Input.mousePosition.x;
            if (!_bIsStarted)
            {
                _bIsStarted = true;
                GameEventManager.Instance.LevelStart();
            }
        }
        else if (Input.GetMouseButton(0)) // Parmağı/mouse'u sürüklerken
        {
            float deltaX = Input.mousePosition.x - lastFrameFingerPositionX;
            moveFactorX = deltaX * 0.01f; // Hassasiyet için ölçekleme
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0)) // Parmağı/mouse'u kaldırınca dur
        {
            moveFactorX = 0f;
        }

        if (!_bIsStarted||_bIsGameEnd)
        {
            return;
        }

        crowdMainObjTransform.position += Vector3.forward * speed * Time.deltaTime;
        // Swerve hareketi
        float newX = crowdMainObjTransform.transform.position.x + moveFactorX * swerveSpeed;
        newX = Mathf.Clamp(newX, -maxSwerveAmount, maxSwerveAmount); // Sağ-sol sınırları belirleme

        // Yeni pozisyonu uygula
        crowdMainObjTransform.position =
            new Vector3(newX, crowdMainObjTransform.position.y, crowdMainObjTransform.position.z);
    }

    private void EndGame()
    {
        _bIsGameEnd = true;
        Time.timeScale = 0;
    }
    private void RestartGame()
    {
        _bIsGameEnd = true;
        crowdMainObjTransform.position = new Vector3(0, 1.38f, 1.24f);
        Time.timeScale = 1;
     _bIsStarted = false;
    _bIsGameEnd = false;
    }
}