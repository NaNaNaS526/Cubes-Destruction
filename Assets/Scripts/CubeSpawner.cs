using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public Action OnDestroyCube;
    [SerializeField] private Cube cubePrefab;
    [SerializeField] [Range(0, 10)] private float cubeSpawnTimerMin;
    [SerializeField] [Range(0, 10)] private float cubeSpawnTimerMax;

    private void Start()
    {
        OnDestroyCube += StartSpawnTimer;
    }

    private void StartSpawnTimer()
    {
        StartCoroutine(SpawnCubeTimer());
    }

    private IEnumerator SpawnCubeTimer()
    {
        yield return new WaitForSeconds(Random.Range(cubeSpawnTimerMin, cubeSpawnTimerMax));
        SpawnCube();
    }

    private void SpawnCube()
    {
        Cube newCube = Instantiate(cubePrefab, new Vector3(0.0f, cubePrefab.transform.position.y, 0.0f),
            Quaternion.identity);
        newCube.transform.SetParent(gameObject.transform);
    }
}