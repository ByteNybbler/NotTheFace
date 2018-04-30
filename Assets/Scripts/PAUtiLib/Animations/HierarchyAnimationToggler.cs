// Author(s): Paul Calande
// Toggles all Animator components in a GameObject's hierarchy.
// Only toggles Animator components that are enabled at odd-numbered Toggle calls
// (1st call, 3rd call, etc).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyAnimationToggler : MonoBehaviour
{
    // All Animator components in the hierarchy.
    Animator[] animators;
    // List of Animator components to re-enable.
    List<Animator> toEnable = new List<Animator>();
    // Whether this script is about to re-enable any Animator components that it disabled.
    bool willEnable = false;

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    public void Toggle()
    {
        if (willEnable)
        {
            foreach (Animator animator in toEnable)
            {
                animator.enabled = true;
            }
            toEnable.Clear();
        }
        else
        {
            foreach (Animator animator in animators)
            {
                if (animator.enabled)
                {
                    animator.enabled = false;
                    toEnable.Add(animator);
                }
            }
        }
        willEnable = !willEnable;
    }
}