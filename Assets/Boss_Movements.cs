using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movements : StateMachineBehaviour
{

    private float attackTimer;
    public float attackDelay = 0f;

    public GameObject armPrefab;

    public static Animator BossAnimator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossAnimator = animator;
        if (animator.GetBool("isEnraged"))
        {
            attackDelay = 3;
        }
        attackTimer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!AnimacionInicioNiveles.isPlaying)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;
                ChooseAtack(animator);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void ChooseAtack(Animator animator)
    {
        int atack = 0;
        if (PlayerMovement.isEmpowered)
        {
            atack = Random.Range(0, 3);
        }
        else
        {
            atack = Random.Range(0, 2);
        }
        switch (atack)
        {
            case 0:
                animator.SetTrigger("LaserCast");
                animator.gameObject.SendMessage("ActivateLaser");
                break;
            case 1:
                animator.SetTrigger("ArmLaunch");
                break;
            case 2:
                animator.SetTrigger("Inmune");
                break;
        }
    }

}
