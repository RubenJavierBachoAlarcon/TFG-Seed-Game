using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{

    [Range(1, 10)] public float velocidad;
    Rigidbody2D rigidbody2;
    SpriteRenderer spriteRenderer;

    bool isGrounded = true;
    [Range(1, 500)] public float fuerzaSalto;

    Animator animator;

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal");
        rigidbody2.velocity = new Vector2(movimiento * velocidad, rigidbody2.velocity.y);

        if (movimiento < 0)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = true;
        }
        else if (movimiento > 0)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = false;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, fuerzaSalto);
            isGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    
    }
   
}
