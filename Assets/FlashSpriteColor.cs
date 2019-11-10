using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpriteColor : MonoBehaviour
{
    [SerializeField] float flashingSpeed = 0.1f;

    float hueTimer = 0.0f;
    List<SpriteRenderer> renderers;

    
    private void Start()
    {
        renderers = new List<SpriteRenderer>();
        renderers.Add(GetComponent<SpriteRenderer>());
        renderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    private void Update()
    {
        foreach(var ren in renderers)
        {
            ren.color = Color.HSVToRGB(hueTimer % 1.0f, 0.5f, 1.0f);
        }
        hueTimer += Time.deltaTime * flashingSpeed;
    }
}
