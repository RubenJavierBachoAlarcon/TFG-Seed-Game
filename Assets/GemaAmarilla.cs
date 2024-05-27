using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GemaAmarilla : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    public int gemaId;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();

        Debug.Log("GemaAmarilla" + gemaId + " = " + PlayerPrefs.GetInt("GemaAmarilla" + gemaId, 0));

        if (PlayerPrefs.GetInt("GemaAmarilla" + gemaId, 0) == 1)
        {
            spriteRenderer.color = new Color32(80, 80, 0, 255);
            light2D.color = new Color(1, 1, 1, 1);
        }
    }
}


