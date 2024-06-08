using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioUI : MonoBehaviour
{
    public bool portfolioActive = false;
    public int levels = 0;
    public GameObject portfolio;
    public CheckerLevels managerLevels;
    //public FlowerDraw level1Spr1;
    public Image item1;
    public GameObject unlock1;

    public Image item2;
    public GameObject unlock2;

    public Image item3;
    public GameObject unlock3;

    public Image item4;

   public Image item5;

   Animator anim;


    void Start()
    {
        anim = portfolio.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(portfolioActive)
            {
              DectivatePortfolio();
             }
            else
            {
                ActivatePortfolio();
                anim.Play("PortfolioSize");
            }
        }


        if(managerLevels.level1Check)
        {
            managerLevels.level1Check = false;
            item1.gameObject.SetActive(true);
            item1.color = managerLevels.levelColor1;
            unlock1.SetActive(false);

            ActivatePortfolio();
            anim.Play("PortfolioSize");
            levels++;

        }
        if(managerLevels.level2Check)
        {
            managerLevels.level2Check = false;
           item2.gameObject.SetActive(true);
           item2.color = managerLevels.levelColor2;
            unlock2.SetActive(false);
            ActivatePortfolio();
            anim.Play("PortfolioSize");
            levels++;
        }
         if(managerLevels.level3Check)
        {
            managerLevels.level3Check = false;
            item3.gameObject.SetActive(true);
            item3.color = managerLevels.levelColor3;
            unlock3.SetActive(false);
            ActivatePortfolio();
            anim.Play("PortfolioSize");
            levels++;
        }
         if(managerLevels.level4Check)
        {
            managerLevels.level4Check = false;
            item4.gameObject.SetActive(true);
            item4.color = managerLevels.levelColor4;
            ActivatePortfolio();
            anim.Play("PortfolioSize");
            levels++;
           
        }
        if(managerLevels.level5Check)
        {
            managerLevels.level5Check = false;
            item5.gameObject.SetActive(true);
            item5.color = managerLevels.levelColor5;
            ActivatePortfolio();
            anim.Play("PortfolioSize");
            levels++;
           
        }

    }

    void ActivatePortfolio()
    {
            
            portfolioActive = true;
            //Time.timeScale = 0f;
            portfolio.SetActive(true);
           

    }
      void DectivatePortfolio(){
       
            portfolioActive = false;
            //Time.timeScale = 1f;
            portfolio.SetActive(false);
        

    }
}
