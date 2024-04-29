using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLevelStop : MonoBehaviour
{
    public int zone = 0;
    public GameObject trigger;
    public int healthWall = 10;
    private CombatController hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = GameObject.Find("Player").GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(healthWall <= 0)
        {
            gameObject.SetActive(false);
            trigger.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision " + collision + " " + collision.gameObject);
        if (collision.gameObject.tag =="square" && collision.gameObject.name == "TemporaryCollider")
        {
            Debug.Log("ATTACKED");
            healthWall -= hit.attackDmg;
        }
    }

    public void DamageWall()
    {
        healthWall -= hit.attackDmg;
    }
}
