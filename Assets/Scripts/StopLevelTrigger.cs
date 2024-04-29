using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLevelTrigger : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision TRIGGER " + collision + " " + collision.gameObject);
        if (collision.gameObject.tag == "square" && collision.gameObject.name == "TemporaryCollider")
        {
            Debug.Log("ATTACKED");
            parent.GetComponent<SubLevelStop>().DamageWall();
        }
    }
}
