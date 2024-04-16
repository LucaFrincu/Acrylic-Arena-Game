using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ownReference;
    public GameObject enemyPrefab; // Assign in inspector
    public int spawnZone = 0;
    public int maxEnemies = 5; // Maximum number of enemies
    public float spawnRadius = 0f; // Radius within which enemies can spawn
    public bool partOfLevel = false;
    public bool restrictSpawn = false;
    private int currentEnemies = 0; // Current number of enemies
    private GameObject enemy;

    private void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        /*if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }*/
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position /*+ Random.insideUnitSphere * spawnRadius*/;
        if(spawnZone == 4)
        {
            spawnPosition.y = 0.51f;
        }
        else { spawnPosition.y = 0f; }
        //spawnPosition.y = 0f; // Assuming y is up and enemies spawn on the ground

        //Define the rotation: 90 degrees around the X-axis
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

        enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        enemy.GetComponent<Enemy_AI>().SetSpawner(ownReference);
        currentEnemies++;
    }

    // Call this method when an enemy is destroyed
    public void OnEnemyDestroyed()
    {
        currentEnemies--;
        //Debug.Log(currentEnemies);
        if(restrictSpawn == false)
            SpawnEnemy();
    }


    public void ResetEnemy(int zone)
    {
        if (spawnZone == zone)
        {
            Debug.Log("currentEnemies " + currentEnemies);
            if (currentEnemies >= 1)
            {
                Destroy(enemy);
                OnEnemyDestroyed();
            }

            //SpawnEnemy();
        }
    }

    public void SetRestriction(bool restriction)
    {
        restrictSpawn = restriction;
    }
}
