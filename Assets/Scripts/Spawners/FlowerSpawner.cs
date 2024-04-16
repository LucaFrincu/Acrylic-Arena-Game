using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject ownReference;
    public GameObject squarePrefab; // Assign a square prefab in the inspector
    public GameObject squares;
    private int maxSquares = 0; // Maximum number of squares
    public int spawnZone = 0;
    //public float spawnRadius = 0f; // Radius within which squares can spawn
    private int currentSquares = 0; // Current number of squares

    private void Start()
    {
        SpawnSquare();
    }

    private void Update()
    {
        /*if (currentSquares < maxSquares)
        {
            SpawnSquare();
        }*/
    }

    void SpawnSquare()
    {
        Vector3 spawnPosition = transform.position /*+ Random.insideUnitSphere * spawnRadius*/;
        if (spawnZone == 4)
        {
            spawnPosition.y = 0.51f;
        }
        else { spawnPosition.y = 0f; }
        //spawnPosition.y = 0f; // Adjust the Y position as needed

        //Define the rotation: 90 degrees around the X-axis
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);
        
        squares =Instantiate(squarePrefab, spawnPosition, spawnRotation);
        squares.GetComponent<FlowerDraw>().SetSpawner(ownReference);
        currentSquares++;
        //Debug.Log("Flower Spawned");
        
    }

    // Call this method when a square is destroyed
    public void OnSquareDestroyed()
    {
        //Debug.Log(currentSquares);
        currentSquares--;
        //squares == ;
        //Debug.Log(currentSquares);
    }

    public void ResetPattern(int zone)
    {
        if (spawnZone == zone)
        {
            Debug.Log("currentSquares " + currentSquares);
            if (currentSquares == 1)
            {
                Destroy(squares);
                OnSquareDestroyed();
            }
         
            SpawnSquare();
        }   
    }

    public bool CheckSquares()
    {
        if(currentSquares == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
}
