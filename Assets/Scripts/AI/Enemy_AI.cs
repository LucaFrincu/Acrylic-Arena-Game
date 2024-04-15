using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stoppingDistance = 1f;
    public float detectDistance = 6f;
    public string playerTag = "Player";
    public bool hasCollided = false;
    public int health = 100;
    public float staggerTime = 1.3f; // Time in seconds

    private Transform playerTransform; // Reference to the player's transform
    public CombatController hit;
    public GameObject player;
    private Vector3 targetPosition; // Position the enemy is moving towards
    private bool playerSeen = false;
    private bool isStaggered = false; 

    public float squareSpawnOffset = 2f; // Offset for spawning the square

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
        hit = player.GetComponent<CombatController>();
        if (hit == null)
        {
            Debug.Log("hit null detected!");
        }
    }

    void Update()
    {
        if (playerTransform != null && !isStaggered) // Check if not staggered
        {
            // Calculate direction to the player
            Vector3 direction = (targetPosition - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            // Check if the player is within detect distance
            if (Vector3.Distance(transform.position, playerTransform.position) <= detectDistance)
            {
                targetPosition = playerTransform.position;
                playerSeen = true;

                //Debug.Log("Saw you!");
            }

            if (playerSeen && distanceToTarget > stoppingDistance)
            {
                // Move enemy towards the target position
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the enemy or handle accordingly
            FindObjectOfType<EnemySpawner>().OnEnemyDestroyed(); // Update the spawner's enemy count
        }

        // Check if the enemy is staggered
        if (isStaggered)
        {
            staggerTime -= Time.deltaTime;

            // Check if the stun si over
            if (staggerTime <= 0)
            {
                // Reset state
                isStaggered = false;
                staggerTime = 1.5f; // Reset stagger time for next use
            }
            else
            {
                // Stop enemy movement while staggered
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TemporaryCollider" && !isStaggered) 
        {
            hasCollided = true;
            health -= hit.attackDmg;
            SpawnColoredSquare(other.transform.position);
            /*playerSeen = false;
              stunTime -= false;*/
            isStaggered = true;
        }
    }

    void SpawnColoredSquare(Vector3 collisionPosition)
    {
        // Calculate spawn position for the square
        Vector3 direction = (collisionPosition - transform.position).normalized;
        Vector3 spawnPosition = transform.position - direction * squareSpawnOffset;

        // Create the square
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.tag = "square";
        square.transform.position = spawnPosition;
        square.transform.localScale = new Vector3(1f, 0.1f, 1f); // Make it a flat square
        Renderer renderer = square.GetComponent<Renderer>();
        renderer.material.color = new Color(1f, 0f, 1f); // Set color to magenta

        Destroy(square.GetComponent<Collider>());
        BoxCollider collider = square.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        // Optional: Destroy the square after some time
        Destroy(square, 5f);
    }
}
