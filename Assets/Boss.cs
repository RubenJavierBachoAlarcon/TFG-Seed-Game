using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private bool isFacingRight = false; // Variable to keep track of which way the boss is facing
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer 

    public int vida = 6; // Vida del jefe

    public GameObject laserObject;

    private Animator animator;
    private Animator laserAnimator;

    public GameObject[] gemMaps;

    public MusicScriptLevel musicScriptLevel;

    void Start()
    {
        laserAnimator = laserObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer   
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            if (isFacingRight && player.position.x < transform.position.x)
                Flip();
        }
        else if (!isFacingRight && player.position.x > transform.position.x)
        {
            Flip();
        }
    }

    public void TakeDamage()
    {
        vida--;
        switch (vida)
        {
            case 6:
                gemMaps[0].SetActive(true);
                break;
            case 5:
                gemMaps[1].SetActive(true);
                gemMaps[0].SetActive(false);
                break;
            case 4:
                gemMaps[2].SetActive(true);
                gemMaps[1].SetActive(false);
                break;
            case 3:
                gemMaps[3].SetActive(true);
                gemMaps[2].SetActive(false);
                break;
            case 2:
                gemMaps[4].SetActive(true);
                gemMaps[3].SetActive(false);
                break;
            case 1:
                gemMaps[5].SetActive(true);
                gemMaps[4].SetActive(false);
                break;
            case 0:
                animator.SetTrigger("Die");
                musicScriptLevel.EndAudio();
                GetComponent<BoxCollider2D>().enabled = false;
                gemMaps[5].SetActive(false);
                break;
            default:
                animator.SetTrigger("Die");
                break;
        }

        if (vida <= 3 && vida > 0)
        {
            animator.SetTrigger("Enraged");
            laserAnimator.speed = 1.5f;
        }
        Debug.Log("Vida del jefe: " + vida);
        // Iniciar la corutina para cambiar el color a rojo y luego volver al original
        StartCoroutine(FlashRed());
        PlayerMovement.isEmpowered = false;
    }

    IEnumerator FlashRed()
    {
        // Guardar el color original
        Color originalColor = spriteRenderer.color;

        // Cambiar el color a rojo
        spriteRenderer.color = Color.red;

        // Esperar 0.2 segundos (puedes ajustar este valor según lo necesites)
        yield return new WaitForSeconds(0.2f);

        // Volver al color original
        spriteRenderer.color = originalColor;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight; // Flip the boolean
        spriteRenderer.flipX = !spriteRenderer.flipX; // Flip the sprite
    }

    public void ActivateLaser()
    {
        laserObject.SetActive(true);
    }

    public void DeactivateLaser()
    {
        laserObject.SetActive(false);
    }
}

