using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerLevels : MonoBehaviour
{
   public GameObject levelParent1;
   public GameObject levelParent2;
   public GameObject levelParent3;
   public GameObject levelParent4;
   public GameObject levelParent5;
   //public GameObject level1Sprite;
   public FlowerDraw level1;
   public bool level1Check = false;

   public FlowerDraw level2;
   public bool level2Check = false;

   public FlowerDraw level3;
   public bool level3Check = false;

   public FlowerDraw level4;
   public bool level4Check = false;

   public FlowerDraw level5;
   public bool level5Check = false;

   public Color levelColor1;
   public Color levelColor2;
   public Color levelColor3;
   public Color levelColor4;
   public Color levelColor5;

    void Start()
    {
        
    }

   
    void Update()
    {
        level1 = levelParent1.transform.Find("Lvl1spr2Tutorial1(Clone)/sprite2").gameObject.GetComponent<FlowerDraw>();
        if(level1.health == 0 && level1Check == false){
            level1Check = true;
            levelColor1 = level1.newColor;
        }

        level2 = levelParent2.transform.Find("Lvl1spr1Tutorial2(Clone)/sprite1").gameObject.GetComponent<FlowerDraw>();
        if(level2.health == 0 && level2Check == false){
            level2Check = true;
            levelColor2 = level2.newColor;
        }

        level3 = levelParent3.transform.Find("Lvl1spr1(Clone)/sprite1").gameObject.GetComponent<FlowerDraw>();
        if(level3.health == 0 && level3Check == false){
            level3Check = true;
            levelColor3 = level3.newColor;
        }

        level4 = levelParent4.transform.Find("Lvl1spr2(Clone)/sprite2").gameObject.GetComponent<FlowerDraw>();
        if(level4.health == 0 && level4Check == false){
            level4Check = true;
            levelColor4 = level4.newColor;
        }

        level5 = levelParent5.transform.Find("Lvl1spr3(Clone)/sprite3").gameObject.GetComponent<FlowerDraw>();
        if(level5.health == 0 && level5Check == false){
            level5Check = true;
            levelColor5 = level5.newColor;
        }
        
    }
}
