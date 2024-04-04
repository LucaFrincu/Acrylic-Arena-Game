using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in inspector
    public int maxEnemies = 5; // Maximum number of enemies
    public float spawnRadius = 10f; // Radius within which enemies can spawn
    private int currentEnemies = 0; // Current number of enemies

    void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0; // Assuming y is up and enemies spawn on the ground

        //Define the rotation: 90 degrees around the X-axis
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        //enemy.AddComponent<Enemy_AI>(); // Assuming an Enemy script handles the enemy logic
        currentEnemies++;
    }

    // Call this method when an enemy is destroyed
    public void OnEnemyDestroyed()
    {
        currentEnemies--;
        //Debug.Log(currentEnemies);
    }
}
