using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentScene;
    public bool isLevelCompleted;
    public PortfolioUI levelsCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene Index set to " + currentScene);
        isLevelCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelsCheck.levels == 5){
            isLevelCompleted = true;
        }
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
