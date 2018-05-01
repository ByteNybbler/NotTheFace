// Author(s): Paul Calande
// Invokes a VoidEvent based on a random chance.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChanceToVoidEvent : VoidEvent
{
    [SerializeField]
    [Tooltip("The chance [0-1] of invoking the VoidEvent.")]
    float chance;

    private void Start()
    {
        if (UtilRandom.Bool(chance))
        {
            OnFired();
        }
    }
}