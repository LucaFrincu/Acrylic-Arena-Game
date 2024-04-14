using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    private enum AIState { 
        Idle,
        Attacking,
        Cooldown 
    }

    public float colliderLifetime = 0.5f; // Time for the collider to disappear
    private AIState state = AIState.Idle;
    public float attackDelay = 3f;
    public float cooldownPeriod = 4f;
    private float lastAttackTime;
    public Color color = Color.white;


    public Vector3 direction;
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
        hit = GameObject.Find("Player").GetComponent<CombatController>();
        //hit = player.GetComponent<CombatController>();
        if (hit == null) {
            Debug.Log("hit null detected!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {

            switch (state)
            {
                case AIState.Idle:
                    // Calculate direction to the player
                    direction = (targetPosition - transform.position).normalized;
                    float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                    //Debug.Log(distanceToTarget);
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
                    else if(playerSeen && distanceToTarget <= stoppingDistance)
                    {
                        state = AIState.Attacking;
                    }
                    break;
                case AIState.Attacking:
                    if(Time.time - attackDelay >= attackDelay)
                    {

                        Debug.Log("ATTACK");
                        CreateTemporaryCollider(new Vector3(1f, 0f, 1f), 1f);
                        lastAttackTime = Time.time;
                        state = AIState.Cooldown;
                    }
                    break;
                case AIState.Cooldown:
                    // Check if cooldown period has passed
                    if (Time.time - lastAttackTime >= cooldownPeriod)
                    {
                        state = AIState.Idle;
                    }
                    break;
                default:
                    break;
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
        string strcolor = "";
        if(color == Color.white)
        {
            strcolor = "white";
        }
        else if(color == Color.blue)
        {
            strcolor = "blue";
        }
        else if (color == Color.red)
        {
            strcolor = "red";
        }
        else if (color == Color.yellow)
        {
            strcolor = "yellow";
        }

        if (other.gameObject.name == "TemporaryCollider" && other.gameObject.tag == "square")
        {
            switch (strcolor)
            {
                case "white":
                    hasCollided = true;
                    health -= hit.attackDmg;
                    SpawnColoredSquare(other.transform.position);
                    break;
                case "blue":

                    hasCollided = true;
                    health -= hit.attackDmg;
                    SpawnColoredSquare(other.transform.position);
                    break;
                default:
                    break;
            }

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
        square.tag = "squareenemy";
        square.transform.position = spawnPosition;
        square.transform.localScale = new Vector3(1f, 0.1f, 1f); // Make it a flat square
        Renderer renderer = square.GetComponent<Renderer>();
        renderer.material.color = new Color(1f, 0f, 1f); // Set color to magenta

        Destroy(square.GetComponent<Collider>());
        BoxCollider collider = square.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        // Optional: Destroy the square after some time
        Destroy(square, 1f);
    }


    void CreateTemporaryCollider(Vector3 colliderSize, float fixedDistance)
    {
        // Calculate the collider's position
        Vector3 colliderPosition = CalculateColliderPosition(fixedDistance);

        // Calculate direction from object to player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Create the temporary collider GameObject
        GameObject tempColliderObject = new GameObject("TemporaryCollider");
        tempColliderObject.transform.position = colliderPosition;

        // Set the rotation of the collider to face the direction of the player
        // Calculate rotation based on the direction vector
        tempColliderObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust the collider object's rotation to match the sprite's orientation if needed
        tempColliderObject.transform.eulerAngles = new Vector3(0, tempColliderObject.transform.eulerAngles.y, tempColliderObject.transform.eulerAngles.z);

        BoxCollider boxCollider = tempColliderObject.AddComponent<BoxCollider>();
        boxCollider.size = colliderSize;
        boxCollider.isTrigger = true; // Set as a trigger so it doesn't physically block objects

        // Optionally add a renderer to visualize the collider
        tempColliderObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = tempColliderObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateBoxMesh(colliderSize);
        Material material = new Material(Shader.Find("Standard"));
        tempColliderObject.GetComponent<MeshRenderer>().material = material;

        // Destroy the collider GameObject after a specified lifetime
        Destroy(tempColliderObject, colliderLifetime);
    }

    Vector3 CalculateColliderPosition(float distance)
    {
        // Calculate the collider's position at a fixed distance from the object in the direction of the player
        Vector3 colliderPosition = transform.position + (playerTransform.position - transform.position).normalized * distance;

        // Ensure the collider is at the same height as the object
        colliderPosition.y = transform.position.y;

        return colliderPosition;
    }



    Mesh CreateBoxMesh(Vector3 size)
    {
        // Create a simple cube mesh
        Mesh mesh = new Mesh();
        Vector3 p0 = new Vector3(-size.x, -size.y, -size.z) * 0.5f;
        Vector3 p1 = new Vector3(size.x, -size.y, -size.z) * 0.5f;
        Vector3 p2 = new Vector3(size.x, -size.y, size.z) * 0.5f;
        Vector3 p3 = new Vector3(-size.x, -size.y, size.z) * 0.5f;
        Vector3 p4 = new Vector3(-size.x, size.y, -size.z) * 0.5f;
        Vector3 p5 = new Vector3(size.x, size.y, -size.z) * 0.5f;
        Vector3 p6 = new Vector3(size.x, size.y, size.z) * 0.5f;
        Vector3 p7 = new Vector3(-size.x, size.y, size.z) * 0.5f;

        Vector3[] vertices = new Vector3[]
        {
            // Bottom
            p0, p1, p2, p3,
            // Left
            p7, p4, p0, p3,
            // Front
            p4, p5, p1, p0,
            // Back
            p6, p7, p3, p2,
            // Right
            p5, p6, p2, p1,
            // Top
            p7, p6, p5, p4
        };

        int[] triangles = new int[]
        {
            // Bottom
            3, 1, 0,
            3, 2, 1,            
            // Left
            7, 5, 4,
            7, 6, 5,
            // Front
            11, 9, 8,
            11, 10, 9,
            // Back
            15, 13, 12,
            15, 14, 13,
            // Right
            19, 17, 16,
            19, 18, 17,
            // Top
            23, 21, 20,
            23, 22, 21
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}

