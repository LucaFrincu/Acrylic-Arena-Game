using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject squarePrefab; // Assign a square prefab in the inspector
    public int maxSquares = 5; // Maximum number of squares
    public float spawnRadius = 10f; // Radius within which squares can spawn
    private int currentSquares = 0; // Current number of squares

    private void Update()
    {
        if (currentSquares < maxSquares)
        {
            SpawnSquare();
        }
    }

    void SpawnSquare()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0f; // Adjust the Y position as needed

        //Define the rotation: 90 degrees around the X-axis
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

        Instantiate(squarePrefab, spawnPosition, spawnRotation);
        currentSquares++;
    }

    // Call this method when a square is destroyed
    public void OnSquareDestroyed()
    {
        currentSquares--;
    }
}
