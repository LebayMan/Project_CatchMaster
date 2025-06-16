using UnityEngine;
using System.Collections.Generic;

public class BallSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> goodBallPrefabs;
    public List<GameObject> badBallPrefabs;

    public float spawnInterval = 1.5f;
    [Range(0f, 1f)] public float badBallChance = 0.3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval);
    }

    void SpawnBall()
    {
        if (spawnPoints.Count == 0) return;

        // Choose random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Choose which list to use
        GameObject prefabToSpawn;
        if (Random.value < badBallChance && badBallPrefabs.Count > 0)
        {
            prefabToSpawn = badBallPrefabs[Random.Range(0, badBallPrefabs.Count)];
        }
        else if (goodBallPrefabs.Count > 0)
        {
            prefabToSpawn = goodBallPrefabs[Random.Range(0, goodBallPrefabs.Count)];
        }
        else
        {
            return; // No prefabs available
        }
        Debug.Log("Spawning: " + prefabToSpawn.name + " at " + spawnPoint.position);
        // Instantiate
        Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
