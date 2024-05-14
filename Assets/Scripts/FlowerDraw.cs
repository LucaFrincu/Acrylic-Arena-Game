using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDraw : MonoBehaviour
{
    public bool hasCollided = false;
    private GameObject parentSpawner;
    private short checkCounter = 0;
    public int zone = 0;
    public int health = 1;
    public int maxHealth = 0;
    public int dmgDealt = 1;
    private void Update()
    {
        if(health <= 0)
        {
            if (checkCounter == 0)
            {
                checkCounter++;
                hasCollided = false;
                Debug.Log("INTERACTED WITH ATTACK");
                // Notify the spawner that a square is being destroyed
                health = maxHealth;
                checkCounter = 0;
                // Destroy this square
                gameObject.SetActive(false);
                //gameObject.SetActive(true);
                parentSpawner.GetComponent<FlowerSpawner>().OnSquareDestroyed();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if this square has collided with a square spawned by an enemy
        Debug.Log("Collided with object" + other);
        if (other.gameObject.tag == "square" && other.gameObject.name == "TemporaryCollider" && hasCollided == false)
        {
            /*hasCollided = true;
            Debug.Log("INTERACTED WITH ATTACK");
            // Notify the spawner that a square is being destroyed

            // Destroy this square
            gameObject.SetActive(false);
            //gameObject.SetActive(true);

            parentSpawner.GetComponent<FlowerSpawner>().OnSquareDestroyed();*/
            other.gameObject.GetComponent<PlayerAttack>().SetPattern(gameObject);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        hasCollided = false;
    }*/

    public void DestroyFlower()
    {
        
        if (health >= 0)
        {
            health -= dmgDealt;
        }
    }

    public void SetSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
    }
}
