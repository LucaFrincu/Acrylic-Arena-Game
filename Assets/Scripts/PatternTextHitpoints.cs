using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternTextHitpoints : MonoBehaviour
{
    public GameObject sprite;
    public FlowerDraw flowerHealth;
    private int previousHealth = 1000;
    // Start is called before the first frame update
    void Start()
    {
        flowerHealth = sprite.GetComponent<FlowerDraw>();
    }

    // Update is called once per frame
    void Update()
    {
        if(previousHealth >= flowerHealth.health)
        {
            if (previousHealth != flowerHealth.health)
            {
                previousHealth = flowerHealth.health;
            }
            TextMeshPro text = gameObject.GetComponent<TextMeshPro>();
            text.text = flowerHealth.health.ToString() + "/" + flowerHealth.maxHealth.ToString();
        }
        else
        {
            TextMeshPro text = gameObject.GetComponent<TextMeshPro>();
            text.text ="0/" + flowerHealth.maxHealth.ToString();
        }
        
        
    }
}
