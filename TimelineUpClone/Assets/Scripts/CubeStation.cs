using System;
using System.Collections;
using System.Collections.Generic;
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
    [Serializable]
    public class CircleCubeDatas
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    void Start()
    {
        // GenerateCylinder();
        SpawnAllCubes();
        
    }

    void SpawnAllCubes()
    {
        foreach (var cubeData in outerCircleData)
        {
            GameObject cube = Instantiate(cubePrefab, transform);
            cube.transform.localPosition = cubeData.Position;
            cube.transform.localRotation = cubeData.Rotation;
            cube.transform.localScale = Vector3.one * cubeSize;
            outerCircleCubes.Add(cube.transform);
            Renderer renderer = cube.GetComponent<Renderer>();
            renderer.material = materials[0];
        }

        foreach (var cubeData in innerCircleData)
        {
            GameObject cube = Instantiate(cubePrefab, transform);
            cube.transform.localPosition = cubeData.Position;
            cube.transform.localRotation = cubeData.Rotation;
            cube.transform.localScale = Vector3.one * cubeSize;
            innerCircleCubes.Add(cube.transform);
            Renderer renderer = cube.GetComponent<Renderer>();
            var material=  colorDiskRenderer.material;
            renderer.material = material;
        }
        colorDiskRenderer.material = materials[Random.Range(2, materials.Length)];
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

    public void SpawnNewCircleAndReLocate()
    {
        foreach (var cube in innerCircleCubes)
        {
            outerCircleCubes.Add(cube.transform);
        }
        innerCircleCubes.Clear();
        for (int i = 0; i < outerCircleCubes.Count; i++)
        {
            outerCircleCubes[i].localPosition = outerCircleData[i].Position;
            outerCircleCubes[i].localRotation = outerCircleData[i].Rotation;
        }

        foreach (var cubeData in innerCircleData)
        {
            GameObject cube = Instantiate(cubePrefab, transform);
            cube.transform.localPosition = cubeData.Position;
            cube.transform.localRotation = cubeData.Rotation;
            cube.transform.localScale = Vector3.one * cubeSize;
            innerCircleCubes.Add(cube.transform);
            Renderer renderer = cube.GetComponent<Renderer>();
            var material=  colorDiskRenderer.material;
            renderer.material = material;
        }
        colorDiskRenderer.material = materials[Random.Range(2, materials.Length)];

    }

 
    public void TakeDamage(int damage)
    { // Eğer elimizde yeterince küp yoksa, sadece olanları sil
        int cubesToRemove = Mathf.Min(damage, outerCircleCubes.Count);

        for (int i = 0; i < cubesToRemove; i++)
        {
            // İlk küpü listeden al
            Transform cube = outerCircleCubes[0];

            // Sahnedeki küpü yok et
            Destroy(cube.gameObject);

            // Listeden çıkar
            outerCircleCubes.RemoveAt(0);
        }
    }

    public void Interact(Soldier soldier)
    {
        //throw new NotImplementedException();
    }
}