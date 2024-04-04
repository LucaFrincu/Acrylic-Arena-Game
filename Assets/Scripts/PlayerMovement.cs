using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Rigidbody rb;
    public CombatController combat;
    Vector3 movement;


    private void Start()
    {
        combat = GetComponent<CombatController>();
    }
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 8f;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            moveSpeed = 2f;
            combat.checkmode = true;
            //playerMovement.moveSpeed = 2f;
        }
        else
        {
            moveSpeed = 4f;
            combat.checkmode = false;
        }
        
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}