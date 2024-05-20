using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    public GameObject[] subLevels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(int zone)
    {
        for(int i = 0; i <= subLevels.Length - 1; i++)
        {
            PlayerSpawn subLevel = subLevels[i].GetComponent<PlayerSpawn>();
            if (zone == i)
            {
                subLevel.ResetLevels(i, true);
                subLevel.SpawnPlayer();
            }
            else
            {
                subLevels[i].GetComponent<PlayerSpawn>().ResetLevels(i, false);
            }
        }

    }
}
