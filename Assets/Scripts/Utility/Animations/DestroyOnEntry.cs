// Author(s): Paul Calande
// Destroys the GameObject upon entry to this state.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEntry : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}