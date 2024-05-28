using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 4f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (Boss_Movements.BossAnimator.GetBool("isEnraged"))
        {
            spriteRenderer.color = new Color(1, 0.5f, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1);
        }
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 attackDirection = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<ColisionEnemigo>().getAtacked(attackDirection);
        }
    }
}
