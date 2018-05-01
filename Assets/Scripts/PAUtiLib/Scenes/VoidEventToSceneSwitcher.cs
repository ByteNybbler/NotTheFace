// Author(s): Paul Calande
// Switches scenes when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToSceneSwitcher : SceneSwitcher
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }
}