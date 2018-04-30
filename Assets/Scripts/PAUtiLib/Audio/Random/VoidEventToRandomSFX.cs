// Author(s): Paul Calande
// Plays a random sound when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToRandomSFX : RandomSFX
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }
}