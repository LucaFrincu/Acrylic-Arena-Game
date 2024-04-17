using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBlocker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Prevent the player from passing through the collider
            // You may want to add additional logic here such as stopping the player's movement
            //Debug.Log("Player blocked by collider!");
        }
    }
}
