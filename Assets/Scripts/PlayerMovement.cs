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
    Vector3 lastSafePosition;
    public int zone = 0;
    public bool hasCollided = false;
    public bool obstacleAhead = false;
    public float raycastDistance = 30f;
    public LayerMask collisionLayer;

    public float rayLength = 0.1f;
    public Color rayColor = Color.red;


    private void Start()
    {
        combat = GetComponent<CombatController>();
        lastSafePosition = transform.position;
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        //hasCollided = false;
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 20f;
        }
        else if (Input.GetMouseButton(1))
        {
            moveSpeed = 8f;
            combat.checkmode = true;
            combat.finishCombo = true;
            //playerMovement.moveSpeed = 2f;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            moveSpeed = 15f;
            //combat.checkmode = false;
            combat.finishCombo = false;
        }
        else
        {
            moveSpeed = 15f;
        }
       
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = rb.velocity.normalized;
        // Create a ray starting from the object's position and extending in the direction of movement
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement, out hit, rayLength, collisionLayer))
        {
            if (hit.collider.CompareTag("obstacle"))
            {
                obstacleAhead = true;
                // Stop movement if obstacle is detected
                movement = Vector3.zero;
            }
        }
        else
        {
            obstacleAhead = false;
            // Move the object if no obstacle detected
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        // Visualize the ray
        Debug.DrawRay(transform.position, movement * rayLength, rayColor);
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        
        //Debug.Log(collision);
        if (collision.gameObject.tag == "obstacle")
        {
            
            //obstacleAhead = true;

        }
        if (collision.gameObject.tag == "enemyattack" && hasCollided == false)
        {
            hasCollided = true;
            Debug.Log("ENEMY HIT PLAYER");
            healthController.GetComponent<HealthController>().DamagePlayer(10);
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            //Debug.Log("");
            

        }
        hasCollided = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            //transform.position = lastSafePosition;
            obstacleAhead = false;
        }
        hasCollided = false;
        //Debug.Log("COLLIDED SET TO FALSE FOR PLAYER");
    }



    public int GetPlayerZone() {
        return zone;
    }
}