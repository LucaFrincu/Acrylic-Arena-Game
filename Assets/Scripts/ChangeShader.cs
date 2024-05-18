using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    public Color newColor = Color.green; // The new color you want to apply to the shader
    public string nameShader= "_Outline";
    void Start()
    {
        // Get the material attached to the GameObject's Renderer component
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material material = renderer.material;

        // Change the color of the "_Color" property in the shader to the new color
        material.SetColor(name, newColor);
    }
}
