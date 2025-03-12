using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem damageParticle;

    private void Start()
    {
        GameEventManager.Instance.OnSpawnDamageParticle += SpawnDamageParticle;
    }

    public void SpawnDamageParticle(Vector3 spawnPosition)
    {

        var instance = LeanPool.Spawn(damageParticle);
        instance.transform.position = new Vector3(spawnPosition.x, spawnPosition.y , spawnPosition.z);
        instance.gameObject.SetActive(true);
        instance.Play();
        DOVirtual.DelayedCall(1f, () =>
        {
            LeanPool.Despawn(instance);
            instance.gameObject.SetActive(false);

        });
    }
}