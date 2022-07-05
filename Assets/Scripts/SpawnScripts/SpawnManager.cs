using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    SpawnPoints[] spawnPoints;

    private void Awake()
    {
        Instance = this; 
        spawnPoints = GetComponentsInChildren<SpawnPoints>();
    }

    public Transform GetSpawnerpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
}
