using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    //public GameObject ownReference;
    public GameObject squarePrefab; // Assign a square prefab in the inspector
    private GameObject squares;
    public GameObject zoneManager;
    public bool restrictSpawn = true;
    private int maxSquares = 0; // Maximum number of squares
    public int spawnZone = 0;
    //public float spawnRadius = 0f; // Radius within which squares can spawn
    public int currentSquares = 0; // Current number of squares
    public bool family = false;
    public int patternZone = -1;
    private void Start()
    {
        SpawnSquare();
        squares.gameObject.SetActive(false);
        /*for(int i = 0; i < squarePrefab.transform.childCount; i++)
        {
            squarePrefab.transform.GetChild(i).gameObject.SetActive(false);
        }*/
    }

    private void Update()
    {

        //Debug.Log(squarePrefab.transform.childCount);
        /*if (currentSquares < maxSquares)
        {
            SpawnSquare();
        }*/
        
    }

    void SpawnSquare()
    {
        Vector3 spawnPosition = transform.position /*+ Random.insideUnitSphere * spawnRadius*/;

        spawnPosition.y = -2f;
        //spawnPosition.y = 0f; // Adjust the Y position as needed

        //Define the rotation: 90 degrees around the X-axis
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
        
        squares =Instantiate(squarePrefab, spawnPosition, spawnRotation);
        squares.transform.SetParent(gameObject.transform);
        for (int i = 0; i < squarePrefab.transform.childCount; i++)
        {
            squares.transform.GetChild(i).GetComponent<FlowerDraw>().SetSpawner(gameObject);
        }
        //currentSquares++;
        //Debug.Log("Flower Spawned");
        
    }

    // Call this method when a square is destroyed
    public void OnSquareDestroyed()
    {
        //Debug.Log(currentSquares);
        currentSquares--;
        if (squarePrefab.transform.childCount == currentSquares * (-1))
        {
            if (family == false)
            {
                Debug.Log("RESTRICTING ENEMIES SPAWNS FROM ZONE " + spawnZone);
                zoneManager.GetComponent<PlayerSpawn>().CompletePart(spawnZone);
            }
            else
            {
                zoneManager.GetComponent<PlayerSpawn>().KillFamily();
            }
        }
        //squares == ;
        //Debug.Log(currentSquares);
    }

    public void ResetPattern(int zone, bool checkpoint)
    {
        if (spawnZone == zone)
        {
            /*Debug.Log("currentSquares " + currentSquares);
            if (currentSquares == 1)
            {
                Destroy(squares);
                OnSquareDestroyed();
            }
         
            SpawnSquare();*/
            if (checkpoint)
            {
                Collider childCollider;
                GameObject child;
                for (int i = 0; i < squares.transform.childCount; i++)
                {
                    Debug.Log("SET " + squares.transform.GetChild(i) + " TO ACTIVE");
                    squares.transform.GetChild(i).gameObject.SetActive(true);
                    squares.gameObject.SetActive(true);
                    // TRY 1
                    childCollider = squares.transform.GetChild(i).gameObject.GetComponent<Collider>();
                    child = squares.transform.GetChild(i).gameObject;
                    child.GetComponent<FlowerDraw>().health = child.GetComponent<FlowerDraw>().maxHealth;
                    child.GetComponent<SpriteRenderer>().color = Color.white;
                    childCollider.enabled = true;
                    /////
                }
            }
            else
            {
                Collider childCollider;
                for (int i = 0; i < squares.transform.childCount; i++)
                {
                    Debug.Log(squares.transform.GetChild(i).gameObject);
                    //TRY 1
                    squares.transform.GetChild(i).gameObject.SetActive(false);
                    squares.gameObject.SetActive(false);
                    childCollider = squares.transform.GetChild(i).gameObject.GetComponent<Collider>();
                    childCollider.enabled = false;
                    //squares.gameObject.SetActive(true);
                    //////////////
                }
            }
            //currentSquares = 0;
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

    public bool CheckFamily()
    {
        return family;
    }

    public void SetRestriction(bool restriction)
    {
        restrictSpawn = restriction;
    }
}
