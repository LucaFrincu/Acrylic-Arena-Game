using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentScene;
    public bool isLevelCompleted;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene Index set to " + currentScene);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isLevelCompleted == true)
            {
                SceneManager.LoadScene(currentScene + 1);
            }
             
        }
    }
}
