using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSubLevel : MonoBehaviour
{
    public int zone = 0;
    //public GameObject ownReference;
    private GameObject stopSubLevel;
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Collision on zone " + zone);
            gameObject.SetActive(false);
            spawner.GetComponent<PlayerSpawn>().DeploySpawner(zone);

        }
    }
}
