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
    public bool finishPart = false;
    private bool restrictSpawn = false;
    public int currentEnemies = 0; // Current number of enemies
    private GameObject enemy;

    private void Start()
    {
        //SpawnEnemy();
        gameObject.SetActive(false);
    }

    void Update()
    {
        /*if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }*/
        if(restrictSpawn == false)
        {
            Debug.Log("Spawning Enemy");
            SpawnEnemy();
            restrictSpawn = true;
        }
    }

    public void SpawnEnemy()
    {
        Debug.Log("Enemy spawning in zone " + spawnZone);
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
        enemy.GetComponent<Enemy_AI>().SetSpawner(gameObject);
        currentEnemies++;
    }

    // Call this method when an enemy is destroyed
    public void OnEnemyDestroyed()
    {
        currentEnemies--;
        Debug.Log("ENEMY DESTROYED");
        SetRestriction(false);
        
    }


    public void ResetEnemy(int zone, bool checkpoint)
    {
        if (spawnZone == zone)
        {
            Debug.Log("currentEnemies " + currentEnemies);
            if (currentEnemies >= 1)
            {
                Destroy(enemy);
                OnEnemyDestroyed();
            }
            if(checkpoint)
                gameObject.SetActive(true);
            else
            {
                gameObject.SetActive(false);
            }
            //SpawnEnemy();
        }
    }

    public void SetRestriction(bool restriction)
    {
        Debug.Log("Setting restriction to " + restriction);
        restrictSpawn = restriction;
    }

    public bool GetRestriction()
    {
        return restrictSpawn;
    }

    public void SetFinishPart(bool completeness, int completeZone)
    {
        Debug.Log(spawnZone + " " + completeZone);
        if (spawnZone == completeZone)
        {
            
            gameObject.SetActive(completeness);
        }

    }
}
