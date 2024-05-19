using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_animate : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        animator.gameObject.SetActive(false);
    }
}
