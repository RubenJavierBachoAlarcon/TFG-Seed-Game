using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmMelee : MonoBehaviour
{
    public Vector3 attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public void Atack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackPoint.x;
        pos += transform.up * attackPoint.y;

        Collider2D collider2D = Physics2D.OverlapCircle(pos, attackRange, playerLayer);

        if (collider2D != null)
        {
            Vector2 attackDirection = (collider2D.transform.position - transform.position).normalized;
            collider2D.GetComponent<ColisionEnemigo>().getAtacked(attackDirection);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackPoint.x;
        pos += transform.up * attackPoint.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
