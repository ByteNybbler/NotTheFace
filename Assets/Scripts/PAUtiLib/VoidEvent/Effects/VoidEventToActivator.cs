// Author(s): Paul Calande
// Makes an Activator get fired by a VoidEvent.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToActivator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("The activator to fire.")]
    Activator activator;
    
    private void Start()
    {
        voidEvent.Subscribe(activator.Fire);
    }
}