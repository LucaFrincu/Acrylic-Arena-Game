using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private int health = 100;
    private int maxHealth = 100;
    public int zone = 0;
    public GameObject manager;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1f;
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
        }

        if(health <= 66 && health >= 33)
        {
            healthBar.fillAmount = 0.66f;
        }
        else if (health < 33)
        {
            healthBar.fillAmount = 0.33f;
        }
        else
        {
            healthBar.fillAmount = 1f;
        }
    }

    public void SetZone(int zoneCollided)
    {
        Debug.Log("SETTING ZONE FOR PLAYER");
        zone = zoneCollided;
    }
}
