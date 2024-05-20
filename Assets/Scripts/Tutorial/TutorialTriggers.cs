using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public int zone = 1;
    private GameObject current;
    private GameObject last;
    //[SerializedField] private GameObject zones;
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
            string lastArea = "Zone" + zone.ToString();
            zone++;
            string currentArea = "Zone" + zone.ToString();
            Debug.Log("LastArea" + lastArea);
            current = GameObject.Find("TutorialInfo/Zones/" + currentArea);
            last = GameObject.Find("TutorialInfo/Zones/" + lastArea);

            current.SetActive(true);
            last.SetActive(false);

            gameObject.SetActive(false);

        }
    }
}

