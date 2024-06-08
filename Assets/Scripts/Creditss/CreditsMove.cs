using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMove : MonoBehaviour
{
    public float moveScreen = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += transform.up * moveScreen * Time.deltaTime;
    }
}
