using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    // Start is called before the first frame update

    /*Animator anim;
    private int direction = 4;
    private bool front = true;
    private bool left = true;
    private float delayTime = 7.0f;

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
            anim.speed = 1;
            direction = 1;
            left = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.Play("WalkLeft");
            anim.speed = 1;
            direction = 3;
            left = true;

        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.Play("WalkBackward");
            anim.speed = 1;
            direction = 2;
            front = false;

        }
        else if (Input.GetKey(KeyCode.S))
        {

            anim.Play("WalkForward");
            anim.speed = 1;
            direction = 4;
            front = true;

        }
        else
        {

            switch (direction)
            {
                case 1:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            anim.speed = 3;
                            if (front == false)
                            {
                                anim.Play("Back_right_attack");
                            }
                            else
                            {
                                anim.Play("Front_Right_attack");
                            }
                        }
                        else
                        {
                            anim.speed = 1;
                            anim.Play("RightPlayer");
                        }
                        break;
                    }
                case 2:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            anim.speed = 3;
                            if (left == true)
                            {
                                anim.Play("Front_left_attack");
                            }
                            else
                            {
                                anim.Play("Front_Right_attack");
                            }
                        }
                        else
                        {
                            anim.speed = 1;
                            anim.Play("BackPlayer");
                        }
                        break;
                    }
                case 3:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            anim.speed = 3;
                            if (front == false)
                            {
                                anim.Play("Back_left_attack");
                            }
                            else
                            {
                                anim.Play("Front_left_attack");
                            }
                        }
                        else
                        {
                            anim.speed = 1;
                            anim.Play("LeftPlayer");
                        }
                        break;
                    }
                case 4:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            anim.speed = 3;
                            if (left == true)
                            {
                                anim.Play("Front_left_attack");
                            }
                            else
                            {
                                anim.Play("Front_left_attack");
                            }
                        }
                        else
                        {
                            anim.speed = 1;
                            anim.Play("IdlePlayer");
                        }
                        break;
                    }
            }
        }
        
    }*/

    /*Animator anim;
    private int direction = 4;
    private bool front = true;
    private bool left = true;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Handle attack animation input
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            HandleAttack();
            return;
        }

        // If currently attacking, do not process movement
        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f)
            {
                isAttacking = false;
            }
            else
            {
                return;
            }
        }

        // Handle movement input
        if (Input.GetKey(KeyCode.D))
        {
            anim.Play("WalkRight");
            anim.speed = 1;
            direction = 1;
            left = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.Play("WalkLeft");
            anim.speed = 1;
            direction = 3;
            left = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.Play("WalkBackward");
            anim.speed = 1;
            direction = 2;
            front = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.Play("WalkForward");
            anim.speed = 1;
            direction = 4;
            front = true;
        }
        else
        {
            HandleIdle();
        }
    }

    void HandleAttack()
    {
        isAttacking = true;
        anim.speed = 3;

        switch (direction)
        {
            case 1:
                anim.Play(front ? "Front_Right_attack" : "Back_right_attack");
                break;
            case 2:
                anim.Play(left ? "Front_left_attack" : "Front_Right_attack");
                break;
            case 3:
                anim.Play(front ? "Front_left_attack" : "Back_left_attack");
                break;
            case 4:
                anim.Play(front ? "Front_attack" : "Back_attack");
                break;
        }
    }

    void HandleIdle()
    {
        anim.speed = 1;

        switch (direction)
        {
            case 1:
                anim.Play("RightPlayer");
                break;
            case 2:
                anim.Play("BackPlayer");
                break;
            case 3:
                anim.Play("LeftPlayer");
                break;
            case 4:
                anim.Play("FrontPlayer");
                break;
        }
    }*/

    Animator anim;
    private int direction = 4;
    private bool front = true;
    private bool left = true;
    private bool isAttacking = false;
    private bool canClick = true;
    private float delayTime = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if attack animation is still playing
        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f && !anim.IsInTransition(0))
            {
                isAttacking = false;
            }
            else
            {
                return;
            }
        }

        // Handle attack input
        if (Input.GetMouseButtonDown(0) && !isAttacking && canClick)
        {
            StartCoroutine(ClickCooldown());
            HandleAttack();
            return;
        }
        
        if (Input.GetMouseButtonUp(1) && !isAttacking)
        {
            //StartCoroutine(ClickCooldown());
            HandleAttack();
            return;
        }

        // Handle movement input
        if (Input.GetKey(KeyCode.D))
        {
            anim.Play("WalkRight");
            anim.speed = 1;
            direction = 1;
            left = false;
            //front = true; // Assuming facing front when walking right
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.Play("WalkLeft");
            anim.speed = 1;
            direction = 3;
            left = true;
            //front = true; // Assuming facing front when walking left
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.Play("WalkBackward");
            anim.speed = 1;
            direction = 2;
            front = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.Play("WalkForward");
            anim.speed = 1;
            direction = 4;
            front = true;
        }
        else
        {
            //Debug.Log("IDLING");
            HandleIdle();
        }
    }

    void HandleAttack()
    {
        isAttacking = true;
        anim.speed = 3; // Set the speed to 1 to respect the animation speed

        switch (direction)
        {
            case 1:
                anim.Play(front ? "Front_Right_attack" : "Back_right_attack");
                break;
            case 2:
                anim.Play(left ? "Front_left_attack" : "Back_right_attack");
                break;
            case 3:
                anim.Play(front ? "Front_left_attack" : "Back_left_attack");
                break;
            case 4:
                anim.Play(left ? "Front_left_attack" : "Back_right_attack");
                break;
        }
    }

    void HandleIdle()
    {
        anim.speed = 1; // Set the speed to 1 to respect the animation speed

        switch (direction)
        {
            case 1:
                anim.Play("RightPlayer");
                break;
            case 2:
                anim.Play("BackPlayer");
                break;
            case 3:
                anim.Play("LeftPlayer");
                break;
            case 4:
                anim.Play("IdlePlayer");
                break;
        }
    }

    IEnumerator ClickCooldown()
    {
        // Set clicking flag to false to prevent further clicks
        canClick = false;

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Set clicking flag to true to allow clicks again
        canClick = true;
    }
}
