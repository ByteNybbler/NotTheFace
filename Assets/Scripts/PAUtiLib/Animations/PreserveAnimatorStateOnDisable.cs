// Author(s): Paul Calande
// Preserves Animator state so that it does not get lost upon component disable.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveAnimatorStateOnDisable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Animator to preserve.")]
    Animator animator;

    AnimatorStateTracker ast = new AnimatorStateTracker();

    private void OnDisable()
    {
        ast.ReadFrom(animator);
    }

    private void OnEnable()
    {
        ast.WriteTo(animator);
    }
}