using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            anim.Play("RightPlayer");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.Play("LeftPlayer");

        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.Play("BackPlayer");

        }
        else
        {
            anim.Play("IdlePlayer");
        }
    }
}
