using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Color myColor = Color.white;
    private GameObject patternObject = null;
    private bool checkColor = false;
    private bool checkPattern = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyColor(Color enemyColor)
    {
        myColor = enemyColor;
    }

    public void SetPattern(GameObject pattern)
    {
        patternObject = pattern;
    }

    public void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.tag == "pattern")
        {
            Debug.Log("PATTERN INTERACTED");
            //if(myColor != Color.white)
            //{
                checkPattern = true;
            //}
        }*/
        /*if(other.gameObject.tag == "enemy")
        {
            Debug.Log("ENEMY INTERACTED");
            if(patternObject != null)
            {
                other.gameObject.GetComponent<Enemy_AI>().pattern = patternObject;
            }
        }*/

        //Debug.Log("CHECK INTERACTION BTW PATTERN AND ENEMY");
        if(myColor != Color.white && patternObject != null)
        {
            Debug.Log("PATTERN DISINTEGRATED");
            //if (other.gameObject.tag == "pattern")
            //{
                patternObject.gameObject.GetComponent<FlowerDraw>().DestroyFlower();
            //}
        }
        /*else
        {
            Debug.Log("INTERACTION NOT HAPPENED " + checkColor + " " + checkPattern);
            checkPattern = false;
            checkColor = false;
            patternObject = null;
            myColor = Color.white;
        }*/
    }
}