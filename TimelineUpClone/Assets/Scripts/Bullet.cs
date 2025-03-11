using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private bool _bIsActive;

    [SerializeField]private float speed = 20f; // Mermi hızı

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
    public void SetBullet(int damage,float range)
    {
        _damage = damage;
        _bIsActive = true;
        Invoke(nameof(Destroy),range);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
    
        if (damagable != null&&_bIsActive)
        {
            _bIsActive = false;
            damagable.TakeDamage(_damage);
            CancelInvoke(nameof(Destroy));
            Destroy();


        }
    }
}
