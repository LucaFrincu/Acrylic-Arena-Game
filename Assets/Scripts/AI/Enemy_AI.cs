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
    public int health = 0;
    public float stunTime = 30f;

    private Transform playerTransform; // Reference to the player's transform
    public CombatController hit;
    public GameObject player;
    private Vector3 targetPosition; // Position the enemy is moving towards
    private bool playerSeen = false;

    public float squareSpawnOffset = 2f; // Offset for spawning the square

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
        hit = player.GetComponent<CombatController>();
        if (hit == null) {
            Debug.Log("hit null detected!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "TemporaryCollider" /*&& hasCollided == false*/)
        {

            hasCollided = true;
            health -= hit.attackDmg;
            SpawnColoredSquare(other.transform.position);
            /*playerSeen = false;
            stunTime -= Time.deltaTime;*/
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

