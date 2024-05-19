using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionEnemigo : MonoBehaviour
{
    public Boss boss;

    public float reboundForce = 10f; // Fuerza de rebote
    public Vector2 respawnPoint;
    private Vector2 checkPoint;
    public float respawnDelay = 1f; // Tiempo de espera antes de reaparecer
    public Animator animator;
    public int vida = 10; // Vida del jugador
    private bool isDying = false;


    // Necesario para trabajar con UI

    public Image healthBar; // Referencia al componente Image
    public Sprite[] sprites; // Array de sprites
    public Sprite defaultSprite;

    public SpriteRenderer spriteRenderer;

    public PauseMenu pauseMenu;

    private void Start()
    {
        PlayerMovement.isDying = false;
        PlayerMovement.isRespawning = false;
        checkPoint = respawnPoint;
        animator = GetComponent<Animator>();
        healthBar.sprite = defaultSprite;
    }



    private void Update()
    {
        if (vida >= 0 && vida < sprites.Length) // Asegúrate de que el índice esté dentro del rango del array
        {
            healthBar.sprite = sprites[vida]; // Cambia el sprite basándote en el valor de vida
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDying)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Boss") && PlayerMovement.isEmpowered && PlayerMovement.IsDashing && !Boss_Inmune.isInmune)
        {
            boss.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {

            // Decrementa la vida
            vida--;

            // Calcula la dirección del rebote
            Vector2 reboundDirection = (transform.position - collision.transform.position).normalized;

            // Aplica la fuerza de rebote
            GetComponent<Rigidbody2D>().AddForce(reboundDirection * reboundForce, ForceMode2D.Impulse);

            // Si la vida es 0, el jugador muere
            if (vida <= 0)
            {
                PlayerMovement.isDying = true;
                animator.SetTrigger("Die");

                // Comienza la corrutina de reaparición
                pauseMenu.GameOver();
            }
            else
            {
                // Comienza la corrutina de cambio de color
                StartCoroutine(ChangeColorAfterDamage());
                StartCoroutine(ShakeHealthBar());
            }
        }
        else if (collision.gameObject.CompareTag("Pinchos")) // Asegúrate de que el objeto tenga la etiqueta "Pinchos"
        {
            // Decrementa la vida
            vida--;

            // Comienza la corrutina de shake en la barra de vida
            StartCoroutine(ShakeHealthBar());

            if (!isDying)
            {
                PlayerMovement.isDying = true;
                isDying = true; 
                animator.SetTrigger("Die");
                if (vida <= 0)
                {
                    // Comienza la corrutina de reaparición
                    pauseMenu.GameOver();
                }
                else
                {
                    StartCoroutine(RespawnCheckPointAfterDelay());
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying)
        {
            return;
        }

        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("CheckPoint");
            // Actualiza la posición del checkPoint para que sea la posición del "CheckPoint"
            checkPoint = collision.transform.position;
            //Checkpoint debug pos
            Debug.Log(checkPoint);
        }
    }

    public void getAtacked(Vector2 attackDirection)
    {
        // Decrementa la vida
        vida--;

        // Aplica la fuerza de rebote
        GetComponent<Rigidbody2D>().AddForce(attackDirection * reboundForce, ForceMode2D.Impulse);

        // Si la vida es 0, el jugador muere
        if (vida <= 0)
        {
            PlayerMovement.isDying = true;
            animator.SetTrigger("Die");

            // Comienza la corrutina de reaparición
            pauseMenu.GameOver();
        }
        else
        {
            // Comienza la corrutina de cambio de color
            StartCoroutine(ChangeColorAfterDamage());
            StartCoroutine(ShakeHealthBar());
        }
    }

    IEnumerator RespawnCheckPointAfterDelay()
    {
        PlayerMovement.isRespawning = true;
        // Espera el tiempo especificado
        yield return new WaitForSeconds(respawnDelay);

        // Detiene el movimiento del personaje
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // "Muerte" del jugador y reaparición en el punto de control
        transform.position = checkPoint;

        PlayerMovement.isDying = false;
        isDying = false;
        animator.Play("Apareciendo");
        yield return new WaitForSeconds(0.6f);

        // Verifica si el personaje está en contacto con el viento
        if (PlayerMovement.isWindUpActive)
        {
            // Si es así, reproduce la animación de caída
            animator.Play("Cayendo");
        }
        else
        {
            animator.Play("Quieto");
        }

        PlayerMovement.isRespawning = false;
    }

    IEnumerator ChangeColorAfterDamage()
    {
        // Cambia el color del sprite a rojo
        spriteRenderer.color = Color.red;

        // Espera 0.2 segundos
        yield return new WaitForSeconds(0.2f);

        // Cambia el color del sprite de nuevo al original
        if (PlayerMovement.isEmpowered){
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    IEnumerator ShakeHealthBar()
    {
        RectTransform healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        Vector3 originalPosition = healthBarRectTransform.localPosition;

        float elapsed = 0.0f;
        float duration = 0.2f; // Duración del efecto de shake
        float magnitude = 5f; // Magnitud del efecto de shake

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            healthBarRectTransform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        healthBarRectTransform.localPosition = originalPosition;
    }





}
