// Author(s): Paul Calande
// Changes the weight of an animator layer when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToAnimatorWeight : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The animator to change the layer weight of.")]
    Animator animator;
    [SerializeField]
    [Tooltip("The index of the layer.")]
    int layerIndex;
    [SerializeField]
    [Tooltip("The new weight to assign to the layer.")]
    float layerWeight;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        animator.SetLayerWeight(layerIndex, layerWeight);
    }
}