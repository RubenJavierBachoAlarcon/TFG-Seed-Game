using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{

    private SpriteRenderer sprite;
    private float posicionXAnterior;

    void Start()
    {
        posicionXAnterior = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.flipX = transform.position.x < posicionXAnterior;
        posicionXAnterior = transform.position.x;
    }
}
