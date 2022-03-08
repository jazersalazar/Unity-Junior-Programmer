using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject[] animalPrefabs;
    
    private float spawnRangeX = 10.0f;
    private float spanwPosZ = 20.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    void Start() {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void SpawnRandomAnimal() {
        // Random index of animal
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        // Random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
        0, spanwPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos,
        animalPrefabs[animalIndex].transform.rotation);
    }
}
