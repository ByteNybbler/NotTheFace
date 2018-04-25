// Author(s): Paul Calande
// Changes the active state of an object via VoidEvent.

using UnityEngine;

public class VoidEventToActivation : Activation
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }
}