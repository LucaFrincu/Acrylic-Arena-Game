using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.UI;

public class PaintSizeAnimation : MonoBehaviour
{
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void Increase()
    {
        LeanTween.scale(gameObject, originalScale * 1.2f, 0.2f).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, originalScale, 0.2f);
        });
    }

    public void Decrease()
    {
        LeanTween.scale(gameObject, originalScale * 0.8f, 0.2f).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, originalScale, 0.2f);
        });
    }

    void Update()
    {
       
    }
}
