using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboGuide : MonoBehaviour
{
    public GameObject comboGuideUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<CombatController>().CheckMode() == true)
        {
            comboGuideUI.gameObject.SetActive(true);
            //Debug.Log("TRUE");
        }
        else
        {
            comboGuideUI.gameObject.SetActive(false);
            //Debug.Log("FALSE");
        }
    }
}
