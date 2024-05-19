using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public Transform player;
    Vector3 directionToPlayer;
    BoxCollider2D boxCollider2D;

    private void OnEnable()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    public void savePlayerPos()
    {
        directionToPlayer = (player.position - transform.position);
    }

    public void FireLaser()
    {
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        boxCollider2D.enabled = true;
    }

    public void StopLaser()
    {
        boxCollider2D.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ColisionEnemigo>().getAtacked(directionToPlayer);
        }
    }


}
