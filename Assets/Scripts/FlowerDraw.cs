using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDraw : MonoBehaviour
{
    private bool hasCollided = false;
    private GameObject parentSpawner;
    private void OnTriggerEnter(Collider other)
    {
        // Check if this square has collided with a square spawned by an enemy
        //Debug.Log("Collided with object" + other);
        if (other.gameObject.tag == "squareenemy" && hasCollided == false)
        {
            hasCollided = true;
            //Debug.Log("INTERACTED");
            // Notify the spawner that a square is being destroyed

            // Destroy this square
            Destroy(gameObject);


            parentSpawner.GetComponent<FlowerSpawner>().OnSquareDestroyed();
        }
    }

    public void SetSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
    }
}
