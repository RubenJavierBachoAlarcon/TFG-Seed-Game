using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 4f;

    private void Start()
    {
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
