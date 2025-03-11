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
    void Update()
    {
        // İleri hareket
        crowdMainObjTransform.position += Vector3.forward * speed * Time.deltaTime;
        // Oyuncu dokunduğunda/mouse basıldığında
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFingerPositionX = Input.mousePosition.x;
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

        // Swerve hareketi
        float newX = crowdMainObjTransform.transform.position.x + moveFactorX * swerveSpeed;
        newX = Mathf.Clamp(newX, -maxSwerveAmount, maxSwerveAmount); // Sağ-sol sınırları belirleme

        // Yeni pozisyonu uygula
        crowdMainObjTransform.position = new Vector3(newX, crowdMainObjTransform.position.y, crowdMainObjTransform.position.z);
    }
}

