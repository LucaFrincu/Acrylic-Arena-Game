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
    public int children = 0;
    public int numberChildren = 0;
    // Start is called before the first frame update
    void Start()
    {
        children = flowers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        int playerZone = player.GetComponent<PlayerMovement>().GetPlayerZone();
        //for(int i =0)
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
            //ResetLevels(zone, true);

        }
    }

    public void ResetLevels(int zone, bool checkpoint)
    {
        Debug.Log("Beggining to Reset");
        for(int i=0; i <= zone; i++)
        {
            if (i == zone)
            {
                Debug.Log("Resetting subLevel" + i);
                
                ResetPatterns(i, checkpoint);
                ResetEnemies(i, checkpoint);
            }
            
        }
    }

    private void ResetPatterns(int zone, bool checkpoint)
    {
        numberChildren = 0;
        children = flowers.Length;
        if (flowers.Length != 0)
        {
            if (flowers[0].CheckFamily() == false)
            {
                for (int i = 0; i <= flowers.Length - 1; i++)
                {
                    Debug.Log("Pattern " + flowers[i]);

                    flowers[i].ResetPattern(zone, checkpoint);
                    flowers[i].currentSquares = 0;
                }
            }
            else if(checkpoint == true)
            {
                flowers[numberChildren].currentSquares = 0;
                flowers[numberChildren++].ResetPattern(zone, true);
                if (numberChildren < flowers.Length)
                    for (int i = numberChildren; i <= flowers.Length - 1; i++)
                    {
                        flowers[i].ResetPattern(zone, false);
                        flowers[i].currentSquares = 0;
                    }
            }
            else
            {
                for (int i = 0; i <= flowers.Length - 1; i++)
                {
                    flowers[i].currentSquares = 0;
                    flowers[i].ResetPattern(zone, false);
                }
            }
        }
        else
        {
            //WE ARE SPAWNING ONLY ONE ENEMY AND THEN DISABLING IT        
        }
    }
    private void ResetEnemies(int zone, bool checkpoint)
    {
        for (int i = 0; i <= enemies.Length - 1; i++)
        {
            Debug.Log("Enemies " + enemies[i]);
            enemies[i].ResetEnemy(zone, checkpoint);
            enemies[i].SetRestriction(false);
            if(flowers.Length == 0 && checkpoint == true)
            {
                enemies[i].gameObject.SetActive(false);
                enemies[i].SpawnEnemy();
            }
        }
    }

 

    public void DeploySpawner(int zoneCollider)
    {
        /*for (int i = 0; i <= flowers.Length - 1; i++)
        {
            flowers[i].Res
        }*/
        Debug.Log("Deploying Spawner from zone " + zone);
        if (zoneCollider == zone)
        {
            player.GetComponent<HealthController>().SetZone(zoneCollider);
            for (int i = 0; i <= enemies.Length - 1; i++)
            {
                enemies[i].gameObject.SetActive(true);
            }
            if(flowers.Length == 0)
            {
                //Debug.Log("ENEMY IS WITHOUT FLOWERS!");
                for (int i = 0; i <= enemies.Length - 1; i++)
                {
                    enemies[i].SpawnEnemy();
                    enemies[i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (flowers[0].CheckFamily() == true)
                {
                    flowers[numberChildren++].ResetPattern(zone, true);
                    //TRY 1
                    if (numberChildren < flowers.Length)
                    {
                        Debug.Log(numberChildren + " " + flowers.Length);
                        for (int i = numberChildren; i <= flowers.Length -1; i++)
                        {
                            flowers[i].ResetPattern(zone, false);
                        }
                    }
                    /////////
                }
                else
                {
                    for (int i = 0; i <= flowers.Length - 1; i++)
                    {
                        flowers[i].ResetPattern(zone, true);
                    }
                    
                }
                
            }
        }
    }

    public void CompletePart(int zoneComplete) {
        Debug.Log("Disable enemy spawn activity");
        if(zone == zoneComplete)
            for (int i = 0; i <= enemies.Length - 1; i++)
            {
                Debug.Log("THE ZONE " + zoneComplete);
                enemies[i].SetFinishPart(false, zoneComplete);
            }
    }


    public void KillFamily()
    {
        children--;
        if(children == 0)
        {
            CompletePart(zone);
        }
        else
        {
            flowers[numberChildren++].ResetPattern(zone, true);
        }
    }

}
