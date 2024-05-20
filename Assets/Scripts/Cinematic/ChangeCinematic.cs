using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeCinematic : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;
    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Played(PlayableDirector obj)
    {

    }

    private void Director_Stopped(PlayableDirector obj)
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
