using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DrawingController : MonoBehaviour
{
    public InputField labelInputField;
    public LineRenderer lineRenderer;
    public List<Vector3> drawingPoints = new List<Vector3>();
    public float pointRemovalDistance = 1.111f;
    public Vector3 startingPoint = new Vector3();
    public Vector3 endingPoint = new Vector3();
    private float minDistanceBetweenPoints = 0.1f;
    string jsonFilePath;
    public float threshold = 0.11f;
    // Threshold for roundness
    public float roundnessThreshold = 0.9f;
    public string shape = "", shapeDirection = "";
    public CombatController combat;



    private Dictionary<string, List<List<Vector3>>> shapesDictionary; // Dictionary to store loaded shapes
    void Start()
    {
        jsonFilePath = Path.Combine(Application.dataPath, "JsonFile", "drawings.json");
        //combat = GetComponent<CombatController>();
    }

    void Update()
    {
        if (combat.CheckMode() == true /*&& combat.nrAttacks == 2*/) {
            if (Input.GetMouseButtonDown(0))
            {
                endingPoint = startingPoint = GetMouseWorldPosition();
                AddPointToLine(startingPoint);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = GetMouseWorldPosition();
                AddPointToLine(mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //endingPoint = GetMouseWorldPosition();
                AddPointToLine(endingPoint);
                if (drawingPoints.Count > 1)
                {
                    RemovePoints();
                    drawingPoints = StabilizePoints(drawingPoints);
                    startingPoint = drawingPoints[0];
                    endingPoint = drawingPoints[^1];
                    LoadShapesFromJSON();
                    RecognizeShape();
                    CheckDirection();
                    shapesDictionary.Clear();
                    drawingPoints.Clear();
                    lineRenderer.positionCount = 0;
                    //shape = ""; 
                    //shapeDirection = "";
                    UpdateLineRenderer();
                }
                else
                {
                    drawingPoints.Clear();
                    lineRenderer.positionCount = 0;
                }
                //ManagePoints(startingPoint, endingPoint);
            }
           /*else if (Input.GetKeyDown(KeyCode.P))
            {
                PrintLine();
                drawingPoints.Clear();
                lineRenderer.positionCount = 0;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                LoadShapesFromJSON();
                RecognizeShape();
                shapesDictionary.Clear();
                CheckDirection();
                drawingPoints.Clear();
                lineRenderer.positionCount = 0;
                shape = shapeDirection = null;

            }
            // Check for 's' key press to save points
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SavePointsToJson();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {

            }*/
        }
    }

    void CheckDirection()
    {
        switch (shape) {
            case "verticalline":
                if (startingPoint.y < endingPoint.y)
                {
                    shapeDirection = "up";
                }
                else
                {
                    shapeDirection = "down";
                }
                break;
            case "horizontalline":
                if (startingPoint.x > endingPoint.x)
                {
                    shapeDirection = "left";
                }
                else
                {
                    shapeDirection = "right";
                }
                break;
            case "diagonalline":
                if (startingPoint.x > endingPoint.x && startingPoint.y > endingPoint.y)
                {
                    shapeDirection = "left";
                }
                else
                {
                    shapeDirection = "right";
                }
                break;
            case "circle":
                //Debug.Log(startingPoint.x + " " + drawingPoints[1].x);
                if (startingPoint.y < drawingPoints[1].y)
                {
                    shapeDirection = "left";
                }
                else
                {
                    shapeDirection = "right";
                }
                break;
            default:
                //Debug.Log("Direction not found");
                break;


        }

        //Debug.Log("Direction is: " + shapeDirection);
    }

    void RecognizeShape()
    {
        float bestMatchScore = float.MaxValue; // Initialize the best match score to a large value
        float roundness = 0f;
        string bestMatchName = null; // Initialize the name of the best matching shape to null
        // Iterate through each shape category
        foreach (KeyValuePair<string, List<List<Vector3>>> shapeCategory in shapesDictionary)
        {
            string categoryName = shapeCategory.Key;
            List<List<Vector3>> referenceShapes = shapeCategory.Value;

            // Compare the drawn shape with each reference shape in the category
            foreach (List<Vector3> referenceShape in referenceShapes)
            {
                // Calculate similarity score between drawn shape and reference shape
                float similarityScore = CalculateSimilarity(drawingPoints, referenceShape);

                // Update best match if similarity score is better than previous best match
                if (similarityScore < bestMatchScore)
                {
                    bestMatchScore = similarityScore;
                    bestMatchName = categoryName;
                }
            }
        }
        //Debug.Log("Best match: "+bestMatchScore);
        roundness = CalculateRoundness(drawingPoints);
        //Debug.Log("roundness value: " + roundness);
        // If a match with low similarity score is found, recognize the shape
        if (bestMatchScore < threshold)
        {
            Debug.Log("Recognized shape: " + bestMatchName);
            shape = bestMatchName;
        }else if (roundness > roundnessThreshold) {
            //Debug.Log("Roundness Recognized. It is a circle");
            shape = "circle";
        }
        else
        {
            Debug.Log("No matching shape found.");
            shape = "";
            shapeDirection = "";
        }

        // Clear drawn shape points after recognition
        //drawingPoints.Clear();
    }


    // Method to calculate roundness of a shape
    float CalculateRoundness(List<Vector3> shapePoints)
    {
        // Calculate perimeter of the shape
        float perimeter = CalculatePerimeter(shapePoints);

        // Calculate area of the shape
        float area = CalculateArea(shapePoints);

        // Calculate roundness as the ratio of shape's area to the area of a circle with the same perimeter
        float roundness = area / (Mathf.PI * Mathf.Pow(perimeter / (2 * Mathf.PI), 2));

        return roundness;
    }

    // Method to calculate perimeter of a shape
    float CalculatePerimeter(List<Vector3> shapePoints)
    {
        float perimeter = 0;
        int numPoints = shapePoints.Count;

        for (int i = 0; i < numPoints; i++)
        {
            Vector3 point1 = shapePoints[i];
            Vector3 point2 = shapePoints[(i + 1) % numPoints]; // Wrap around for the last point

            // Calculate distance between consecutive points and add to perimeter
            perimeter += Vector3.Distance(point1, point2);
        }

        return perimeter;
    }

    // Method to calculate area of a shape SHOELACE FORMULA
    float CalculateArea(List<Vector3> shapePoints)
    {
        float area = 0;
        int numPoints = shapePoints.Count;

        for (int i = 0; i < numPoints; i++)
        {
            Vector3 point1 = shapePoints[i];
            Vector3 point2 = shapePoints[(i + 1) % numPoints]; // Wrap around for the last point

            area += (point1.x * point2.y - point2.x * point1.y);
        }

        return Mathf.Abs(area) * 0.5f;
    }

    float CalculateSimilarity(List<Vector3> drawnShape, List<Vector3> referenceShape)
    {
        // Implement shape comparison algorithm here
        // For demonstration, let's just return a placeholder similarity score
        return CalculateHausdorffDistance(drawnShape, referenceShape);
    }

    // Function to calculate Hausdorff distance between two shapes
    float CalculateHausdorffDistance(List<Vector3> shape1, List<Vector3> shape2)
    {
        float maxDistance = float.MinValue;

        // Calculate Hausdorff distance from shape1 to shape2
        foreach (Vector3 point1 in shape1)
        {
            float minDistance = float.MaxValue;
            foreach (Vector3 point2 in shape2)
            {
                float distance = Vector3.Distance(point1, point2);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            if (minDistance > maxDistance)
            {
                maxDistance = minDistance;
            }
        }

        // Calculate Hausdorff distance from shape2 to shape1
        foreach (Vector3 point2 in shape2)
        {
            float minDistance = float.MaxValue;
            foreach (Vector3 point1 in shape1)
            {
                float distance = Vector3.Distance(point2, point1);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            if (minDistance > maxDistance)
            {
                maxDistance = minDistance;
            }
        }

        return maxDistance;
    }


    void LoadShapesFromJSON()
    {
        try
        {
            // Read JSON file
            string jsonString = File.ReadAllText(jsonFilePath);

            // Deserialize JSON data
            JObject json = JsonConvert.DeserializeObject<JObject>(jsonString);

            // Extract shapes data from JSON
            shapesDictionary = new Dictionary<string, List<List<Vector3>>>();
            foreach (JProperty shapeProperty in json["patterns"])
            {
                string shapeName = shapeProperty.Name;
                List<List<Vector3>> shapePointsList = new List<List<Vector3>>();
                foreach (JArray pointsArray in shapeProperty.Value)
                {
                    List<Vector3> pointsList = new List<Vector3>();
                    foreach (JObject pointObject in pointsArray)
                    {
                        float x = (float)pointObject["x"];
                        float y = (float)pointObject["y"];
                        float z = (float)pointObject["z"];
                        pointsList.Add(new Vector3(x, y, z));
                    }
                    shapePointsList.Add(pointsList);
                }
                shapesDictionary.Add(shapeName, shapePointsList);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error loading JSON file: " + e.Message);
        }
    }


    List<Vector3> StabilizePoints(List<Vector3> points)
    {
        // Translate points to center them around the origin
        Vector3 centroid = CalculateCentroid(points);
        List<Vector3> translatedPoints = TranslatePoints(points, -centroid);

        // Scale points to fit within a fixed-size bounding box
        float scaleFactor = CalculateScaleFactor(translatedPoints);
        List<Vector3> scaledPoints = ScalePoints(translatedPoints, 1.0f / scaleFactor);

        //Debug.Log("Points Operated");
        return scaledPoints;
    }


    Vector3 CalculateCentroid(List<Vector3> points)
    {
        Vector3 sum = Vector3.zero;
        foreach (Vector3 point in points)
        {
            sum += point;
        }
        return sum / points.Count;
    }


    // Helper method to translate points by a given offset
    List<Vector3> TranslatePoints(List<Vector3> points, Vector3 offset)
    {
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach (Vector3 point in points)
        {
            translatedPoints.Add(point + offset);
        }
        return translatedPoints;
    }

    // Helper method to calculate the scale factor to fit points within a unit bounding box
    float CalculateScaleFactor(List<Vector3> points)
    {
        Bounds bounds = new Bounds(points[0], Vector3.zero);
        foreach (Vector3 point in points)
        {
            bounds.Encapsulate(point);
        }
        return Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
    }

    // Helper method to scale points by a given factor
    List<Vector3> ScalePoints(List<Vector3> points, float scaleFactor)
    {
        List<Vector3> scaledPoints = new List<Vector3>();
        foreach (Vector3 point in points)
        {
            scaledPoints.Add(point * scaleFactor);
        }
        return scaledPoints;
    }


    // Method to get label input from the InputField
    private string GetLabelFromInputField()
    {
        // Return the text entered in the InputField
        return labelInputField.text;
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void AddPointToLine(Vector3 point)
    {
        if (drawingPoints.Count == 0 || Vector3.Distance(drawingPoints[drawingPoints.Count - 1], point) > minDistanceBetweenPoints)
        {
            drawingPoints.Add(point);
            //Debug.Log("Added point");
            endingPoint = point;
            UpdateLineRenderer();
        }
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = drawingPoints.Count;
        lineRenderer.SetPositions(drawingPoints.ToArray());
    }

    void PrintLine()
    {
        Debug.Log("Line Points:");
        /*foreach (Vector3 point in drawingPoints)
        {
            Debug.Log(point);
        }*/
        Debug.Log(drawingPoints.Count);
    }


    void RemovePoints()
    {
        if (drawingPoints.Count < 2)
        {
            return; // Need at least 2 points to form a line
        }

        float totalDistance = 0f;
        for (int i = 1; i < drawingPoints.Count; i++)
        {
            totalDistance += Vector3.Distance(drawingPoints[i - 1], drawingPoints[i]);
        }

        float targetDistance = totalDistance / 19; // Since you want exactly 20 points, you need 19 segments between points

        List<Vector3> newPoints = new List<Vector3>();
        newPoints.Add(drawingPoints[0]);

        Vector3 lastPoint = drawingPoints[0];
        float accumulatedDistance = 0f;

        for (int i = 1; i < drawingPoints.Count; i++)
        {
            float distance = Vector3.Distance(lastPoint, drawingPoints[i]);
            accumulatedDistance += distance;

            while (accumulatedDistance >= targetDistance)
            {
                float t = (targetDistance - (accumulatedDistance - distance)) / distance;
                Vector3 newPoint = Vector3.Lerp(lastPoint, drawingPoints[i], t);
                newPoints.Add(newPoint);
                accumulatedDistance -= targetDistance;
            }

            lastPoint = drawingPoints[i];
        }

        drawingPoints = newPoints;

        UpdateLineRenderer();
    }
    void SavePointsToJson()
    {
        string fileName = "drawings.json";
        string filePath = Path.Combine(Application.dataPath, "JsonFile", fileName);

        DrawingData data = LoadDataFromFile(filePath); // Load existing data
        string label = GetLabelFromInputField();

        // Check if the label already exists
        if (data.patterns == null)
        {
            data.patterns = new Dictionary<string, List<List<PointData>>>();
        }

        if (!data.patterns.ContainsKey(label))
        {
            data.patterns[label] = new List<List<PointData>>();
        }

        // Add new points to the existing label
        List<PointData> points = new List<PointData>();
        foreach (Vector3 point in drawingPoints)
        {
            PointData pointData = new PointData();
            pointData.x = point.x;
            pointData.y = point.y;
            pointData.z = point.z;
            points.Add(pointData);
        }

        // Add the points to the pattern
        data.patterns[label].Add(points);

        // Serialize data to JSON using Newtonsoft.Json
        string json = JsonConvert.SerializeObject(data);

        // Write JSON data to file
        File.WriteAllText(filePath, json);
    }

    // Method to load existing data from file or create new data if file doesn't exist
    DrawingData LoadDataFromFile(string filePath)
    {
        DrawingData data;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<DrawingData>(json);
        }
        else
        {
            data = new DrawingData();
        }

        return data;
    }


    // Class definitions for JSON data structure
    [System.Serializable]
    public class DrawingData
    {
        public Dictionary<string, List<List<PointData>>> patterns;
    }


    [System.Serializable]
    public class PointData
    {
        public float x;
        public float y;
        public float z;
    }

}