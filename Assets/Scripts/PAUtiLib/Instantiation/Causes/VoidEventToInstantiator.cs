// Author(s): Paul Calande
// Runs an Instantiator when a VoidEvent is invoked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventToInstantiator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The VoidEvent to subscribe to.")]
    VoidEvent voidEvent;
    [SerializeField]
    [Tooltip("Instantiates a GameObject when the VoidEvent is invoked.")]
    Instantiator instantiator;

    private void Awake()
    {
        voidEvent.Subscribe(Fire);
    }

    private void Fire()
    {
        instantiator.Instantiate();
    }
}