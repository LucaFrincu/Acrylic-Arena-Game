using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform target; // The object to follow
    public float arrowDistance = 20f; // Distance between the arrow and the target
    public float arrowSpeed = 5f; // Speed of arrow movement

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y; // Ensure the arrow stays on the same y level as the target

        // Calculate the direction from the arrow to the mouse cursor
        Vector3 directionToMouse = mousePosition - target.position;

        // Adjust the direction to ignore changes in the y-axis (upward)
        directionToMouse.y = 0f;

        // Update the arrow's position towards the mouse cursor
        transform.position = Vector3.MoveTowards(target.position, mousePosition, arrowSpeed * Time.deltaTime);

        // Rotate the arrow towards the mouse cursor
        Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);
    }
}