using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Color myColor = Color.white;
    public Color attackColor = Color.white;
    public GameObject patternObject = null;
    private bool checkColor = false;
    private bool checkPattern = false;
    private bool hasCollided = false;
    public string combinedColorName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((myColor != Color.white && patternObject != null))
        {
            hasCollided = true;
            Debug.Log("PATTERN DISINTEGRATED");
            //if (other.gameObject.tag == "pattern")
            //{
            
            patternObject.gameObject.GetComponent<FlowerDraw>().DestroyFlower(combinedColorName, myColor);
          
            myColor = new Color(0f, 0f, 0f);
            patternObject = null;
            combinedColorName = "";
            //}
        }
    }

    public void SetEnemyColor(Color enemyColor, string colorName)
    {
        myColor = enemyColor;
        combinedColorName = colorName;
    }

    public void SetPattern(GameObject pattern)
    {
        patternObject = pattern;
    }

    /*public void OnTriggerEnter(Collider other)
    {
        *//*if(other.gameObject.tag == "pattern")
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
        }*//*
        Debug.Log("THE OBJECT AND COLOR BEFORE PAINTING myColor: " + myColor + " patternObject: " + patternObject);
        //Debug.Log("CHECK INTERACTION BTW PATTERN AND ENEMY");
        if(myColor != Color.white && patternObject != null)
        {
            hasCollided = true;
            Debug.Log("PATTERN DISINTEGRATED");
            //if (other.gameObject.tag == "pattern")
            //{
                patternObject.gameObject.GetComponent<FlowerDraw>().DestroyFlower(combinedColorName, myColor);
                myColor = new Color(0f, 0f, 0f);
                patternObject = null;
                combinedColorName = "";
            //}
        }
        *//*else
        {
            Debug.Log("INTERACTION NOT HAPPENED " + checkColor + " " + checkPattern);
            checkPattern = false;
            checkColor = false;
            patternObject = null;
            myColor = Color.white;
        }*//*
    }*/
    
}
