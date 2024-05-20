using System;
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

    public GameObject coloredSquareObject;

    public GameObject pattern = null;
    private GameObject parentSpawner;

    public string enemyColor;

    public AudioSource test;
    public AudioClip dmgSound;
    public float colliderLifetime = 0.5f; // Time for the collider to disappear
    private AIState state = AIState.Idle;
    public float attackDelay = 2f;
    private bool isAttacking = false;
    private DateTime currentTime;
    public float cooldownPeriod = 2f;
    private float lastAttackTime;
    public Color color = Color.white;
    private DateTime attackStartTime;

    public Vector3 direction;
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

    public float squareSpawnOffset = 4f; // Offset for spawning the square

    void Start()
    {
        test = GetComponent<AudioSource>();
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
        if (playerTransform != null && !isStaggered) // Check if not staggered
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

                        //Debug.Log("seeking!");
                    }
                    else
                    {
                        playerSeen = false;
                    }

                    if (playerSeen && distanceToTarget > stoppingDistance)
                    {
                        // Move enemy towards the target position
                        transform.position += moveSpeed * Time.deltaTime * direction;
                    }
                    else if(playerSeen && distanceToTarget <= stoppingDistance)
                    {
                        //Debug.Log("Entered attacking state");
                        state = AIState.Attacking;
                        attackStartTime = DateTime.Now;
                    }
                    break;
                case AIState.Attacking:
                    // Calculate the elapsed time since the attack delay started
                    TimeSpan elapsedTime = DateTime.Now - attackStartTime;
                    if (elapsedTime.TotalSeconds >= attackDelay)
                    {
                        //Debug.Log("ATTACK");
                        CreateTemporaryCollider(new Vector3(5f, 4f, 5f), 5f);
                        lastAttackTime = Time.time;
                        state = AIState.Cooldown;
                    }
                    break;
                case AIState.Cooldown:
                    // Check if cooldown period has passed
                    if (Time.time - lastAttackTime >= cooldownPeriod)
                    {
                        //Debug.Log("Entered idle state");
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
            parentSpawner.GetComponent<EnemySpawner>().OnEnemyDestroyed(); // Update the spawner's enemy count
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

    public void SetSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "TemporaryCollider" && other.gameObject.tag == "square")
        {
            test.PlayOneShot(dmgSound);
            string strcolor = "";
            Color otherColor = other.GetComponent<MeshRenderer>().material.color;
            if (otherColor == Color.white)
            {
                strcolor = "white";
            }
            else if (otherColor == Color.blue)
            {
                strcolor = "blue";
            }
            else if (otherColor == Color.red)
            {
                strcolor = "red";
            }
            else if (otherColor == Color.yellow)
            {
                strcolor = "yellow";
            }

            switch (strcolor)
            {
                case "white":
                    hasCollided = true;
                    health -= hit.attackDmg;
                    switch (enemyColor)
                    {
                        case "blue":
                            if (hit.manaBlue < hit.manaMax)
                                hit.manaBlue += 30;
                            break;
                        case "red":
                            if (hit.manaRed < hit.manaMax)
                                hit.manaRed += 30;
                            break;
                        case "yellow":
                            if (hit.manaYellow < hit.manaMax)
                                hit.manaYellow += 30;
                            break;
                        default:
                            break;
                    }
                    //SpawnColoredSquare(other.transform.position, otherColor);
                    break;
                case "blue":
                    hasCollided = true;
                    health -= hit.attackDmg;
                    
                    switch (enemyColor)
                    {
                        case "blue":
                            SpawnColoredSquare(other.transform.position, otherColor);
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(otherColor, "blue");
                            break;
                        case "yellow":
                            SpawnColoredSquare(other.transform.position, Color.green);
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(Color.green, "green");
                            break;
                        case "red":
                            SpawnColoredSquare(other.transform.position, new Color(0.5f, 0f, 0.5f)); //purple
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(new Color(0.5f, 0f, 0.5f), "purple");
                            break;
                        default:
                            break;
                    }
                    //SpawnColoredSquare(other.transform.position, otherColor);
                    break;
                case "red":
                    hasCollided = true;
                    health -= hit.attackDmg;
                    
                    switch (enemyColor)
                    {
                        case "blue":
                            SpawnColoredSquare(other.transform.position, new Color(0.5f, 0f, 0.5f)); // purple
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(new Color(0.5f, 0f, 0.5f), "purple");
                            break;
                        case "yellow":
                            SpawnColoredSquare(other.transform.position, new Color(1f, 0.5f, 0f)); // orange
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(new Color(1f, 0.5f, 0f), "orange");
                            break;
                        case "red":
                            SpawnColoredSquare(other.transform.position, otherColor);
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(otherColor, "red");
                            break;
                        default:
                            break;
                    }
                    //SpawnColoredSquare(other.transform.position, otherColor);
                    break;
                case "yellow":
                    hasCollided = true;
                    health -= hit.attackDmg;
                    
                    switch (enemyColor)
                    {
                        case "blue":
                            SpawnColoredSquare(other.transform.position, Color.green); // green
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(Color.green, "green");
                            break;
                        case "yellow":
                            SpawnColoredSquare(other.transform.position, otherColor); 
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(otherColor, "yellow");
                            break;
                        case "red":
                            SpawnColoredSquare(other.transform.position, new Color(1f, 0.5f, 0f));  // orange
                            other.gameObject.GetComponent<PlayerAttack>().SetEnemyColor(new Color(1f, 0.5f, 0f), "red");
                            break;
                        default:
                            break;
                    }
                    //SpawnColoredSquare(other.transform.position, otherColor);
                    break;
                default:
                    break;
            }

            /*hasCollided = true;
            health -= hit.attackDmg;
            SpawnColoredSquare(other.transform.position, color);*/

            /*playerSeen = false;
              stunTime -= false;*/
            isStaggered = true;
        }
    }

    void SpawnColoredSquare(Vector3 collisionPosition, Color color)
    {
        // Calculate spawn position for the square
        Vector3 direction = (collisionPosition - transform.position).normalized;
        Vector3 spawnPosition = transform.position - direction * squareSpawnOffset;
        spawnPosition = new Vector3(spawnPosition.x, -0.9f, spawnPosition.z);
        GameObject coloredSquare = GameObject.Instantiate(coloredSquareObject);

        coloredSquare.transform.position = spawnPosition;
        coloredSquare.tag = "squareenemy";
        Renderer renderer = coloredSquare.GetComponent<Renderer>();
        renderer.material.color = color; // Set color to magenta
        Destroy(coloredSquare, 5f);

        /*// Create the square
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.tag = "squareenemy";
        square.transform.position = spawnPosition;
        square.transform.localScale = new Vector3(1f, 0.1f, 1f); // Make it a flat square
        Renderer renderer = square.GetComponent<Renderer>();
        renderer.material.color = color; // Set color to magenta

        Destroy(square.GetComponent<Collider>());
        BoxCollider collider = square.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        // Optional: Destroy the square after some time
        Destroy(square, 5f);*/
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
        tempColliderObject.tag = "enemyattack";

        // Set the rotation of the collider to face the direction of the player
        // Calculate rotation based on the direction vector
        tempColliderObject.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust the collider object's rotation to match the sprite's orientation if needed
        tempColliderObject.transform.eulerAngles = new Vector3(0, tempColliderObject.transform.eulerAngles.y, tempColliderObject.transform.eulerAngles.z);

        BoxCollider boxCollider = tempColliderObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(colliderSize.x, 100f, colliderSize.z);
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
