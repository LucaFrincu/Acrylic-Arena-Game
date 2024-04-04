using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDraw : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if this square has collided with a square spawned by an enemy
        Debug.Log("Collided with object" + other);
        if (other.gameObject.tag == "square")
        {
            Debug.Log("INTERACTED");
            // Notify the spawner that a square is being destroyed
            FindObjectOfType<FlowerSpawner>().OnSquareDestroyed();

            // Destroy this square
            Destroy(gameObject);
        }
    }
}
