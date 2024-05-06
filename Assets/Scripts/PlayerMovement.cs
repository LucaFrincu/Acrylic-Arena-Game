using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject healthController;
    public float moveSpeed = 4f;
    public Rigidbody rb;
    public CombatController combat;
    Vector3 movement;
    public int zone = 0;
    public bool hasCollided = false;
   


    private void Start()
    {
        combat = GetComponent<CombatController>();
        
    }
    void Update()
    {
        hasCollided = false;
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 13f;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            moveSpeed = 8f;
            combat.checkmode = true;
            //playerMovement.moveSpeed = 2f;
        }
        else
        {
            moveSpeed = 10f;
            combat.checkmode = false;
        }
       
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // If colliding with an obstacle, prevent movement along that direction
            Vector3 direction = collision.contacts[0].point - transform.position;
            direction.Normalize();
            movement -= Vector3.Project(movement, direction);
        }
        if (collision.gameObject.CompareTag("enemyattack") && hasCollided == false)
        {
            Debug.Log("ENEMY HIT PLAYER");
            healthController.GetComponent<HealthController>().DamagePlayer(10);
            hasCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false;
        Debug.Log("COLLIDED SET TO FALSE FOR PLAYER");
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
    public int GetPlayerZone() {
        return zone;
    }
}