using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleMaterial : MonoBehaviour
{
    public Material material;

    void Start()
    {
        Color color = material.color;
        color.a = 0f; // Set alpha to 0
        material.color = color;
    }
}
