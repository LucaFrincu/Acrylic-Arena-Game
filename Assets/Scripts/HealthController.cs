using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int health = 100;
    private int maxHealth = 100;
    public int zone = 0;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            manager.GetComponent<ManagerSpawner>().SpawnPlayer(zone);
            health = maxHealth;
        }
    }

    public void SetZone(int zoneCollided)
    {
        Debug.Log("SETTING ZONE FOR PLAYER");
        zone = zoneCollided;
    }
}
