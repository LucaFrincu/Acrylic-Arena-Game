using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    private int direction = 4;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            anim.Play("WalkRight");
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.Play("WalkLeft");
            direction = 3;

        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.Play("WalkBackward");
            direction = 2;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.Play("WalkForward");
            direction = 4;
        }
        else
        {

            switch (direction)
            {
                case 1:
                    {
                        anim.Play("RightPlayer");
                        break;
                    }
                case 2:
                    { 
                        anim.Play("BackPlayer");
                        
                        break;
                    }
                case 3:
                    {
                        anim.Play("LeftPlayer");
                        break;
                    }
                case 4:
                    {
                       anim.Play("IdlePlayer");
                        break;
                    }
            }
        }
    }
}
