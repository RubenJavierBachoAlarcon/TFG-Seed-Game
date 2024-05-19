using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Atack : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;

    private float attackDelay = 2f;
    private float attackTimer;


    public float attackRange = 3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("isEnraged"))
        {
            attackDelay = 1.5f;
        }
        attackTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
                attackTimer = 0f;
            }
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
