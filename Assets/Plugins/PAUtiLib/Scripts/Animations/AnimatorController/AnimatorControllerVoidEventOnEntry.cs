// Author(s): Paul Calande
// Invokes a VoidEvent upon entry to this state.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerVoidEventOnEntry : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VoidEvent voidEvent = animator.GetComponent<VoidEvent>();
        voidEvent.OnFired();
    }
}