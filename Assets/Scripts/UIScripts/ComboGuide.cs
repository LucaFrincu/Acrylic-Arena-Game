using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboGuide : MonoBehaviour
{
    public GameObject comboGuideUI;
    public RectTransform parentGuide;
    public GameObject blueAttack;
    public GameObject redAttack;
    public GameObject yellowAttack;
    public CombatController combat;
    // Start is called before the first frame update
    void Start()
    {
        combat = gameObject.GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        //comboGuideUI.transform.position = combat.lastMousePosition;
        
        if(combat.CheckMode() == true)
        {
            if(combat.manaBlue >= 30 && combat.manaRed >= 30 && combat.manaYellow >= 30)
            {
                comboGuideUI.gameObject.SetActive(true);
            }
            if(combat.manaBlue >= 30)
            {
                blueAttack.gameObject.SetActive(true);
            }
            if (combat.manaRed >= 30)
            {
                redAttack.gameObject.SetActive(true);
            }
            if (combat.manaYellow >= 30)
            {
                //Debug.Log("YELLOW ENOUGH!");
                yellowAttack.gameObject.SetActive(true);
            }
            if(combat.manaBlue < 30 && combat.manaRed < 30 && combat.manaYellow < 30)
            {
                blueAttack.gameObject.SetActive(false);
                redAttack.gameObject.SetActive(false);
                yellowAttack.gameObject.SetActive(false);
            }

            //Debug.Log("TRUE");
        }
        else
        {
            parentGuide.transform.position = Input.mousePosition + new Vector3(50f, 40f, 0f);
            comboGuideUI.gameObject.SetActive(false);
            blueAttack.gameObject.SetActive(false);
            redAttack.gameObject.SetActive(false);
            yellowAttack.gameObject.SetActive(false);
            //Debug.Log("FALSE");
        }
    }
}
