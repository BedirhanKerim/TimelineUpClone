using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeStation : MonoBehaviour, IDamagable, IInteractable
{
    public GameObject cubePrefab;
    public int numColumns = 16; // Dairede kaç sütun var
    public int numRows = 10; // Yükseklik kaç sıra
    public float radius = 2f; // Silindirin yarıçapı
    public float cubeSize = 0.5f; // Küplerin boyutu
    [SerializeField] private Material[] materials;
    [SerializeField] private List<CircleCubeDatas> outerCircleData = new();
    [SerializeField] private List<CircleCubeDatas> innerCircleData = new();
    [SerializeField] private List<Transform> outerCircleCubes = new();
    [SerializeField] private List<Transform> innerCircleCubes = new();
    [SerializeField] private MeshRenderer colorDiskRenderer;
    [SerializeField] private RectTransform cubeTargetTransform;
    private bool _bIsAlive = true;
    private bool _bCanTakeDamage = true;
    [SerializeField] private Transform centerCylinder;
    private bool _bIsBouncing = false;

    [Serializable]
    public class CircleCubeDatas
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    private void Awake()
    {
        DOTween.SetTweensCapacity(1000, 50);
    }

    void Start()
    {
        // GenerateCylinder();
        SpawnAllCubes();
        GameEventManager.Instance.OnLevelRestart += GameRestart;
    }

    void SpawnAllCubes()
    {
        ClearCubes();
        var mat1 = materials[Random.Range(2, materials.Length)];
        var mat2 = materials[Random.Range(2, materials.Length)];

        foreach (var cubeData in outerCircleData)
        {
            SpawnCube(cubeData.Position, cubeData.Rotation, mat1, outerCircleCubes);
        }

        foreach (var cubeData in innerCircleData)
        {
            SpawnCube(cubeData.Position, cubeData.Rotation, mat2, innerCircleCubes);
        }

        colorDiskRenderer.sharedMaterial = materials[Random.Range(2, materials.Length)];
    }

    private void SpawnCube(Vector3 position, Quaternion rotation, Material material, List<Transform> list)
    {
        var cube = LeanPool.Spawn(cubePrefab);
        Transform t = cube.transform;

        t.SetParent(transform);
        t.localPosition = position;
        t.localRotation = rotation;
        t.localScale = Vector3.one * cubeSize;

        list.Add(t);

        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.sharedMaterial = material;
    }

    void GenerateCylinder()
    {
        for (int y = 0; y < numRows; y++)
        {
            for (int i = 0; i < numColumns; i++)
            {
                float angle = i * Mathf.PI * 2 / numColumns;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                float yPos = 0.7f + y * cubeSize;

                GameObject cube = Instantiate(cubePrefab, new Vector3(x, yPos, z), Quaternion.identity, transform);
                cube.transform.LookAt(new Vector3(0, yPos, 0));
                cube.transform.Rotate(0, 90, 0);

                cube.transform.localScale = Vector3.one * cubeSize;
                float randomXRotation = Random.Range(-10f, 10f);
                cube.transform.Rotate(randomXRotation, 0, 0, Space.Self);
                CircleCubeDatas cubeData = new();
                cubeData.Position = cube.transform.position;
                cubeData.Rotation = cube.transform.rotation;
                outerCircleData.Add(cubeData);
            }
        }
    }

    public void DenemeDestroyOutCircle()
    {
        foreach (var cube in outerCircleCubes)
        {
            cube.gameObject.SetActive(false);
        }

        outerCircleCubes.Clear();
        SpawnNewCircleAndReLocate();
    }
    void ClearCubes()
    {
        foreach (var cube in outerCircleCubes)
        {
            if (cube != null)
            {
                LeanPool.Despawn(cube);
            }
        }
        foreach (var cube in innerCircleCubes)
        {
            if (cube != null)
            {
                LeanPool.Despawn(cube);
            }
        }
        outerCircleCubes.Clear();
        innerCircleCubes.Clear();

    }
    public void SpawnNewCircleAndReLocate()
    {
        _bCanTakeDamage = false;
        foreach (var cube in innerCircleCubes)
        {
            outerCircleCubes.Add(cube.transform);
        }

        innerCircleCubes.Clear();
        for (int i = 0; i < outerCircleCubes.Count; i++)
        {
            outerCircleCubes[i].DOLocalMove(outerCircleData[i].Position, .2f)
                .SetEase(Ease.OutQuad); 
            outerCircleCubes[i].localRotation = outerCircleData[i].Rotation;
        }

        foreach (var cubeData in innerCircleData)
        {
            var cube = LeanPool.Spawn(cubePrefab).transform;
            cube.transform.parent = transform;
            cube.localPosition = cubeData.Position;
            cube.localRotation = cubeData.Rotation;
            cube.localScale = Vector3.one * cubeSize;
            innerCircleCubes.Add(cube);
            Renderer renderer = cube.GetComponent<Renderer>();
            var material = colorDiskRenderer.material;
            renderer.sharedMaterial = material;
        }

        int count = innerCircleCubes.Count;
        for (int i = count - 24; i < count; i++)
        {
            Transform cube = innerCircleCubes[i];
            Vector3 originalPos = cube.localPosition;
            Vector3 targetPos = originalPos + new Vector3(0, 1, 0);

            cube.DOLocalMove(targetPos, .3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    cube.DOLocalMove(originalPos, .2f)
                        .SetEase(Ease.InQuad);
                });
        }

        centerCylinder.DOScaleY(2.5f, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                centerCylinder.DOScaleY(1.53f, 0.3f)
                    .SetEase(Ease.InQuad);
            });
        colorDiskRenderer.sharedMaterial = materials[Random.Range(2, materials.Length)];
        DOVirtual.DelayedCall(.3f, () => { _bCanTakeDamage = true; });
    }


    public void TakeDamage(int damage)
    {
        if (!_bCanTakeDamage)
        {
            return;
        }

        TakeDamageEffect();
        int cubesToRemove = Mathf.Min(damage, outerCircleCubes.Count);

        for (int i = 0; i < cubesToRemove; i++)
        {
            Transform cube = outerCircleCubes[outerCircleCubes.Count - 1];
            cube.parent = null;

            Rigidbody rb = cube.GetComponent<Rigidbody>();
            Collider col = cube.GetComponent<Collider>();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 1.5f, Random.Range(-1f, 1f)) * 3f;
                rb.AddForce(randomForce, ForceMode.Impulse);
            }

            if (col != null)
            {
                col.enabled = true;
            }

            StartCoroutine(MoveCubeToTarget(cube, rb, col));

            outerCircleCubes.RemoveAt(outerCircleCubes.Count - 1);
            if (outerCircleCubes.Count == 0)
            {
                SpawnNewCircleAndReLocate();
            }
        }
    }


    IEnumerator MoveCubeToTarget(Transform cube, Rigidbody rb, Collider col)
    {
        yield return new WaitForSeconds(2f); 
        rb.isKinematic = true;
        col.enabled = false;
        float speed = 25f; 

        while (cube != null)
        {
            cube.position = Vector3.MoveTowards(cube.position, cubeTargetTransform.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(cube.position, cubeTargetTransform.position);
            float scaleFactor = Mathf.Clamp01(distance / 2f); 
            cube.localScale = Vector3.one * Mathf.Lerp(0.1f, cubeSize, scaleFactor);
            if (distance < 0.1f)
            {
                cube.position = cubeTargetTransform.position;
                break;
            }

            yield return null;
        }


        LeanPool.Despawn(cube);
        GameEventManager.Instance.CubeExpEarned(1);
    }

    public void Interact(Soldier soldier)
    {
        if (!_bIsAlive)
        {
            return;
        }

        transform.DOKill();

        _bIsAlive = false;
        transform.DOScale(1.25f, 0.2f) 
            .OnComplete(() =>
            {
                transform.DOScale(0f, 0.5f) 
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(3f, () =>
                        {
                            foreach (var cube in innerCircleCubes)
                            {
                                LeanPool.Despawn(cube);
                            }

                            foreach (var cube in outerCircleCubes)
                            {
                                LeanPool.Despawn(cube);
                            }
                            innerCircleCubes.Clear();
                            outerCircleCubes.Clear();
                            transform.gameObject.SetActive(false);
                        });
                    });
            });
       
    }
    public void TakeDamageEffect()
    {
        if (_bIsBouncing||!_bIsAlive)
        {
            return;
        }

        _bIsBouncing = true;
        transform.DOScale(1.2f, 0.1f) 
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(1f, 0.1f) 
                    .SetEase(Ease.InQuad).OnComplete(() => { _bIsBouncing = false; });
            });
    }
    private void GameRestart()
    {
        transform.gameObject.SetActive(true);
        transform.localScale=Vector3.one;
        SpawnAllCubes();
        _bIsAlive = true;

    }
}