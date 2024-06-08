using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDraw : MonoBehaviour
{
    public bool hasCollided = false;
    private GameObject parentSpawner;
    public string flowerColorName;
    public Color flowerColor;
    public Color newColor;
    private short checkCounter = 0;
    public int zone = 0;
    public int health = 1;
    public int maxHealth = 0;
    public int dmgDealt = 1;

    private void Update()
    {
        if(health <= 0)
        {
            //SET COLLIDER TO FALSE INSTEAD OF MAKING THE OBJECT DISAPPEAR
            Collider collider = gameObject.GetComponent<Collider>();
            collider.enabled = false;
            if (checkCounter == 0)
            {
                checkCounter++;
                hasCollided = false;
                Debug.Log("INTERACTED WITH ATTACK");
                // Notify the spawner that a square is being destroyed
                health = maxHealth;
                checkCounter = 0;
                // Destroy this square
                //gameObject.SetActive(false);                
                //gameObject.SetActive(true);
                parentSpawner.GetComponent<FlowerSpawner>().OnSquareDestroyed();


            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if this square has collided with a square spawned by an enemy
        Debug.Log("Collided with object" + other);
        if (other.gameObject.tag == "square" && other.gameObject.name == "TemporaryCollider" && hasCollided == false)
        {
            /*hasCollided = true;
            Debug.Log("INTERACTED WITH ATTACK");
            // Notify the spawner that a square is being destroyed

            // Destroy this square
            gameObject.SetActive(false);
            //gameObject.SetActive(true);

            parentSpawner.GetComponent<FlowerSpawner>().OnSquareDestroyed();*/
            Color colorOfAttack = other.GetComponent<PlayerAttack>().attackColor;
            if (colorOfAttack != Color.red && colorOfAttack != Color.blue && colorOfAttack != Color.yellow)
                other.gameObject.GetComponent<PlayerAttack>().SetPattern(gameObject);
            else
            {
                if(colorOfAttack == Color.red)
                {
                    DestroyFlower("red", Color.red);
                }
                if (colorOfAttack == Color.blue)
                {
                    DestroyFlower("blue", Color.blue);
                }
                if (colorOfAttack == Color.yellow)
                {
                    DestroyFlower("yellow", Color.yellow);
                }
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        hasCollided = false;
    }*/

    public void DestroyFlower(string colorName, Color mixedColor)
    {
        Debug.Log("mixed Color: " + colorName + " The required color" + flowerColor + " The name of the color: " + flowerColorName);
        if (health > 0 && (colorName == flowerColorName || flowerColorName == "white"))
        {
            Debug.Log("HIT WITH THE SPECIFIED COLOR!");
            health -= dmgDealt;
            //health = Mathf.Clamp(health, 0, maxHealth);
            float healthPercentage = (float)health / maxHealth;
            if (flowerColorName != "white")
            {
                Debug.Log("IS NOT WHITE!");
                newColor = Color.Lerp(flowerColor, Color.white, healthPercentage);
                newColor.a = 1;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
            else
            {
                Debug.Log("IS WHITE!");
                newColor = Color.Lerp(mixedColor, Color.white, healthPercentage);
                newColor.a = 1;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
        }
    }

    public void SetSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
    }
}
