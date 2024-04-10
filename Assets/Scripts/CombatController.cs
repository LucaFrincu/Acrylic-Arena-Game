using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public enum ShapeCombo
    {
        Wave,
        DiagonalLeft,
        DiagonalRight,
        CurveLeft,
        CurveRight,
        CircleLeft,
        CircleRight,
        Zero
    }

    public ShapeCombo shapeFormCombo = ShapeCombo.Zero;

    public float colliderLifetime = 0.5f; // Time for the collider to disappear
    public float fixedDistance = 1f; // Fixed distance from the object to the collider
    Vector3 colliderSize = new Vector3(0f, 0f, 0f);

    public Vector3 directionToMouse;
    Vector3 lastMousePosition;

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

    // Start is called before the first frame update
    void Start()
    {
        //drawing = GetComponent<DrawingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nrAttacks == 2)
        {
            SetShape();
        }
        if (checkmode == false)
        {
            //move coordinates of the mouse
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            lastMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            nrAttacks = 0;
            nrCombo = 0;

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
                            if(nrAttacks < 2)
                                DoBasicAttack();
                                break;
                        case ShapeCombo.CurveLeft:
                            colliderSize = new Vector3(1f, 2f, 1f);
                            fixedDistance = 1.2f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            attackDmg = 4;
                            break;
                        case ShapeCombo.DiagonalRight:
                            colliderSize = new Vector3(1f, 2f, 0.5f);
                            fixedDistance = 1.2f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            attackDmg = 4;
                            break;
                        case ShapeCombo.CircleLeft:
                            colliderSize = new Vector3(1.25f, 2f, 1.25f);
                            fixedDistance = 0f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            attackDmg = 4;
                            break;
                        default:
                            //shapeFormCombo = ShapeCombo.Zero;
                            /*nrAttacks = 0;
                            nrCombo = 0;*/
                            break;
                    }
                    break;
                case 1:
                    switch (shapeFormCombo)
                    {
                        case ShapeCombo.Zero:
                            if (nrAttacks < 2)
                                DoBasicAttack();
                            break;
                        case ShapeCombo.CurveRight:
                            colliderSize = new Vector3(1.5f, 2f, 1.5f);
                            fixedDistance = 1.5f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            nrAttacks = 0;
                            attackDmg = 5;
                            break;
                        case ShapeCombo.DiagonalLeft:
                            colliderSize = new Vector3(1f, 2f, 1f);
                            fixedDistance = 1.2f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            nrAttacks = 0;
                            attackDmg = 4;
                            break;
                        case ShapeCombo.CircleRight:
                            colliderSize = new Vector3(1.5f, 2f, 1.5f);
                            fixedDistance = 0f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo++;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            nrAttacks = 0;
                            attackDmg = 4;
                            break;
                        default:
                            //shapeFormCombo = ShapeCombo.Zero;
                            /*nrAttacks = 0;
                            nrCombo = 0;*/
                            break;
                    }
                    break;
                case 2:
                    switch (shapeFormCombo)
                    {
                        case ShapeCombo.Zero:
                            if (nrAttacks < 2)
                                DoBasicAttack();
                            break;
                        case ShapeCombo.CircleRight:
                            colliderSize = new Vector3(2f, 2f, 2f);
                            fixedDistance = 2f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo = 0;
                            nrAttacks = 0;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            nrAttacks = 0;
                            attackDmg = 7;
                            break;
                        case ShapeCombo.Wave:
                            colliderSize = new Vector3(1.25f, 2f, 1.25f);
                            fixedDistance = 1.25f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo = 0;
                            nrAttacks = 0;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            attackDmg = 4;
                            break;
                        case ShapeCombo.CircleLeft:
                            colliderSize = new Vector3(2f, 2f, 2f);
                            fixedDistance = 1.2f;
                            CreateTemporaryCollider(colliderSize, fixedDistance);
                            nrCombo = 0;
                            nrAttacks = 0;
                            shapeFormCombo = ShapeCombo.Zero;
                            drawing.shape = "";
                            drawing.shapeDirection = "";
                            attackDmg = 5;
                            break;
                        default:
                            //shapeFormCombo = ShapeCombo.Zero;
                            /*nrAttacks = 0;
                            nrCombo = 0;*/
                            break;
                    }
                    break;

            }            
        }        

        if (Input.GetMouseButtonDown(0))
        {
            if (!isOneClick) // first click
            {
                isOneClick = true;
                timeSinceLastClick = Time.time;
            }
            else if (Time.time - timeSinceLastClick <= doubleClickTimeLimit) // double click
            {
                Debug.Log("Double Click Detected");
                isTwoClicks = true;
                isOneClick = false; // reset for next click
            }
        }

        if (isOneClick)
        {
            if ((Time.time - timeSinceLastClick) > doubleClickTimeLimit) // time exceeded for double click
            {
                Debug.Log("Single Click Detected");
                isOneClick = false; // reset for next click
            }
        }
    }

    public void DoBasicAttack()
    {
        if (isOneClick == true && (Time.time - timeSinceLastClick) > doubleClickTimeLimit)
        {
            //do left to right attack
            nrAttacks++;
            //Debug.Log("BA1 activated");
            //timeSinceLastCombo = Time.time;
            //CreateTemporaryBoxCollider(transform.position);
            colliderSize = new Vector3(1.5f, 2f, 1f);
            fixedDistance = 1.2f;
            CreateTemporaryCollider(colliderSize, fixedDistance);
            attackDmg = 3;
        }
        else if (isTwoClicks == true && Time.time - timeSinceLastClick <= doubleClickTimeLimit)
        {
            //Debug.Log("BA2 activated");
            //do double attack
            isTwoClicks = false;
            nrAttacks++;
            //timeSinceLastCombo = Time.time;
            //CreateTemporaryBoxCollider(transform.position);
            colliderSize = new Vector3(1.5f, 2f, 1f);
            fixedDistance = 1.2f;
            attackDmg = 6;
            CreateTemporaryCollider(colliderSize, fixedDistance);
        }
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
            case "curveline":
                if (drawing.shapeDirection == "right")
                    shapeFormCombo = ShapeCombo.CurveRight;
                else
                {
                    shapeFormCombo = ShapeCombo.CurveLeft;
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
            default:
                shapeFormCombo = ShapeCombo.Zero;
                break;
        }
    }

    void CreateTemporaryCollider(Vector3 colliderSize, float fixedDistance)
    {
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
        tempColliderObject.transform.position = colliderPosition;

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
        tempColliderObject.GetComponent<MeshRenderer>().material = material;

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
        Vector3 objectPlanePosition = transform.position;
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
