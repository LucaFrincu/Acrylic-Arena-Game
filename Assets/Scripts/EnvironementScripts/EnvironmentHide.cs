using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHide : MonoBehaviour
{
    public GameObject[] objectsToHide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
        {
            for(int i =0; i< objectsToHide.Length; i++)
            {
                objectsToHide[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < objectsToHide.Length; i++)
            {
                objectsToHide[i].gameObject.SetActive(true);
            }
        }
    }
}
