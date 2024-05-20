using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public enum ShapeCombo
    {
        Wave,
        DiagonalLeft,
        DiagonalRight,
        VerticalUp,
        VerticalDown,
        HorizontalLeft,
        HorizontalRight,
        CircleLeft,
        CircleRight,
        Zero,
        Null,
        Wait
    }

    public ShapeCombo shapeFormCombo = ShapeCombo.Zero;

    public GameObject attackPlayer;

    public float delayTime = 1.0f; // Set the delay time between clicks

    private bool canClick = true; // Flag to track if clicking is allowed

    public bool finishCombo = false;
    public float colliderLifetime = 0.5f; // Time for the collider to disappear
    public float fixedDistance = 1f; // Fixed distance from the object to the collider
    Vector3 colliderSize = new Vector3(0f, 0f, 0f);
    public Color color = Color.white;

    public AudioSource test;
    public AudioClip basicAudio;
    public AudioClip comboAudio;
    public Vector3 directionToMouse;
    public Vector3 lastMousePosition;

    public float doubleClickTimeLimit = 0.2f;
    private float timeSinceLastClick = 0f;
    private bool isOneClick = false;
    private bool isTwoClicks = false;

    public Vector3 mousePos;
    public int nrAttacks = 0;
    public int nrCombo = 0;
    public bool checkmode = false;
    public DrawingController drawing;
    public int attackDmg = 0;
    public int manaBlue = 0;
    public int manaRed = 0;
    public int manaYellow = 0;
    public int manaMax = 90;

    public Image manaBlueFill;
    public Image manaRedFill;
    public Image manaYellowFill;


    public Image yellowBgd;
    public Image blueBgd;
    public Image redBgd;
    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<AudioSource>();
        //drawing = GetComponent<DrawingController>();
        //Debug.Log(shapeFormCombo);
        shapeFormCombo = ShapeCombo.Zero;
        manaBlueFill.fillAmount = 0.0f;
        manaRedFill.fillAmount = 0.0f;
        manaYellowFill.fillAmount = 0.0f;
    }

    private void FillBlueStamina()
    {
        if (manaBlue <= 90 && manaBlue > 60)
        {
            manaBlueFill.fillAmount = 1.0f;
            

        }
        else if (manaBlue <= 60 && manaBlue >= 35)
        {
            manaBlueFill.fillAmount = 0.66f;
            

        }
        else if (manaBlue < 35)
        {
            manaBlueFill.fillAmount = 0.33f;
            

        }
        if (manaBlue == 0)
        {
            manaBlueFill.fillAmount = 0.0f;
            
        }
    }

    private void FillRedStamina()
    {
        if (manaRed <= 90 && manaRed > 60)
        {
            manaRedFill.fillAmount = 1.0f;
            


        }
        else if (manaRed <= 60 && manaRed >= 35)
        {
            manaRedFill.fillAmount = 0.66f;
            

        }
        else if (manaRed < 35)
        {
            manaRedFill.fillAmount = 0.33f;
            

        }
        if (manaRed == 0)
        {
            manaRedFill.fillAmount = 0.0f;
        }
    }

    private void FillYellowStamina()
    {
        if (manaYellow <= 90 && manaYellow > 60)
        {
            manaYellowFill.fillAmount = 1.0f;
            


        }
        else if (manaYellow <= 60 && manaYellow >= 35)
        {
            manaYellowFill.fillAmount = 0.66f;
            

        }
        else if (manaYellow < 35)
        {
            manaYellowFill.fillAmount = 0.33f;
            

        }
        if (manaYellow == 0)
        {
            manaYellowFill.fillAmount = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
 
         SetShape();

        FillBlueStamina();
        FillRedStamina();
        FillYellowStamina();
   

        if (checkmode == false)
        {
            //move coordinates of the mouse
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            lastMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            nrAttacks = 0;
            nrCombo = 0;
            shapeFormCombo = ShapeCombo.Zero;
            color = Color.white;

        }
        else
        {
            //block coordinates of the mouse
            //Debug.Log("NrCombo " + CheckNrCombos());

            

            switch (CheckNrCombos())
            {
                case 0:
                    switch (shapeFormCombo)
                    {
                        case ShapeCombo.Zero:
                            /*if(nrAttacks < 3)
                                DoBasicAttack();
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }*/
                            break;
                        case ShapeCombo.VerticalUp:
                            if (manaBlue >= 30)
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                colliderSize = new Vector3(6f, 6f, 6f);
                                fixedDistance = 7f;
                                color = Color.blue;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                //nrCombo++;
                                shapeFormCombo = ShapeCombo.Null;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                attackDmg = 4;
                                manaBlue -= 30;
                                blueBgd.GetComponent<PaintSizeAnimation>().Decrease();

                                /*if(manaBlue <= 90 && manaBlue > 60)
                                {
                                    manaBlueFill.fillAmount = 1.0f;
                                    Debug.Log("MANA 1");

                                }
                                if (manaBlue < 60 && manaBlue >= 35)
                                {
                                    manaBlueFill.fillAmount = 0.66f;
                                    Debug.Log("MANA 2");
                                }
                                if (manaBlue < 35)
                                {
                                    manaBlueFill.fillAmount = 0.33f;
                                    Debug.Log("MANA 3");
                                }*/

                            }
                            else
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                shapeFormCombo = ShapeCombo.Null;
                                
                                //manaBlueFill.fillAmount = 0.0f;
                                
                            }
                            checkmode = false;
                            break;
                        case ShapeCombo.DiagonalRight:
                            if (manaRed >= 30)
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                colliderSize = new Vector3(6f, 6f, 5.5f);
                                fixedDistance = 7f;
                                color = Color.red;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                //nrCombo++;
                                shapeFormCombo = ShapeCombo.Null;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                attackDmg = 4;
                                manaRed -= 30;
                                redBgd.GetComponent<PaintSizeAnimation>().Decrease();
                            }
                            else
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            checkmode = false;
                            break;
                        case ShapeCombo.HorizontalRight:
                            if (manaYellow >= 30)
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                colliderSize = new Vector3(10f, 10f, 10f);
                                fixedDistance = 0f;
                                color = Color.yellow;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                //nrCombo++;
                                shapeFormCombo = ShapeCombo.Null;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                attackDmg = 4;
                                manaYellow -= 30;
                                yellowBgd.GetComponent<PaintSizeAnimation>().Decrease();
                            }
                            else
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            checkmode = false;
                            break;
                        case ShapeCombo.Wait:
                            if(drawing.shape == "not_found")
                            {
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                shapeFormCombo = ShapeCombo.Null;
                                color = Color.white;
                                checkmode = false;
                            }
                            break;
                        default:
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            shapeFormCombo = ShapeCombo.Null;
                            nrAttacks = 0;
                            nrCombo = 0;
                            color = Color.white;
                            checkmode = false;
                            break;
                    }
                    break;
                /*case 1:
                    switch (shapeFormCombo)
                    {
                        case ShapeCombo.Zero:
                            //DoBasicAttack();
                            break;
                        case ShapeCombo.VerticalDown:
                            if (manaBlue >= 30)
                            {
                                colliderSize = new Vector3(4f, 4f, 4f);
                                fixedDistance = 5f;
                                color = Color.blue;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo++;
                                shapeFormCombo = ShapeCombo.Wait;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                //nrAttacks = 0;
                                attackDmg = 5;
                                manaBlue -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            break;
                        case ShapeCombo.DiagonalLeft:
                            if(manaRed >= 30)
                            {
                                colliderSize = new Vector3(4f, 4f, 3.5f);
                                fixedDistance = 5f;
                                color = Color.red;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo++;
                                shapeFormCombo = ShapeCombo.Wait;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                //nrAttacks = 0;
                                attackDmg = 4;
                                manaRed -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            
                            break;
                        case ShapeCombo.HorizontalLeft:
                            if(manaYellow >= 30)
                            {
                                colliderSize = new Vector3(9f, 9f, 9f);
                                fixedDistance = 0f;
                                color = Color.yellow;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo++;
                                shapeFormCombo = ShapeCombo.Wait;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                //nrAttacks = 0;
                                attackDmg = 4;
                                manaYellow -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            
                            break;
                        case ShapeCombo.Wait:
                            break;
                        default:
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            shapeFormCombo = ShapeCombo.Zero;
                            nrAttacks = 0;
                            nrCombo = 0;
                            color = Color.white;
                            break;
                    }
                    break;
                case 2:
                    switch (shapeFormCombo)
                    {
                        case ShapeCombo.Zero:
                            //DoBasicAttack();
                            break;
                        case ShapeCombo.CircleRight:
                            if (manaBlue >= 30)
                            {
                                colliderSize = new Vector3(8f, 8f, 8f);
                                fixedDistance = 5f;
                                color = Color.blue;
                                test.clip = comboAudio;
                                test.Play(0);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo = 0;
                                nrAttacks = 0;
                                shapeFormCombo = ShapeCombo.Zero;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                nrAttacks = 0;
                                attackDmg = 7;
                                color = Color.white;
                                manaBlue -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            
                            break;
                        case ShapeCombo.Wave:
                            if(manaRed >= 30)
                            {
                                colliderSize = new Vector3(5f, 5f, 4.5f);
                                fixedDistance = 5f;
                                color = Color.red;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo = 0;
                                nrAttacks = 0;
                                shapeFormCombo = ShapeCombo.Zero;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                attackDmg = 4;
                                color = Color.white;
                                manaRed -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            
                            break;
                        case ShapeCombo.CircleLeft:
                            if(manaYellow >= 30)
                            {
                                colliderSize = new Vector3(11f, 11f, 11f);
                                fixedDistance = 0f;
                                color = Color.yellow;
                                test.clip = comboAudio;
                                test.PlayOneShot(comboAudio);
                                CreateTemporaryCollider(colliderSize, fixedDistance, color);
                                nrCombo = 0;
                                nrAttacks = 0;
                                shapeFormCombo = ShapeCombo.Zero;
                                drawing.shape = "";
                                drawing.shapeDirection = "";
                                attackDmg = 5;
                                color = Color.white;
                                manaYellow -= 30;
                            }
                            else
                            {
                                shapeFormCombo = ShapeCombo.Null;
                            }
                            
                            break;
                        case ShapeCombo.Wait:
                            break;
                        default:
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            shapeFormCombo = ShapeCombo.Zero;
                            nrAttacks = 0;
                            nrCombo = 0;
                            color = Color.white;
                            break;
                    }
                    break;*/

            }            
        }

        if (Input.GetMouseButtonDown(0) && checkmode == false && canClick)
        {
            /*if (!isOneClick) // first click
             {
                 isOneClick = true;
                 timeSinceLastClick = Time.time;
             }
             else if (Time.time - timeSinceLastClick <= doubleClickTimeLimit) // double click
             {
                 //Debug.Log("Double Click Detected");
                 isTwoClicks = true;
                 isOneClick = false; // reset for next click
             }*/
            StartCoroutine(ClickCooldown());
            DoBasicAttack();
        }

        /*if (isOneClick)
        {
            if ((Time.time - timeSinceLastClick) > doubleClickTimeLimit) // time exceeded for double click
            {
                //Debug.Log("Single Click Detected");
                //Debug.Log(shapeFormCombo);
                isOneClick = false; // reset for next click
            }
        }*/
    }

   

    IEnumerator ClickCooldown()
    {
        // Set clicking flag to false to prevent further clicks
        canClick = false;

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Set clicking flag to true to allow clicks again
        canClick = true;
    }

    public void DoBasicAttack()
    {
        //if (/*isOneClick == true && checkmode == false*/ /*&& (Time.time - timeSinceLastClick) > doubleClickTimeLimit*/)
        //{
            //do left to right attack
            //Debug.Log("1 ATTACK!");
            nrAttacks++;
            //Debug.Log("BA1 activated");
            //timeSinceLastCombo = Time.time;
            //CreateTemporaryBoxCollider(transform.position);
            colliderSize = new Vector3(6f, 5f, 5f);
            fixedDistance = 8f;
            test.clip = basicAudio;
            test.PlayOneShot(comboAudio);
            CreateTemporaryCollider(colliderSize, fixedDistance, color);
            attackDmg = 3;
            isOneClick = false;
        //}
        /*else if (isTwoClicks == true && Time.time - timeSinceLastClick <= doubleClickTimeLimit)
        {
            //Debug.Log("BA2 activated");
            //do double attack
            isTwoClicks = false;
            nrAttacks++;
            //timeSinceLastCombo = Time.time;
            //CreateTemporaryBoxCollider(transform.position);
            colliderSize = new Vector3(1.5f, 1f, 1f);
            fixedDistance = 1.2f;
            attackDmg = 6;
            CreateTemporaryCollider(colliderSize, fixedDistance, color);
        }*/
        if (CheckNrAttacks() > 2)
        {
            nrAttacks = 0;
            nrCombo = 0;
        }
    }

    void SetShape()
    {
        switch (drawing.shape)
        {
            case "verticalline":
                if (drawing.shapeDirection == "up")
                    shapeFormCombo = ShapeCombo.VerticalUp;
                else
                {
                    shapeFormCombo = ShapeCombo.VerticalDown;
                }
                break;
            case "horizontalline":
                if (drawing.shapeDirection == "left")
                    shapeFormCombo = ShapeCombo.HorizontalLeft;
                else
                {
                    shapeFormCombo = ShapeCombo.HorizontalRight;
                }
                break;
            case "diagonalline":
                //Debug.Log("SETTING DIAGONALLINE");
                if (drawing.shapeDirection == "right")
                    shapeFormCombo = ShapeCombo.DiagonalRight;
                else
                {
                    shapeFormCombo = ShapeCombo.DiagonalLeft;
                }
                break;
            case "circle":
                if (drawing.shapeDirection == "right")
                    shapeFormCombo = ShapeCombo.CircleRight;
                else
                {
                    shapeFormCombo = ShapeCombo.CircleLeft;
                }
                break;
            case "waveline":
                shapeFormCombo = ShapeCombo.Wave;
                break;
            case "one":
                shapeFormCombo = ShapeCombo.Null;
            break;
            default:
                shapeFormCombo = ShapeCombo.Wait;
                break;
        }
    }

    void CreateTemporaryCollider(Vector3 colliderSize, float fixedDistance, Color color)
    {
        //GameObject tempColliderObject = GameObject.Instantiate(attackPlayer);
        // Calculate the collider's position
        Vector3 colliderPosition = CalculateColliderPosition(fixedDistance);

        // Calculate direction from object to mouse cursor
        Vector3 mousePosition = mousePos;
        /*mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);*/
        Vector3 directionToMouse = lastMousePosition - transform.position;
        directionToMouse.Normalize();

        // Create the temporary collider GameObject

        GameObject tempColliderObject = new GameObject("TemporaryCollider");
        tempColliderObject.AddComponent<PlayerAttack>();
        // Add a Rigidbody component to the GameObject
        Rigidbody rb = tempColliderObject.AddComponent<Rigidbody>();

        // Disable gravity for the Rigidbody
        rb.useGravity = false;
        tempColliderObject.transform.position = colliderPosition;
        tempColliderObject.tag = "square";
        // Set the rotation of the collider to face the direction of the mouse
        // Calculate rotation based on the direction vector
        tempColliderObject.transform.rotation = Quaternion.LookRotation(directionToMouse);

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
        material.color =color;
        tempColliderObject.GetComponent<MeshRenderer>().material = material;
        tempColliderObject.GetComponent<MeshRenderer>().enabled = false;


        // Destroy the collider GameObject after a specified lifetime
        Destroy(tempColliderObject, colliderLifetime);
    }

    Vector3 CalculateColliderPosition(float distance)
    {
        // Get the mouse position in world space at the object's depth
        //Vector3 mouseScreenPosition = Input.mousePosition;
        //mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // Since sprites are rotated 90 degrees on the x-axis, we need to adjust the direction accordingly
        // Calculate direction from the object to the mouse position on the same plane as the object
        Vector3 objectPlanePosition = transform.position - new Vector3(0f, 0f, 1.5f);
        Vector3 directionToMouse = (lastMousePosition - objectPlanePosition).normalized;
        directionToMouse.y = 0; // Keep the direction in the xz-plane

        // Calculate the collider's position at a fixed distance from the object in the direction of the mouse
        Vector3 colliderPosition = objectPlanePosition + directionToMouse * distance;
        colliderPosition.y = transform.position.y; // Ensure the collider is at the same height as the object

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

    public bool CheckMode()
    {
        return checkmode;
    }

    public int CheckNrAttacks()
    {
        return nrAttacks;
    }

    public int CheckNrCombos()
    {
        return nrCombo;
    }


}
