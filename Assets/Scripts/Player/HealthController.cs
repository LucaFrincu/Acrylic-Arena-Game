using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#pragma strict

public class HealthController : MonoBehaviour
{
    public int health = 100;
    private int maxHealth = 100;
    public int zone = 0;
    public GameObject manager;
    public GameObject youDied;
    public Image healthBar;
    public AudioSource test;
    public AudioClip hurtAudio;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.fillAmount = health * 0.01f;
    }
    public void DamagePlayer(int damage)
    {
        test.PlayOneShot(hurtAudio);
        health -= damage;
        if(health <= 0)
        {
            youDied.SetActive(true);
            Invoke("DisableDied", 4f);
            
           

            manager.GetComponent<ManagerSpawner>().SpawnPlayer(zone);
            health = 100;
        }

        
    }
    public void DisableDied(){
        youDied.SetActive(false);
    }

    public void SetZone(int zoneCollided)
    {
        //Debug.Log("SETTING ZONE FOR PLAYER");
        zone = zoneCollided;
    }
}
