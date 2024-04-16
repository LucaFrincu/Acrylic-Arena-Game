using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject Camera;
    public float xPos, yPos, zPos;
    public FlowerSpawner[] flowers;
    public EnemySpawner[] enemies;
    public int zone = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPatterns() == false)
        {
            for(int i = 0; i<= enemies.Length -1; i++)
            {
                Debug.Log(enemies[i]);
                enemies[i].SetRestriction(true);
            }
        }
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 0;
        if (GameObject.Find("Player")== false){
            GameObject.Instantiate(player, transform);
        }
        else
        {
            player = GameObject.Find("Player");
            Transform playerTransform = player.GetComponent<Transform>();
            playerTransform.position = new Vector3(xPos, yPos, zPos);
            ResetLevels(zone);

        }
    }

    public void ResetLevels(int zone)
    {
        Debug.Log("Beggining to Reset");
        for(int i=0; i <= zone; i++)
        {
            if(i >= zone)
            {
                Debug.Log("Resetting subLevel" + i);
                ResetPatterns(i);
                ResetEnemies(i);
            }
        }
    }

    private void ResetPatterns(int zone)
    {
        for (int i = 0; i <= flowers.Length - 1; i++)
        {
            Debug.Log("Pattern " + flowers[i]);
            flowers[i].ResetPattern(zone);
        }

    }
    private void ResetEnemies(int zone)
    {
        for (int i = 0; i <= enemies.Length - 1; i++)
        {
            Debug.Log("Enemies " + enemies[i]);
            enemies[i].ResetEnemy(zone);
            enemies[i].SetRestriction(false);
        }
    }

    private bool CheckPatterns()
    {
        int countersquares = 0;
        for(int i=0; i<= flowers.Length -1; i++)
        {
            if(flowers[i].CheckSquares() == false)
            {
                countersquares++;
            }
        }
        if (countersquares == flowers.Length)
            return false;
        else
        {
            return true;
        }
    }
}
