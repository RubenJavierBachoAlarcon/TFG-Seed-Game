using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_Buff : StateMachineBehaviour
{
    public SpriteRenderer armor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isEnraged", true);
        animator.speed = 2f;
    }


}
