using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private  Transform target; // Takip edilecek obje (crowdMainObjTransform)
    [SerializeField]private float zOffset = -10f; // Kameranın takip mesafesi (sadece Z ekseni için)
    [SerializeField]private float smoothSpeed = 0.125f; // X eksenindeki takip hızı
    void LateUpdate()
    {
        // X ekseni için Lerp ile yumuşak hareket
        float newX = Mathf.Lerp(transform.position.x, target.position.x, smoothSpeed);

        // Z ekseni için sabit takip mesafesi
        float newZ = target.position.z + zOffset;

        // Yeni pozisyonu uygula
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }
}
