using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredSquare : MonoBehaviour
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
        if (other.gameObject.tag == "squareenemy")
        {
            //Debug.Log("Change Color");
        }
    }
}
